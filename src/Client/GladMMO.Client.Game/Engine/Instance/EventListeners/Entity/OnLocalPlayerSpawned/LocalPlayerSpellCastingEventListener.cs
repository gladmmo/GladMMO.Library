using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Autofac.Core;
using Autofac.Features.AttributeFilters;
using Common.Logging;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	[AdditionalRegisterationAs(typeof(ILocalPlayerSpellCastingStateChangedEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class LocalPlayerSpellCastingEventListener : DataChangedLocalPlayerSpawnedEventListener, ILocalPlayerSpellCastingStateChangedEventSubscribable
	{
		public event EventHandler<SpellCastingStateChangedEventArgs> OnSpellCastingStateChanged;

		/// <inheritdoc />
		public LocalPlayerSpellCastingEventListener(ILocalPlayerSpawnedEventSubscribable subscriptionService, 
			IEntityDataChangeCallbackRegisterable entityDataCallbackRegister, 
			IReadonlyLocalPlayerDetails playerDetails)
			: base(subscriptionService, entityDataCallbackRegister, playerDetails)
		{

		}

		/// <inheritdoc />
		protected override void OnLocalPlayerSpawned(LocalPlayerSpawnedEventArgs args)
		{
			Debug.LogError($"CASTBAR IS NOT REIMPLEMENTED DUE TO WOW NOT HAVING UNIT_FIELD_CASTING_SPELLID");
			//RegisterPlayerDataChangeCallback<int>(EUnitFields.UNIT_FIELD_CASTING_SPELLID, OnSpellCastingIdChanged);
		}

		private void OnSpellCastingIdChanged(ObjectGuid entity, EntityDataChangedArgs<int> changeArgs)
		{
			//OnSpellCastingStateChanged?.Invoke(this, new SpellCastingStateChangedEventArgs(changeArgs.NewValue, PlayerDetails.EntityData.GetFieldValue<long>(EUnitFields.UNIT_FIELD_CASTING_STARTTIME)));
		}
	}
}
