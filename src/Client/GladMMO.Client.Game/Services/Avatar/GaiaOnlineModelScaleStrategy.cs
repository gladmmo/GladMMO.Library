using System;
using System.Collections.Generic;
using System.Text;
using FreecraftCore;

namespace GladMMO
{
	public sealed class GaiaOnlineModelScaleStrategy : DefaultModelScaleStrategy
	{
		public GaiaOnlineModelScaleStrategy(IClientDataCollectionContainer clientData, 
			IReadonlyEntityGuidMappable<IEntityDataFieldContainer> entityFieldDataMappable) 
			: base(clientData, entityFieldDataMappable)
		{

		}

		protected override float CalculateAdditiveUnitFieldScale(ObjectGuid guid, IEntityDataFieldContainer fieldContainer)
		{
			float value = base.CalculateAdditiveUnitFieldScale(guid, fieldContainer);

			//-1 from normal scale is accurate scale for Gaia Online. When 2 is just TOO big.
			if (guid.TypeId == EntityTypeId.TYPEID_PLAYER)
				return value - 1.0f;
			else
				return value;
		}
	}
}
