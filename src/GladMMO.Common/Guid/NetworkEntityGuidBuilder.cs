using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Simple container for building GUIDs.
	/// </summary>
	public class ObjectGuidBuilder
	{
		/// <summary>
		/// Creates a new <see cref="ObjectGuidBuilder"/> object that allows
		/// for a more fluent building.
		/// </summary>
		/// <returns></returns>
		public static ObjectGuidBuilder New()
		{
			return new ObjectGuidBuilder();
		}

		/// <summary>
		/// The raw 64bit guid value.
		/// </summary>
		public ulong RawGuid { get; set; }

		/// <summary>
		/// Sets the <see cref="ObjectGuid.EntityId"/> to the provided <paramref name="id"/>.
		/// </summary>
		/// <param name="id">The ID for the entity.</param>
		/// <returns></returns>
		public ObjectGuidBuilder WithId(int id)
		{
			RawGuid = 0xFFFFFFFF00000000 & RawGuid; //remove current ID

			RawGuid |= ((ulong)(Int64)id); //bitwise or the ID into the raw guid value.

			return this;
		}

		public ObjectGuidBuilder WithType(EntityTypeId type)
		{
			RawGuid = 0x00FFFFFFFFFFFFFF & RawGuid; //remove current entity type.
			
			RawGuid |= (((ulong)(byte)BaseGuid.TypeIdToMask(type)) << 56);

			return this;
		}

		public ObjectGuidBuilder WithEntryId(int entryId)
		{
			RawGuid = 0xFF000000FFFFFFFF & RawGuid;

			RawGuid |= (((ulong)(int)entryId) << 32);

			return this;
		}

		/// <summary>
		/// Generates the <see cref="ObjectGuid"/> object.
		/// </summary>
		/// <returns>A non-null <see cref="ObjectGuid"/> with the <see cref="RawGuid"/>.</returns>
		public ObjectGuid Build()
		{
			return new ObjectGuid(RawGuid);
		}
	}
}