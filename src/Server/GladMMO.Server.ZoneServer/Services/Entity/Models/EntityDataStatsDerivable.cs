using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Type that describes relevant information
	/// about an entity that can be used
	/// to derive stats information/data from.
	/// </summary>
	public sealed class EntityDataStatsDerivable
	{
		/// <summary>
		/// The type of the entity.
		/// </summary>
		public EntityType EntityType { get; }

		//TODO: Add class here

		/// <summary>
		/// The level of the entity.
		/// </summary>
		public int Level { get; }

		public EntityDataStatsDerivable(EntityType entityType, int level)
		{
			if(!Enum.IsDefined(typeof(EntityType), entityType)) throw new InvalidEnumArgumentException(nameof(entityType), (int)entityType, typeof(EntityType));

			EntityType = entityType;
			Level = level;
		}
	}
}
