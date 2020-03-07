using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac.Features.AttributeFilters;
using Common.Logging;
using Glader.Essentials;
using GladNet;

namespace GladMMO
{
	[AdditionalRegisterationAs(typeof(ILocalPlayerTargetChangedEventListener))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class TargetUnitFrameUIControllerEventListener : DataChangedLocalPlayerSpawnedEventListener, ILocalPlayerTargetChangedEventListener
	{
		private IUIUnitFrame TargetUnitFrame { get; }

		private ILog Logger { get; }

		public event EventHandler<LocalPlayerTargetChangedEventArgs> OnPlayerTargetChanged;

		//Initially Empty because we have no target.
		public ObjectGuid CurrentTarget { get; private set; } = ObjectGuid.Empty;

		private IPeerPayloadSendService<GamePacketPayload> SendService { get; }

		public TargetUnitFrameUIControllerEventListener(ILocalPlayerSpawnedEventSubscribable subscriptionService, 
			IEntityDataChangeCallbackRegisterable entityDataCallbackRegister, 
			IReadonlyLocalPlayerDetails playerDetails,
			[NotNull] [KeyFilter(UnityUIRegisterationKey.TargetUnitFrame)] IUIUnitFrame targetUnitFrame,
			[NotNull] ILog logger,
			INetworkEntityVisibilityLostEventSubscribable networkVisibilityLostSubscriptionService,
			[NotNull] IPeerPayloadSendService<GamePacketPayload> sendService)
			: base(subscriptionService, entityDataCallbackRegister, playerDetails)
		{
			TargetUnitFrame = targetUnitFrame ?? throw new ArgumentNullException(nameof(targetUnitFrame));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));

			//Subscribe to when the target actually disappears.
			networkVisibilityLostSubscriptionService.OnNetworkEntityVisibilityLost += (sender, args) =>
			{
				UnityAsyncHelper.UnityMainThreadContext.Post(o =>
				{
					//This should be safe, we're checking if it was the current target still
					//user may have clicked on something else but this should be safe this way.
					if(args.EntityGuid == this.CurrentTarget)
						OnPlayerTargetDisappeared();
				}, null);
			};
		}

		private void OnPlayerTargetDisappeared()
		{
			if(Logger.IsDebugEnabled)
				Logger.Debug($"Player Target has disappeared.");

			//Disable it and let everyone know.
			TargetUnitFrame.SetElementActive(false);
			CurrentTarget = ObjectGuid.Empty;

			Logger.Error($"TODO REIMPLEMENT TARGET CLEARING ON DESPAWN");

			//We send an empty interaction packet to indicate our target should be cleared.
			//Server doesn't actually know the entity we targeted went out of scope.
			//SendService.SendMessage(new ClientInteractNetworkedObjectRequestPayload(ObjectGuid.Empty, ClientInteractNetworkedObjectRequestPayload.InteractType.Selection));
		}

		protected override void OnLocalPlayerSpawned(LocalPlayerSpawnedEventArgs args)
		{
			RegisterPlayerDataChangeCallback<ulong>(EUnitFields.UNIT_FIELD_TARGET, OnPlayerTargetEntityDatChanged);
		}

		private void OnPlayerTargetEntityDatChanged(ObjectGuid entity, EntityDataChangedArgs<ulong> changeArgs)
		{
			ObjectGuid guid = new ObjectGuid(changeArgs.NewValue);

			if(Logger.IsDebugEnabled)
				Logger.Debug($"Player Target Changed to: {guid}");

			CurrentTarget = guid;

			if (guid == ObjectGuid.Empty)
			{
				//target was cleared.
				if(Logger.IsDebugEnabled)
					Logger.Debug($"Player cleared target.");

				//TODO: We should register listener events to increase UI performance for untargeted callbacks
			}
			else
			{
				OnPlayerTargetChanged?.Invoke(this, new LocalPlayerTargetChangedEventArgs(guid));

				//We can at least set this active here I guess.
				TargetUnitFrame.SetElementActive(true);
			}
		}
	}
}
