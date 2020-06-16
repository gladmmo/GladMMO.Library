using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using FreecraftCore;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class RegisterUnitFrameHealth : OnGroupJoinedEventListener
	{
		private IReadonlyEntityGuidMappable<IEntityDataFieldContainer> EntityDataMappable { get; }

		public RegisterUnitFrameHealth(IPlayerGroupJoinedEventSubscribable subscriptionService, 
			ILog logger, 
			IGroupUnitFrameManager groupUnitFrameManager,
			[NotNull] IReadonlyEntityGuidMappable<IEntityDataFieldContainer> entityDataMappable) 
			: base(subscriptionService, logger, groupUnitFrameManager)
		{
			EntityDataMappable = entityDataMappable ?? throw new ArgumentNullException(nameof(entityDataMappable));
		}

		protected override void OnGroupJoined(PlayerJoinedGroupEventArgs joinArgs)
		{
			//Even if we don't know them, we should register an event for it.
			GroupUnitFrameManager.RegisterCallback<int>(joinArgs.PlayerGuid, (int)EUnitFields.UNIT_FIELD_HEALTH, RecalulateHealthUI);
			GroupUnitFrameManager.RegisterCallback<int>(joinArgs.PlayerGuid, (int)EUnitFields.UNIT_FIELD_MAXHEALTH, RecalulateHealthUI);

			if(EntityDataMappable.ContainsKey(joinArgs.PlayerGuid))
			{
				//Very possible we don't know them
				//But if we do we should calculate their initial unitframe resources
				RecalulateHealthUI(joinArgs.PlayerGuid, new EntityDataChangedArgs<int>(0, 0));
			}
		}

		private void RecalulateHealthUI(ObjectGuid player, EntityDataChangedArgs<int> changeArgs)
		{
			int currentHealth = EntityDataMappable[player].GetFieldValue<int>(EUnitFields.UNIT_FIELD_HEALTH);
			int maxHealth = EntityDataMappable[player].GetFieldValue<int>(EUnitFields.UNIT_FIELD_MAXHEALTH);

			float healthPercentage = (float)currentHealth / maxHealth;

			GroupUnitFrameManager[player].HealthBar.BarFillable.FillAmount = healthPercentage;
			GroupUnitFrameManager[player].HealthBar.BarText.Text = $"{currentHealth} / {maxHealth}";
		}
	}
}
