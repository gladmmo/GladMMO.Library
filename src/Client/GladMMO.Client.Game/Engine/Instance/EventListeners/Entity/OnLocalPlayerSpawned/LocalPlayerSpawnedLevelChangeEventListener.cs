using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Core;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;

namespace GladMMO
{
	//[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class LocalPlayerSpawnedLevelChangeEventListener : HubChangedLocalPlayerSpawnedEventListener
	{
		protected IUIUnitFrame PlayerUnitFrame { get; }

		/// <inheritdoc />
		public LocalPlayerSpawnedLevelChangeEventListener(ILocalPlayerSpawnedEventSubscribable subscriptionService, 
			IEntityDataChangeCallbackRegisterable entityDataCallbackRegister, 
			IReadonlyLocalPlayerDetails playerDetails,
			[KeyFilter(UnityUIRegisterationKey.PlayerUnitFrame)] [NotNull]
			IUIUnitFrame playerUnitFrame)
			: base(subscriptionService, entityDataCallbackRegister, playerDetails)
		{
			PlayerUnitFrame = playerUnitFrame ?? throw new ArgumentNullException(nameof(playerUnitFrame));
		}

		/// <inheritdoc />
		protected override void OnLocalPlayerSpawned(LocalPlayerSpawnedEventArgs args)
		{
			RegisterPlayerDataChangeCallback<int>(EUnitFields.UNIT_FIELD_LEVEL, OnLevelChanged);
		}

		private void OnLevelChanged(NetworkEntityGuid entity, EntityDataChangedArgs<int> changeArgs)
		{
			PlayerUnitFrame.UnitLevel.Text = changeArgs.NewValue.ToString();
		}
	}
}
