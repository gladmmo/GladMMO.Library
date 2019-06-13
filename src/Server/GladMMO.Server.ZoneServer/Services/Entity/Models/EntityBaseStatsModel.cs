using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class EntityBaseStatsModel
	{
		/// <summary>
		/// The base health value of the entity.
		/// </summary>
		public int BaseHealth { get; }

		/// <summary>
		/// The base mana of the entity.
		/// Could be 0 if the user has no mana potential.
		/// </summary>
		public int BaseMana { get; }

		public EntityBaseStatsModel(int baseHealth, int baseMana)
		{
			if (baseHealth < 0) throw new ArgumentOutOfRangeException(nameof(baseHealth));
			if (baseMana < 0) throw new ArgumentOutOfRangeException(nameof(baseMana));

			BaseHealth = baseHealth;
			BaseMana = baseMana;
		}
	}
}
