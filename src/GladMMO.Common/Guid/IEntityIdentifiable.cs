using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace GladMMO
{
	/// <summary>
	/// Contract for types that can indentify a Entity.
	/// </summary>
	public interface IEntityIdentifiable
	{
		/// <summary>
		/// Represents the type of the entity.
		/// </summary>
		EntityType EntityType { get; }

		/// <summary>
		/// Represents the unique entity integer indentifier.
		/// </summary>
		int EntityId { get; }
	}
}
