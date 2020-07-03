using System;
using FreecraftCore;

namespace GladMMO
{
	public class DefaultModelScaleStrategy : IModelScaleStrategy
	{
		private IClientDataCollectionContainer ClientData { get; }

		private IReadonlyEntityGuidMappable<IEntityDataFieldContainer> EntityFieldDataMappable { get; }

		public DefaultModelScaleStrategy([NotNull] IClientDataCollectionContainer clientData,
			[NotNull] IReadonlyEntityGuidMappable<IEntityDataFieldContainer> entityFieldDataMappable)
		{
			ClientData = clientData ?? throw new ArgumentNullException(nameof(clientData));
			EntityFieldDataMappable = entityFieldDataMappable ?? throw new ArgumentNullException(nameof(entityFieldDataMappable));
		}

		public float ComputeScale([NotNull] ObjectGuid guid)
		{
			if (guid == null) throw new ArgumentNullException(nameof(guid));

			float staticModelScale = ModelScaleFromGDBC(guid);

			IEntityDataFieldContainer fieldContainer = EntityFieldDataMappable.RetrieveEntity(guid);

			if (!fieldContainer.DataSetIndicationArray.Get((int) EObjectFields.OBJECT_FIELD_SCALE_X))
				return staticModelScale;

			return staticModelScale + CalculateAdditiveUnitFieldScale(guid, fieldContainer);
		}

		protected virtual float CalculateAdditiveUnitFieldScale(ObjectGuid guid, IEntityDataFieldContainer fieldContainer)
		{
			return fieldContainer.GetFieldValue<float>(EObjectFields.OBJECT_FIELD_SCALE_X);
		}

		//TrinityCore:
		/*    int32 scaleAuras = GetTotalAuraModifier(SPELL_AURA_MOD_SCALE) + GetTotalAuraModifier(SPELL_AURA_MOD_SCALE_2);
		float scale = GetNativeObjectScale() + CalculatePct(1.0f, scaleAuras);
		float scaleMin = GetTypeId() == TYPEID_PLAYER ? 0.1 : 0.01;
		SetObjectScale(std::max(scale, scaleMin));*/

		private float ModelScaleFromGDBC([NotNull] ObjectGuid guid)
		{
			if (guid == null) throw new ArgumentNullException(nameof(guid));

			//TODO: Implement players and gameobjects for scaling.
			if (guid.TypeId != EntityTypeId.TYPEID_GAMEOBJECT)
				return CalculateUnitScale(guid);
			else
				return CalculateGameObjectScale(guid);

		}

		private float CalculateGameObjectScale([NotNull] ObjectGuid guid)
		{
			if (guid == null) throw new ArgumentNullException(nameof(guid));

			IEntityDataFieldContainer fieldContainer = EntityFieldDataMappable.RetrieveEntity(guid);

			if(!fieldContainer.DataSetIndicationArray.Get((int)EGameObjectFields.GAMEOBJECT_DISPLAYID))
				return 1.0f;

			int displayId = fieldContainer.GetFieldValue<int>(EGameObjectFields.GAMEOBJECT_DISPLAYID);

			//GameObjects don't have model scale clientside
			//var displayInfo = ClientData.AssertEntry<GameObjectDisplayInfoEntry<string>>(displayId);
			return 1.0f;
		}

		private float CalculateUnitScale(ObjectGuid guid)
		{
			IEntityDataFieldContainer fieldContainer = EntityFieldDataMappable.RetrieveEntity(guid);

			if (!fieldContainer.DataSetIndicationArray.Get((int) EUnitFields.UNIT_FIELD_DISPLAYID))
				return 1.0f;

			int displayId = fieldContainer.GetFieldValue<int>(EUnitFields.UNIT_FIELD_DISPLAYID);

			var displayInfo = ClientData.AssertEntry<CreatureDisplayInfoEntry<string>>(displayId);
			var modelData = ClientData.AssertEntry<CreatureModelDataEntry<string>>(displayInfo.ModelId);

			return displayInfo.CreatureModelScale * modelData.ModelScale;
		}
	}
}