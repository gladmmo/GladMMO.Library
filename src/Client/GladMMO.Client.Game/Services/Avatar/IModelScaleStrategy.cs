using System;
using System.Collections.Generic;
using System.Text;
using FreecraftCore;

namespace GladMMO
{
	/// <summary>
	/// Contract for type that can compute an object's scale.
	/// </summary>
	public interface IModelScaleStrategy
	{
		float ComputeScale(ObjectGuid guid);
	}

	public sealed class DefaultModelScaleStrategy : IModelScaleStrategy
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

			return staticModelScale + fieldContainer.GetFieldValue<float>(EObjectFields.OBJECT_FIELD_SCALE_X);
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
			if (guid.TypeId != EntityTypeId.TYPEID_UNIT)
				return 1.0f;

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
