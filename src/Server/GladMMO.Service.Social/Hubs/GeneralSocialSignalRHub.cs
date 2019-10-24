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
	public sealed class GeneralSocialSignalRHub : AuthorizationReadySignalRHub<IRemoteSocialHubClient>,
		IRemoteSocialHubServer,
		IConnectionService,
		IPeerPayloadSendService<object>,
		IPeerRequestSendService<object>
	{
		private IEnumerable<IOnHubConnectionEventListener> OnConnectionHubListeners { get; }

		private IEnumerable<IEntityCollectionRemovable> EntityRemovable { get; }

		private ISocialModelMessageRouter<IRemoteSocialHubClient> MessageRouter { get; }

		/// <inheritdoc />
		public GeneralSocialSignalRHub(IClaimsPrincipalReader claimsReader,
			ILogger<GeneralSocialSignalRHub> logger,
			[JetBrains.Annotations.NotNull] IEnumerable<IOnHubConnectionEventListener> onConnectionHubListeners,
			[JetBrains.Annotations.NotNull] IEnumerable<IEntityCollectionRemovable> entityRemovable,
			[JetBrains.Annotations.NotNull] ISocialModelMessageRouter<IRemoteSocialHubClient> messageRouter)
			: base(claimsReader, logger)
		{
			OnConnectionHubListeners = onConnectionHubListeners ?? throw new ArgumentNullException(nameof(onConnectionHubListeners));
			EntityRemovable = entityRemovable ?? throw new ArgumentNullException(nameof(entityRemovable));
			MessageRouter = messageRouter ?? throw new ArgumentNullException(nameof(messageRouter));
		}

		private HubConnectionMessageContext<IRemoteSocialHubClient> CreateHubContext()
		{
			return new HubConnectionMessageContext<IRemoteSocialHubClient>(this, this, this, Groups, Clients, Context);
		}

		/// <inheritdoc />
		public override async Task OnConnectedAsync()
		{
			await base.OnConnectedAsync()
				.ConfigureAwait(false);

			if(Logger.IsEnabled(LogLevel.Information))
				Logger.LogInformation($"Account Connected: {ClaimsReader.GetUserName(Context.User)}:{ClaimsReader.GetUserId(Context.User)} with SignalR UserId: {Context.UserIdentifier}");

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
					Logger.LogError($"Account: {ClaimsReader.GetUserName(Context.User)}:{ClaimsReader.GetUserId(Context.User)} failed to properly connect to hub. Error: {e.ToString()}\n\nStack: {e.StackTrace}");

				Context.Abort();
			}
		}

		/// <inheritdoc />
		public override async Task OnDisconnectedAsync(Exception exception)
		{
			NetworkEntityGuid guid = new NetworkEntityGuidBuilder()
				.WithId(int.Parse(Context.UserIdentifier))
				.WithType(EntityType.Player)
				.Build();

			if(Logger.IsEnabled(LogLevel.Information))
				Logger.LogInformation($"About to attempt final cleanup for Entity: {guid}");

			foreach(var c in EntityRemovable)
				c.RemoveEntityEntry(guid);

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

		public Task SendTestMessageAsync(TestSocialModel testModel)
		{
			return MessageRouter.TryHandleMessage(CreateHubContext(), new NetworkIncomingMessage<BaseSocialModel>(new HeaderlessPacketHeader(0), testModel));
		}
	}
}