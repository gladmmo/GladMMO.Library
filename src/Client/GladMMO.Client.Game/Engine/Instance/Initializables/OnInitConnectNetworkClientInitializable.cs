using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using GladNet;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class OnInitConnectNetworkClientInitializable : IGameInitializable
	{
		/// <summary>
		/// The managed network client that the Unity3D client is implemented on-top of.
		/// </summary>
		private IManagedNetworkClient<GameClientPacketPayload, GameServerPacketPayload> Client { get; }

		private IZoneServerService ZoneServiceClient { get; }

		private IReadonlyZoneDataRepository ZoneDataRepository { get; }

		private ILog Logger { get; }

		public OnInitConnectNetworkClientInitializable(
			[NotNull] IManagedNetworkClient<GameClientPacketPayload, GameServerPacketPayload> client,
			[NotNull] IZoneServerService zoneServiceClient,
			[NotNull] IReadonlyZoneDataRepository zoneDataRepository,
			[NotNull] ILog logger)
		{
			Client = client ?? throw new ArgumentNullException(nameof(client));
			ZoneServiceClient = zoneServiceClient ?? throw new ArgumentNullException(nameof(zoneServiceClient));
			ZoneDataRepository = zoneDataRepository ?? throw new ArgumentNullException(nameof(zoneDataRepository));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task OnGameInitialized()
		{
			if(Client.isConnected)
				throw new InvalidOperationException($"Encountered network client already initialized.");

			//To know where we must connect, we must query for the zone-endpoint that we should be connecting to.
			ResolveServiceEndpointResponse endpointResponse = await ZoneServiceClient.GetServerEndpoint(ZoneDataRepository.ZoneId);

			if(endpointResponse.isSuccessful)
				if(Logger.IsInfoEnabled)
					Logger.Info($"");

			if(Logger.IsInfoEnabled)
				Logger.Info($"Recieved ZoneEndpoint: {endpointResponse.ToString()}");
			
			//TODO: Handle failed queries, probably have to sent back to titlescreen
			if (endpointResponse.isSuccessful)
			{
				//TODO: Handle DNS
				if (await Client.ConnectAsync(endpointResponse.Endpoint.EndpointAddress,
					endpointResponse.Endpoint.EndpointPort))
				{
					//TODO: We could broadcast a successful connection, but no listeners/interest right now.
					if(Logger.IsDebugEnabled)
						Logger.Debug($"Connected to: {endpointResponse}");
				}
			}
		}
	}
}
