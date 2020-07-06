using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using GladNet;
using UnityEngine.SceneManagement;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class OnInitConnectNetworkClientInitializable : IGameInitializable
	{
		/// <summary>
		/// The managed network client that the Unity3D client is implemented on-top of.
		/// </summary>
		private IConnectionService Client { get; }

		private IZoneDataService ZoneServiceClient { get; }

		private IReadonlyZoneDataRepository ZoneDataRepository { get; }

		private ILog Logger { get; }

		private IGeneralErrorEncounteredEventPublisher GeneralErrorPublisher { get; }

		private IInstanceLogoutEventPublisher LogoutEventPublisher { get; }

		public OnInitConnectNetworkClientInitializable(
			[NotNull] IConnectionService client,
			[NotNull] IZoneDataService zoneServiceClient,
			[NotNull] IReadonlyZoneDataRepository zoneDataRepository,
			[NotNull] ILog logger,
			[NotNull] IGeneralErrorEncounteredEventPublisher generalErrorPublisher,
			[NotNull] IInstanceLogoutEventPublisher logoutEventPublisher)
		{
			Client = client ?? throw new ArgumentNullException(nameof(client));
			ZoneServiceClient = zoneServiceClient ?? throw new ArgumentNullException(nameof(zoneServiceClient));
			ZoneDataRepository = zoneDataRepository ?? throw new ArgumentNullException(nameof(zoneDataRepository));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			GeneralErrorPublisher = generalErrorPublisher ?? throw new ArgumentNullException(nameof(generalErrorPublisher));
			LogoutEventPublisher = logoutEventPublisher ?? throw new ArgumentNullException(nameof(logoutEventPublisher));
		}

		public async Task OnGameInitialized()
		{
			try
			{
				if(Client.isConnected)
					throw new InvalidOperationException($"Encountered network client already initialized.");

				//To know where we must connect, we must query for the zone-endpoint that we should be connecting to.
				ResolveServiceEndpointResponse endpointResponse = await ZoneServiceClient.GetZoneConnectionEndpointAsync(ZoneDataRepository.ZoneId);

				//TODO: Handle failed queries, probably have to sent back to titlescreen
				if(endpointResponse.isSuccessful)
				{
					if(Logger.IsInfoEnabled)
						Logger.Info($"Recieved ZoneEndpoint: {endpointResponse.ToString()}");

					//TODO: Handle DNS
					if(await Client.ConnectAsync(endpointResponse.Endpoint.Address,
						endpointResponse.Endpoint.Port))
					{
						//TODO: We could broadcast a successful connection, but no listeners/interest right now.
						if(Logger.IsDebugEnabled)
							Logger.Debug($"Connected to: {endpointResponse}");
					}
				}
				else if(Logger.IsErrorEnabled)
					Logger.Error($"Failed to query ZoneEndpoint. Reason: {endpointResponse.ResultCode}");
			}
			catch (Exception e)
			{
				if(Logger.IsErrorEnabled)
					Logger.Error($"Failed to initialize instance server connection. Reason: {e.Message}");

				//We NEVER throw within an initializer
				GeneralErrorPublisher.PublishEvent(this, new GeneralErrorEncounteredEventArgs("Network Error", "Failed to initialize instance server connection.", () => LogoutEventPublisher.PublishEvent(this, EventArgs.Empty)));
			}
		}
	}
}
