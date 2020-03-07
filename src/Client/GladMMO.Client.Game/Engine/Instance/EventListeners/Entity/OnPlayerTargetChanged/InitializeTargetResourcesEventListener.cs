using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Common.Logging;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class InitializeTargetResourcesEventListener : LocalPlayerTargetChangedEventListener
	{
		private IReadonlyEntityGuidMappable<IEntityDataFieldContainer> EntityDataMappable { get; }

		private IEntityDataChangeCallbackRegisterable EntityDataChangeCallbackService { get; }

		private List<IEntityDataEventUnregisterable> Unregisterables { get; } = new List<IEntityDataEventUnregisterable>(2);

		public InitializeTargetResourcesEventListener(ILocalPlayerTargetChangedEventListener subscriptionService,
			ILog logger,
			[KeyFilter(UnityUIRegisterationKey.TargetUnitFrame)] IUIUnitFrame targetUnitFrame,
			[NotNull] IReadonlyEntityGuidMappable<IEntityDataFieldContainer> entityDataMappable,
			[NotNull] IEntityDataChangeCallbackRegisterable entityDataChangeCallbackService) 
			: base(subscriptionService, logger, targetUnitFrame)
		{
			EntityDataMappable = entityDataMappable ?? throw new ArgumentNullException(nameof(entityDataMappable));
			EntityDataChangeCallbackService = entityDataChangeCallbackService ?? throw new ArgumentNullException(nameof(entityDataChangeCallbackService));
		}

		protected override void OnLocalPlayerTargetChanged(LocalPlayerTargetChangedEventArgs args)
		{
			IEntityDataFieldContainer entityData = EntityDataMappable.RetrieveEntity(args.TargetedEntity);

			foreach(var unreg in Unregisterables)
				unreg.Unregister();

			Unregisterables.Clear();

			//Listen for both max and current health.
			Unregisterables.Add(EntityDataChangeCallbackService.RegisterCallback<int>(args.TargetedEntity, (int)EUnitFields.UNIT_FIELD_HEALTH, OnTargetEntityHealthChanged));
			Unregisterables.Add(EntityDataChangeCallbackService.RegisterCallback<int>(args.TargetedEntity, (int)EUnitFields.UNIT_FIELD_MAXHEALTH, OnTargetEntityHealthChanged));

			//Only initialize if we have their values
			if (entityData.DataSetIndicationArray.Get((int) EUnitFields.UNIT_FIELD_HEALTH))
				OnTargetEntityHealthChanged(args.TargetedEntity, new EntityDataChangedArgs<int>(0, 0));
		}

		private void OnTargetEntityHealthChanged(ObjectGuid entity, EntityDataChangedArgs<int> args)
		{
			IEntityDataFieldContainer entityData = EntityDataMappable.RetrieveEntity(entity);

			//Ignore the changed value.
			int health = entityData.GetFieldValue<int>(EUnitFields.UNIT_FIELD_HEALTH);
			int maxHealth = entityData.GetFieldValue<int>(EUnitFields.UNIT_FIELD_MAXHEALTH);

			TargetUnitFrame.HealthBar.BarText.Text = $"{health} / {maxHealth}";
			TargetUnitFrame.HealthBar.BarFillable.FillAmount = (float)health / maxHealth;
		}
	}
}
