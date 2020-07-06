﻿using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Nito.AsyncEx;

namespace GladMMO
{
	[AdditionalRegisterationAs(typeof(IRealtimeSocialServiceConnectedEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class InitializeRealtimeSocialServiceEventListener : OnLocalPlayerSpawnedEventListener, IRealtimeSocialServiceConnectedEventSubscribable
	{
		private ILog Logger { get; }

		private IServiceDiscoveryService ServiceDiscoveryService { get; }

		private ILocalPlayerDetails PlayerDetails { get; }

		private IReadonlyAuthTokenRepository AuthTokenProvider { get; }

		private IRemoteSocialHubClient RemoteSocialClient { get; }

		private IEnumerable<IConnectionHubInitializable> ConnectionHubInitializable { get; }

		public event EventHandler OnRealtimeSocialServiceConnected;

		public InitializeRealtimeSocialServiceEventListener(ILocalPlayerSpawnedEventSubscribable subscriptionService,
			[NotNull] ILog logger,
			[NotNull] IServiceDiscoveryService serviceDiscoveryService,
			[NotNull] ILocalPlayerDetails playerDetails,
			[NotNull] IReadonlyAuthTokenRepository authTokenProvider,
			[NotNull] IRemoteSocialHubClient remoteSocialClient,
			[NotNull] IEnumerable<IConnectionHubInitializable> connectionHubInitializable) 
			: base(subscriptionService)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			ServiceDiscoveryService = serviceDiscoveryService ?? throw new ArgumentNullException(nameof(serviceDiscoveryService));
			PlayerDetails = playerDetails ?? throw new ArgumentNullException(nameof(playerDetails));
			AuthTokenProvider = authTokenProvider ?? throw new ArgumentNullException(nameof(authTokenProvider));
			RemoteSocialClient = remoteSocialClient ?? throw new ArgumentNullException(nameof(remoteSocialClient));
			ConnectionHubInitializable = connectionHubInitializable ?? throw new ArgumentNullException(nameof(connectionHubInitializable));
		}

		protected override void OnLocalPlayerSpawned(LocalPlayerSpawnedEventArgs args)
		{
			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				try
				{
					//We need to connect the hub to the social backend
					ResolveServiceEndpointResponse endpointResponse = await ServiceDiscoveryService.DiscoverService(new ResolveServiceEndpointRequest(ClientRegionLocale.US, GladMMONetworkConstants.SOCIAL_SERVICE_NAME))
						.ConfigureAwaitFalse();

					if(!endpointResponse.isSuccessful)
						throw new InvalidOperationException($"Failed to query for SocialService. Reason: {endpointResponse.ResultCode}");

					string hubConnectionString = $@"{endpointResponse.Endpoint.Address}:{endpointResponse.Endpoint.Port}/realtime/social";

					if(Logger.IsInfoEnabled)
						Logger.Info($"Social HubConnection String: {hubConnectionString}");

					//TODO: Handle failed service disc query
					HubConnection connection = new HubConnectionBuilder()
						.WithUrl(hubConnectionString, options =>
						{
							options.Headers.Add(SocialNetworkConstants.CharacterIdRequestHeaderName, PlayerDetails.LocalPlayerGuid.CurrentObjectGuid.ToString());
							options.AccessTokenProvider = () => Task.FromResult(AuthTokenProvider.Retrieve());
						})
						.AddJsonProtocol()
						.Build();

					//Register the reciever interface instance for the Connection Hub
					connection.RegisterClientInterface<IRemoteSocialHubClient>(RemoteSocialClient);

					foreach (var initable in ConnectionHubInitializable)
						initable.Connection = connection;

					//Just start the service when the game initializes
					//This will make it so that the signalR clients will start to recieve messages.
					await connection.StartAsync()
						.ConfigureAwaitFalseVoid();

					if(Logger.IsInfoEnabled)
						Logger.Info($"Connected to realtime Social Service.");

					OnRealtimeSocialServiceConnected?.Invoke(this, EventArgs.Empty);
				}
				catch (Exception e)
				{
					if(Logger.IsErrorEnabled)
						Logger.Error($"Failed to connect to Social Service: {e.ToString()}");

					throw;
				}
			});
		}
	}
}
