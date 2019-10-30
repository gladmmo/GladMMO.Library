using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
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

		private IZoneRegistryServiceQueueable ZoneRegisterQueueable { get; }

		public RegisterZoneServerOnlineEventListener(IServerStartedEventSubscribable subscriptionService,
			[NotNull] IZoneRegistryService zoneRegistryService,
			[NotNull] WorldConfiguration worldConfig,
			[NotNull] NetworkAddressInfo addressInfo,
			[NotNull] ILog logger,
			[NotNull] IZoneRegistryServiceQueueable zoneRegisterQueueable) 
			: base(subscriptionService)
		{
			ZoneRegistryService = zoneRegistryService ?? throw new ArgumentNullException(nameof(zoneRegistryService));
			WorldConfig = worldConfig ?? throw new ArgumentNullException(nameof(worldConfig));
			AddressInfo = addressInfo ?? throw new ArgumentNullException(nameof(addressInfo));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			ZoneRegisterQueueable = zoneRegisterQueueable ?? throw new ArgumentNullException(nameof(zoneRegisterQueueable));
		}

		protected override void OnEventFired(object source, EventArgs args)
		{
			//Server is almost certainly started so we should register now
			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				ResponseModel<ZoneServerRegistrationResponse, ZoneServerRegistrationResponseCode> registerResponse = await ZoneRegistryService.TryRegisterZoneServerAsync(new ZoneServerRegistrationRequest((int) WorldConfig.WorldId, (short) AddressInfo.Port));

				//If successful, people should now be able to see it and connect.
				//TODO: Handle error, probably would need to shutdown the application.
				if(!registerResponse.isSuccessful)
				{
					if (Logger.IsErrorEnabled)
						Logger.Error($"Failed to register online status of ZoneServer. Reason: {registerResponse.ResultCode}");

					throw new InvalidOperationException($"Failed ZoneServer register.");
				}
				
#pragma warning disable 4014
				UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
				{
					if (Logger.IsInfoEnabled)
						Logger.Error($"Starting checkin task.");

					while (UnityEngine.Application.isPlaying)
					{
						try
						{
							if (Logger.IsInfoEnabled)
								Logger.Error($"Checking in.");

							//TODO: Don't do this every second.
							await Task.Delay(1000);
							await ZoneRegisterQueueable.ZoneServerCheckinAsync(new ZoneServerCheckinRequestModel());
						}
						catch (Exception e)
						{
							if (Logger.IsErrorEnabled)
								Logger.Error($"Failed to checkin ZoneServer. Reason: {e.ToString()}");

							throw;
						}
					}
				});
#pragma warning restore 4014
			});
		}
	}
}
