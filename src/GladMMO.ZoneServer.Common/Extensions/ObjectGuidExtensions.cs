using System;
using System.Collections.Generic;
using System.Text;
using FreecraftCore;

namespace GladMMO
{
	public static class ObjectGuidExtensions
	{
		public static bool IsWorldObject([NotNull] this BaseGuid guid)
		{
			if (guid == null) throw new ArgumentNullException(nameof(guid));

			switch (guid.TypeId)
			{
				case EntityTypeId.TYPEID_OBJECT:
				case EntityTypeId.TYPEID_ITEM:
				case EntityTypeId.TYPEID_CONTAINER:
					return false;
				case EntityTypeId.TYPEID_UNIT:
				case EntityTypeId.TYPEID_PLAYER:
				case EntityTypeId.TYPEID_GAMEOBJECT:
				case EntityTypeId.TYPEID_DYNAMICOBJECT:
				case EntityTypeId.TYPEID_CORPSE:
					return true;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}
