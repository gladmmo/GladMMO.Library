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

			RegisterLevelCallback(args);

			//Only initialize if we have their values
			if (CheckLevelSet(args.TargetedEntity, entityData))
				OnTargetEntityLevelChanged(args.TargetedEntity, new EntityDataChangedArgs<int>(0, GetLevel(args.TargetedEntity, entityData)));
		}

		private static int GetLevel(ObjectGuid targetGuid, IEntityDataFieldContainer entityData)
		{
			if (targetGuid.TypeId == EntityTypeId.TYPEID_GAMEOBJECT)
				return entityData.GetFieldValue<int>(EGameObjectFields.GAMEOBJECT_LEVEL);
			else
				return entityData.GetFieldValue<int>(EUnitFields.UNIT_FIELD_LEVEL);
		}

		private static bool CheckLevelSet([NotNull] ObjectGuid targetGuid, [NotNull] IEntityDataFieldContainer entityData)
		{
			if (targetGuid == null) throw new ArgumentNullException(nameof(targetGuid));
			if (entityData == null) throw new ArgumentNullException(nameof(entityData));

			if (targetGuid.TypeId == EntityTypeId.TYPEID_GAMEOBJECT)
				return entityData.DataSetIndicationArray.Get((int)EGameObjectFields.GAMEOBJECT_LEVEL);
			else
				return entityData.DataSetIndicationArray.Get((int)EUnitFields.UNIT_FIELD_LEVEL);
		}

		private void RegisterLevelCallback(LocalPlayerTargetChangedEventArgs args)
		{
			//Gamobject level is very different.
			if (args.TargetedEntity.TypeId == EntityTypeId.TYPEID_GAMEOBJECT)
			{
				EntityDataChangeCallbackService.RegisterCallback<int>(args.TargetedEntity, (int)EGameObjectFields.GAMEOBJECT_LEVEL, OnTargetEntityLevelChanged);
			}
			else
			{
				EntityDataChangeCallbackService.RegisterCallback<int>(args.TargetedEntity, (int)EUnitFields.UNIT_FIELD_LEVEL, OnTargetEntityLevelChanged);
			}
		}

		private void OnTargetEntityLevelChanged(ObjectGuid entity, EntityDataChangedArgs<int> args)
		{
			TargetUnitFrame.UnitLevel.Text = args.NewValue.ToString();
		}
	}
}
