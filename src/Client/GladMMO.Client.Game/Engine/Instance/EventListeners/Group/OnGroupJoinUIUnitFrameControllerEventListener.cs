using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Features.AttributeFilters;
using Common.Logging;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.DefaultLobby)]
	public sealed class OnGroupJoinUIUnitFrameControllerEventListener : EventQueueBasedTickable<IPlayerGroupJoinedEventSubscribable, PlayerJoinedGroupEventArgs>
	{
		private IReadonlyEntityGuidMappable<IEntityDataFieldContainer> EntityDataMappable { get; }

		private IGroupUnitFrameManager GroupUnitframeManager { get; }

		/// <inheritdoc />
		public OnGroupJoinUIUnitFrameControllerEventListener(IPlayerGroupJoinedEventSubscribable subscriptionService,
			[NotNull] IReadonlyEntityGuidMappable<IEntityDataFieldContainer> entityDataMappable,
			[NotNull] ILog logger,
			[NotNull] IGroupUnitFrameManager groupUnitframeManager)
			: base(subscriptionService, true, logger)
		{
			EntityDataMappable = entityDataMappable ?? throw new ArgumentNullException(nameof(entityDataMappable));
			GroupUnitframeManager = groupUnitframeManager ?? throw new ArgumentNullException(nameof(groupUnitframeManager));
		}

		/// <inheritdoc />
		protected override void HandleEvent(PlayerJoinedGroupEventArgs args)
		{
			//TODO: We need to check if we have one available
			//Claim a unitframe.
			GroupUnitframeManager.TryClaimUnitFrame(args.PlayerGuid);

			//Even if we don't know them, we should register an event for it.
			GroupUnitframeManager.RegisterCallback<int>(args.PlayerGuid, (int)EUnitFields.UNIT_FIELD_HEALTH, OnCurrentHealthChangedValue);
			GroupUnitframeManager.RegisterCallback<int>(args.PlayerGuid, (int)EUnitFields.UNIT_FIELD_LEVEL, OnCurrentLevelChanged);

			//TODO: If we come to know them after group join, we'll need to register.
			if(!EntityDataMappable.ContainsKey(args.PlayerGuid))
			{
				if(Logger.IsDebugEnabled)
					Logger.Debug($"Encountered GroupJoin from far-away Entity: {args.PlayerGuid.RawGuidValue}");

				GroupUnitframeManager[args.PlayerGuid].SetElementActive(true);
				return;
			}
			else
			{
				//Very possible we don't know them
				//But if we do we should calculate their initial unitframe resources
				RecalulateHealthUI(args.PlayerGuid, EntityDataMappable.RetrieveEntity(args.PlayerGuid).GetFieldValue<int>((int)EUnitFields.UNIT_FIELD_HEALTH));
				RecaculateLevelUI(args.PlayerGuid, EntityDataMappable.RetrieveEntity(args.PlayerGuid).GetFieldValue<int>((int)EUnitFields.UNIT_FIELD_LEVEL));
				GroupUnitframeManager[args.PlayerGuid].SetElementActive(true);
			}
		}

		private void OnCurrentLevelChanged(NetworkEntityGuid entity, EntityDataChangedArgs<int> eventArgs)
		{
			RecaculateLevelUI(entity, eventArgs.NewValue);
		}

		private void RecaculateLevelUI(NetworkEntityGuid player, int currentLevel)
		{
			GroupUnitframeManager[player].UnitLevel.Text = currentLevel.ToString();
		}

		private void RecalulateHealthUI(NetworkEntityGuid player, int currentHealth)
		{
			float healthPercentage = (float)currentHealth / EntityDataMappable[player].GetFieldValue<int>((int)EUnitFields.UNIT_FIELD_MAXHEALTH);

			GroupUnitframeManager[player].HealthBar.BarFillable.FillAmount = healthPercentage;

			//Also we want to see the percentage text
			GroupUnitframeManager[player].HealthBar.BarText.Text = $"{currentHealth} / {EntityDataMappable.RetrieveEntity(player).GetFieldValue<int>((int)EUnitFields.UNIT_FIELD_MAXHEALTH)}";
		}

		private void OnCurrentHealthChangedValue(NetworkEntityGuid source, EntityDataChangedArgs<int> changeArgs)
		{
			RecalulateHealthUI(source, changeArgs.NewValue);
		}
	}
}
