using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac.Features.AttributeFilters;
using Common.Logging;
using Glader.Essentials;

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
		public NetworkEntityGuid CurrentTarget { get; private set; } = NetworkEntityGuid.Empty;

		public TargetUnitFrameUIControllerEventListener(ILocalPlayerSpawnedEventSubscribable subscriptionService, 
			IEntityDataChangeCallbackRegisterable entityDataCallbackRegister, 
			IReadonlyLocalPlayerDetails playerDetails,
			[NotNull] [KeyFilter(UnityUIRegisterationKey.TargetUnitFrame)] IUIUnitFrame targetUnitFrame,
			[NotNull] ILog logger,
			INetworkEntityVisibilityLostEventSubscribable networkVisibilityLostSubscriptionService)
			: base(subscriptionService, entityDataCallbackRegister, playerDetails)
		{
			TargetUnitFrame = targetUnitFrame ?? throw new ArgumentNullException(nameof(targetUnitFrame));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));

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
			CurrentTarget = NetworkEntityGuid.Empty;
		}

		protected override void OnLocalPlayerSpawned(LocalPlayerSpawnedEventArgs args)
		{
			RegisterPlayerDataChangeCallback<ulong>(EntityObjectField.UNIT_FIELD_TARGET, OnPlayerTargetEntityDatChanged);
		}

		private void OnPlayerTargetEntityDatChanged(NetworkEntityGuid entity, EntityDataChangedArgs<ulong> changeArgs)
		{
			NetworkEntityGuid guid = new NetworkEntityGuid(changeArgs.NewValue);

			if(Logger.IsDebugEnabled)
				Logger.Debug($"Player Target Changed to: {guid}");

			CurrentTarget = guid;
			OnPlayerTargetChanged?.Invoke(this, new LocalPlayerTargetChangedEventArgs(guid));

			//We can at least set this active here I guess.
			TargetUnitFrame.SetElementActive(true);
		}
	}
}
