using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using FreecraftCore;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class RegisterUnitFrameLevel : OnGroupJoinedEventListener
	{
		private IReadonlyEntityGuidMappable<IEntityDataFieldContainer> EntityDataMappable { get; }

		public RegisterUnitFrameLevel(IPlayerGroupJoinedEventSubscribable subscriptionService, 
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
			GroupUnitFrameManager.RegisterCallback<int>(joinArgs.PlayerGuid, (int)EUnitFields.UNIT_FIELD_LEVEL, RecaculateLevelUI);

			if(EntityDataMappable.ContainsKey(joinArgs.PlayerGuid))
			{
				//Very possible we don't know them
				//But if we do we should calculate their initial unitframe resources
				RecaculateLevelUI(joinArgs.PlayerGuid, new EntityDataChangedArgs<int>(0, 0));
			}
		}

		private void RecaculateLevelUI(ObjectGuid player, EntityDataChangedArgs<int> changeArgs)
		{
			GroupUnitFrameManager[player].UnitLevel.Text = EntityDataMappable[player].GetFieldValue<int>(EUnitFields.UNIT_FIELD_LEVEL).ToString();
		}
	}
}
