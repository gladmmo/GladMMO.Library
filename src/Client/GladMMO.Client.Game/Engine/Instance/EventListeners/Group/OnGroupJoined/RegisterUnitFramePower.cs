using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using FreecraftCore;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class RegisterUnitFramePower : OnGroupJoinedEventListener
	{
		private IReadonlyEntityGuidMappable<IEntityDataFieldContainer> EntityDataMappable { get; }

		public RegisterUnitFramePower(IPlayerGroupJoinedEventSubscribable subscriptionService, 
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
			GroupUnitFrameManager.RegisterCallback<int>(joinArgs.PlayerGuid, (int)EUnitFields.UNIT_FIELD_POWER1, RecalulatePowerUI);
			GroupUnitFrameManager.RegisterCallback<int>(joinArgs.PlayerGuid, (int)EUnitFields.UNIT_FIELD_MAXPOWER1, RecalulatePowerUI);

			if(EntityDataMappable.ContainsKey(joinArgs.PlayerGuid))
			{
				//Very possible we don't know them
				//But if we do we should calculate their initial unitframe resources
				RecalulatePowerUI(joinArgs.PlayerGuid, new EntityDataChangedArgs<int>(0, 0));
			}
		}

		private void RecalulatePowerUI(ObjectGuid player, EntityDataChangedArgs<int> changeArgs)
		{
			int currentPower = EntityDataMappable[player].GetFieldValue<int>(EUnitFields.UNIT_FIELD_POWER1);
			int maxPower = EntityDataMappable[player].GetFieldValue<int>(EUnitFields.UNIT_FIELD_MAXPOWER1);

			float powerPercentage = (float) currentPower / maxPower;

			GroupUnitFrameManager[player].TechniquePointsBar.BarFillable.FillAmount = powerPercentage;
			GroupUnitFrameManager[player].TechniquePointsBar.BarText.Text = $"{currentPower} / {maxPower}";
		}
	}
}
