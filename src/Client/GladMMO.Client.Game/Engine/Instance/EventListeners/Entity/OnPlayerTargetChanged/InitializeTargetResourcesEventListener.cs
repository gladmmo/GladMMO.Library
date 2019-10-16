using System;
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

			//Listen for both max and current health.
			EntityDataChangeCallbackService.RegisterCallback<int>(args.TargetedEntity, (int)EntityObjectField.UNIT_FIELD_HEALTH, OnTargetEntityHealthChanged);
			EntityDataChangeCallbackService.RegisterCallback<int>(args.TargetedEntity, (int)EntityObjectField.UNIT_FIELD_MAXHEALTH, OnTargetEntityHealthChanged);
		}

		private void OnTargetEntityHealthChanged(NetworkEntityGuid entity, EntityDataChangedArgs<int> args)
		{
			IEntityDataFieldContainer entityData = EntityDataMappable.RetrieveEntity(entity);

			//Ignore the changed value.
			int health = entityData.GetFieldValue<int>(EntityObjectField.UNIT_FIELD_HEALTH);
			int maxHealth = entityData.GetFieldValue<int>(EntityObjectField.UNIT_FIELD_MAXHEALTH);

			//TODO: This is just a test value.
			health /= 2;

			TargetUnitFrame.HealthBar.BarFillable.FillAmount = (float)health / maxHealth;
		}
	}
}
