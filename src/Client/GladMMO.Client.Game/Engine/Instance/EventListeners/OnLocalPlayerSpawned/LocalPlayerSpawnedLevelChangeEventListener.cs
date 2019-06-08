using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Core;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class LocalPlayerSpawnedLevelChangeEventListener : HubChangedLocalPlayerSpawnedEventListener
	{
		/// <inheritdoc />
		public LocalPlayerSpawnedLevelChangeEventListener(ILocalPlayerSpawnedEventSubscribable subscriptionService, 
			IEntityDataChangeCallbackRegisterable entityDataCallbackRegister, 
			IReadonlyLocalPlayerDetails playerDetails,
			[KeyFilter(UnityUIRegisterationKey.PlayerUnitFrame)] IUIUnitFrame playerUnitFrame)
			: base(subscriptionService, entityDataCallbackRegister, playerDetails, playerUnitFrame)
		{
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
