using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac.Features.AttributeFilters;
using Common.Logging;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class TargetUnitFrameUIControllerEventListener : DataChangedLocalPlayerSpawnedEventListener
	{
		private IUIElement RootTargetUnitFrame { get; }

		private IUIUnitFrame TargetUnitFrame { get; }

		private ILog Logger { get; }

		public TargetUnitFrameUIControllerEventListener(ILocalPlayerSpawnedEventSubscribable subscriptionService, 
			IEntityDataChangeCallbackRegisterable entityDataCallbackRegister, 
			IReadonlyLocalPlayerDetails playerDetails,
			[NotNull] [KeyFilter(UnityUIRegisterationKey.TargetUnitFrame)] IUIElement rootTargetUnitFrame,
			[NotNull] [KeyFilter(UnityUIRegisterationKey.TargetUnitFrame)] IUIUnitFrame targetUnitFrame,
			[NotNull] ILog logger) 
			: base(subscriptionService, entityDataCallbackRegister, playerDetails)
		{
			RootTargetUnitFrame = rootTargetUnitFrame ?? throw new ArgumentNullException(nameof(rootTargetUnitFrame));
			TargetUnitFrame = targetUnitFrame ?? throw new ArgumentNullException(nameof(targetUnitFrame));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void OnLocalPlayerSpawned(LocalPlayerSpawnedEventArgs args)
		{
			RegisterPlayerDataChangeCallback<ulong>(EntityObjectField.UNIT_FIELD_TARGET, OnPlayerTargetChanged);
		}

		private void OnPlayerTargetChanged(NetworkEntityGuid entity, EntityDataChangedArgs<ulong> changeArgs)
		{
			NetworkEntityGuid guid = new NetworkEntityGuid(changeArgs.NewValue);

			if(Logger.IsDebugEnabled)
				Logger.Debug($"Player Target Changed to: {guid}");
		}
	}
}
