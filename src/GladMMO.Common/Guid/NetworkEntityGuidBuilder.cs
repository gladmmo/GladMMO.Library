using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Simple container for building GUIDs.
	/// </summary>
	public class NetworkEntityGuidBuilder
	{
		/// <summary>
		/// Creates a new <see cref="NetworkEntityGuidBuilder"/> object that allows
		/// for a more fluent building.
		/// </summary>
		/// <returns></returns>
		public static NetworkEntityGuidBuilder New()
		{
			return new NetworkEntityGuidBuilder();
		}

		/// <summary>
		/// The raw 64bit guid value.
		/// </summary>
		public ulong RawGuid { get; set; }

		/// <summary>
		/// Sets the <see cref="NetworkEntityGuid.EntityId"/> to the provided <paramref name="id"/>.
		/// </summary>
		/// <param name="id">The ID for the entity.</param>
		/// <returns></returns>
		public NetworkEntityGuidBuilder WithId(int id)
		{
			RawGuid = 0xFFFFFFFF00000000 & RawGuid; //remove current ID

			RawGuid |= ((ulong)(Int64)id); //bitwise or the ID into the raw guid value.

			return this;
		}

		public NetworkEntityGuidBuilder WithType(EntityType type)
		{
			RawGuid = 0x00FFFFFFFFFFFFFF & RawGuid; //remove current entity type.

			RawGuid |= (((ulong)(byte)type) << 56);

			return this;
		}

		public NetworkEntityGuidBuilder WithEntryId(int entryId)
		{
			RawGuid = 0xFF000000FFFFFFFF & RawGuid;

			RawGuid |= (((ulong)(int)entryId) << 32);

			return this;
		}

		/// <summary>
		/// Generates the <see cref="NetworkEntityGuid"/> object.
		/// </summary>
		/// <returns>A non-null <see cref="NetworkEntityGuid"/> with the <see cref="RawGuid"/>.</returns>
		public NetworkEntityGuid Build()
		{
			return new NetworkEntityGuid(RawGuid);
		}
	}
}