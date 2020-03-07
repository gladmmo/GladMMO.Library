using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Common.Logging;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class InitializeTargetLevelEventListener : LocalPlayerTargetChangedEventListener
	{
		private IReadonlyEntityGuidMappable<IEntityDataFieldContainer> EntityDataMappable { get; }

		private IEntityDataChangeCallbackRegisterable EntityDataChangeCallbackService { get; }

		public InitializeTargetLevelEventListener(ILocalPlayerTargetChangedEventListener subscriptionService,
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

			EntityDataChangeCallbackService.RegisterCallback<int>(args.TargetedEntity, (int)BaseObjectField.UNIT_FIELD_LEVEL, OnTargetEntityLevelChanged);

			//Only initialize if we have their values
			if (entityData.DataSetIndicationArray.Get((int)BaseObjectField.UNIT_FIELD_LEVEL))
				OnTargetEntityLevelChanged(args.TargetedEntity, new EntityDataChangedArgs<int>(0, entityData.GetFieldValue<int>(BaseObjectField.UNIT_FIELD_LEVEL)));
		}

		private void OnTargetEntityLevelChanged(ObjectGuid entity, EntityDataChangedArgs<int> args)
		{
			TargetUnitFrame.UnitLevel.Text = args.NewValue.ToString();
		}
	}
}
