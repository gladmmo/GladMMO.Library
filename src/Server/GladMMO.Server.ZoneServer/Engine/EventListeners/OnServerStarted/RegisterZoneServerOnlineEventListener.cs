using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Glader.Essentials;
using GladNet;
using Nito.AsyncEx;

namespace GladMMO
{
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class RegisterZoneServerOnlineEventListener : BaseOnServerStartedEventListener
	{
		private IZoneRegistryService ZoneRegistryService { get; }

		private WorldConfiguration WorldConfig { get; }

		private NetworkAddressInfo AddressInfo { get; }

		private ILog Logger { get; }

		public RegisterZoneServerOnlineEventListener(IServerStartedEventSubscribable subscriptionService,
			[NotNull] IZoneRegistryService zoneRegistryService,
			[NotNull] WorldConfiguration worldConfig,
			[NotNull] NetworkAddressInfo addressInfo,
			[NotNull] ILog logger) 
			: base(subscriptionService)
		{
			ZoneRegistryService = zoneRegistryService ?? throw new ArgumentNullException(nameof(zoneRegistryService));
			WorldConfig = worldConfig ?? throw new ArgumentNullException(nameof(worldConfig));
			AddressInfo = addressInfo ?? throw new ArgumentNullException(nameof(addressInfo));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void OnEventFired(object source, EventArgs args)
		{
			//Server is almost certainly started so we should register now
			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				ResponseModel<ZoneServerRegistrationResponse, ZoneServerRegistrationResponseCode> registerResponse = await ZoneRegistryService.TryRegisterZoneServerAsync(new ZoneServerRegistrationRequest((int) WorldConfig.WorldId, (short) AddressInfo.Port));

				//TODO: Handle error, probably would need to shutdown the application.
				if (!registerResponse.isSuccessful)
				{
					if (Logger.IsErrorEnabled)
						Logger.Error($"Failed to register online status of ZoneServer. Reason: {registerResponse.ResultCode}");

					throw new InvalidOperationException($"Failed ZoneServer register.");
				}

				//TODO: Start checkin Queue.
				//TODO: Enable repeating zoneserver checkin.
				//If successful, people should now be able to see it and connect.
			});
		}
	}
}
