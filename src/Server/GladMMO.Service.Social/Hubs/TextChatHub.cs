using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using GladNet;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	[AuthorizeJwt]
	public sealed class TextChatHub : AuthorizationReadySignalRHub<IRemoteSocialTextChatHubClient>, 
		IRemoteSocialTextChatHubServer, 
		IConnectionService, 
		IPeerPayloadSendService<object>, 
		IPeerRequestSendService<object>
	{
		private ISocialServiceToGameServiceClient SocialToGameClient { get; }

		//I am not happy about this, but we need to maintain some state so that we know what zone a connection is in.
		private IConnectionToZoneMappable ZoneLookupService { get; }

		private IEnumerable<IOnHubConnectionEventListener> OnConnectionHubListeners { get; }

		/// <summary>
		/// This is the dependency injection service creator.
		/// It should ONLY be used to create handlers for messages.
		/// </summary>
		private IServiceProvider ServiceProvider { get; }

		private IEntityDataLockingService EntityLockService { get; }

		private IEnumerable<IEntityCollectionRemovable> EntityRemovable { get; }

		/// <inheritdoc />
		public TextChatHub(IClaimsPrincipalReader claimsReader, 
			ILogger<TextChatHub> logger, 
			[JetBrains.Annotations.NotNull] ISocialServiceToGameServiceClient socialToGameClient,
			[JetBrains.Annotations.NotNull] IConnectionToZoneMappable zoneLookupService,
			[JetBrains.Annotations.NotNull] IEnumerable<IOnHubConnectionEventListener> onConnectionHubListeners,
			[JetBrains.Annotations.NotNull] IServiceProvider serviceProvider,
			[JetBrains.Annotations.NotNull] IEntityDataLockingService entityLockService,
			[JetBrains.Annotations.NotNull] IEnumerable<IEntityCollectionRemovable> entityRemovable) 
			: base(claimsReader, logger)
		{
			SocialToGameClient = socialToGameClient ?? throw new ArgumentNullException(nameof(socialToGameClient));
			ZoneLookupService = zoneLookupService ?? throw new ArgumentNullException(nameof(zoneLookupService));
			OnConnectionHubListeners = onConnectionHubListeners ?? throw new ArgumentNullException(nameof(onConnectionHubListeners));
			ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
			EntityLockService = entityLockService ?? throw new ArgumentNullException(nameof(entityLockService));
			EntityRemovable = entityRemovable ?? throw new ArgumentNullException(nameof(entityRemovable));
		}

		/// <inheritdoc />
		public async Task SendZoneChannelTextChatMessageAsync(ZoneChatMessageRequestModel message)
		{
			await HandleIncomingHubRequest<ZoneChatMessageRequestModel, ZoneMessageBroadcastMessageHandler>(message)
				.ConfigureAwait(false);
		}

		//This is fun!
		private async Task HandleIncomingHubRequest<TRequestType, TMessageHandlerType>(TRequestType message)
			where TMessageHandlerType : IPeerPayloadSpecificMessageHandler<TRequestType, object, HubConnectionMessageContext<IRemoteSocialTextChatHubClient>> 
			where TRequestType : class
		{
			TMessageHandlerType handler = ServiceProvider.GetService<TMessageHandlerType>();

			await handler.HandleMessage(CreateHubContext(), message)
				.ConfigureAwait(false);
		}

		/// <inheritdoc />
		public async Task SendGuildChannelTextChatMessageAsync(GuildChatMessageRequestModel message)
		{
			await HandleIncomingHubRequest<GuildChatMessageRequestModel, GuildMessageBroadcastMessageHandler>(message)
				.ConfigureAwait(false);
		}

		private HubConnectionMessageContext<IRemoteSocialTextChatHubClient> CreateHubContext()
		{
			return new HubConnectionMessageContext<IRemoteSocialTextChatHubClient>(this, this, this, Groups, Clients, Context);
		}

		/// <inheritdoc />
		public override async Task OnConnectedAsync()
		{
			await base.OnConnectedAsync()
				.ConfigureAwait(false);

			if(Logger.IsEnabled(LogLevel.Information))
				Logger.LogInformation($"Account Connected: {ClaimsReader.GetUserName(Context.User)}:{ClaimsReader.GetUserId(Context.User)}");

			NetworkEntityGuid guid = new NetworkEntityGuidBuilder()
				.WithId(int.Parse(Context.UserIdentifier))
				.WithType(EntityType.Player)
				.Build();

			//Register interest and then lock
			//We need to lock on the entity so only 1 connection for the entity can go through this process at a time.
			await EntityLockService.RegisterEntityInterestAsync(guid)
				.ConfigureAwait(false);
			using(await EntityLockService.AquireEntityLockAsync(guid).ConfigureAwait(false))
			{
				try
				{
					foreach(var listener in OnConnectionHubListeners)
					{
						HubOnConnectionState connectionState = await listener.OnConnected(this).ConfigureAwait(false);

						//if the listener indicated we need to abort for whatever reason we
						//should believe it and just abort.
						if(connectionState == HubOnConnectionState.Abort)
						{
							Context.Abort();
							break;
						}
					}
				}
				catch(Exception e)
				{
					if(Logger.IsEnabled(LogLevel.Error))
						Logger.LogInformation($"Account: {ClaimsReader.GetUserName(Context.User)}:{ClaimsReader.GetUserId(Context.User)} failed to properly connect to hub. Error: {e.Message}\n\nStack: {e.StackTrace}");

					Context.Abort();
				}
			}
		}

		/// <inheritdoc />
		public override async Task OnDisconnectedAsync(Exception exception)
		{
			NetworkEntityGuid guid = new NetworkEntityGuidBuilder()
				.WithId(int.Parse(Context.UserIdentifier))
				.WithType(EntityType.Player)
				.Build();

			//Right now this doesn't depend on entity, so we just do it.
			if(ZoneLookupService.Contains(Context.ConnectionId))
				ZoneLookupService.Unregister(Context.ConnectionId);

			if(Logger.IsEnabled(LogLevel.Information))
				Logger.LogInformation($"About to attempt final cleanup for Entity: {guid}");

			//If the entity is no longer contained we should clear up
			FinalEntityLockResult entityLockResult = await EntityLockService.TryAquireFinalEntityLockAsync(guid);
			if(entityLockResult.Result)
			{
				if(Logger.IsEnabled(LogLevel.Information))
					Logger.LogInformation($"Clearing Entity data for Entity: {guid}. Last connection related to the Entity.");

				using(entityLockResult.LockObject)
				{
					foreach(var c in EntityRemovable)
					{
						c.RemoveEntityEntry(guid);
					}
				}
			}
			else
			{
				if(Logger.IsEnabled(LogLevel.Information))
					Logger.LogInformation($"Entity: {guid} still has active connections/sessions claiming interest. Won't cleanup entity data.");

				//We still need to release interest
				//so that the ref count goes down, otherwise it'll never be cleaned up.
				await this.EntityLockService.ReleaseEntityInterestAsync(guid)
					.ConfigureAwait(false);
			}

			await base.OnDisconnectedAsync(exception);
		}

		/// <inheritdoc />
		Task IDisconnectable.DisconnectAsync(int delay)
		{
			Context.Abort();
			return Task.CompletedTask;
		}

		/// <inheritdoc />
		Task<bool> IConnectable.ConnectAsync(string ip, int port)
		{
			throw new NotSupportedException($"This does not make sense for SignalR.");
		}

		/// <inheritdoc />
		public bool isConnected => !Context.ConnectionAborted.IsCancellationRequested;

		/// <inheritdoc />
		Task<SendResult> IPeerPayloadSendService<object>.SendMessage<TPayloadType>(TPayloadType payload, DeliveryMethod method)
		{
			throw new NotSupportedException($"This does not make sense for SignalR.");
		}

		/// <inheritdoc />
		Task<SendResult> IPeerPayloadSendService<object>.SendMessageImmediately<TPayloadType>(TPayloadType payload, DeliveryMethod method)
		{
			throw new NotSupportedException($"This does not make sense for SignalR.");
		}

		/// <inheritdoc />
		Task<TResponseType> IPeerRequestSendService<object>.SendRequestAsync<TResponseType>(object request, DeliveryMethod method, CancellationToken cancellationToken)
		{
			throw new NotSupportedException($"This does not make sense for SignalR.");
		}
	}
}
