using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	//This is basically a strategy and a factory rolled into one.
	public sealed class EntityBaseStatsDataFactory : IFactoryCreatable<EntityBaseStatsModel, EntityDataStatsDerivable>
	{
		public EntityBaseStatsModel Create(EntityDataStatsDerivable context)
		{
			if(context.EntityType != EntityType.Player)
				throw new NotImplementedException($"TODO: Implement base stats for creatures.");

			//TODO: Add base mana calculation
			return new EntityBaseStatsModel(CaclulateBaseHealthFromLevel(context.Level), 0);
		}

		public static int CaclulateBaseHealthFromLevel(int level)
		{
			if (level <= 0 || level > 60) throw new ArgumentOutOfRangeException(nameof(level), $"Level: {level} must be greater than 0 and less than or equal to 60.");

			//Base on CLASS_WARRIOR
			//Best fit 3rd order polynomial of base health calculation from 1-60 from another RPG.
			//Derived from known data set.
			//=16.8 + (7.68 * C74) +(-0.0638 * (C74 * C74)) + (0.00668 * (C74 * C74 * C74))
			return (int)(16.8f + 7.68 * level + -0.0638f * level * level + 0.00668 * level * level * level);
		}
	}
}
