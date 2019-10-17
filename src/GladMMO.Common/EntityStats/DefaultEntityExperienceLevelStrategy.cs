using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class DefaultEntityExperienceLevelStrategy : IEntityExperienceLevelStrategy
	{
		public int ComputeLevelFromExperience(int experience)
		{
			if (experience < 0) throw new ArgumentOutOfRangeException(nameof(experience));

			if (experience < 400)
				return 1;
			if (experience >= 400)
				return 2;
			else if (experience >= 1300)
				return 3;

			throw new NotImplementedException($"TODO: Haven't implemented full level/experience system.");
		}

		public int TotalExperienceRequiredForLevel(int level)
		{
			if (level < 0) throw new ArgumentOutOfRangeException(nameof(level));

			switch (level)
			{
				case 1:
					return 0;
				case 2:
					return 400;
				case 3:
					return 1300;
			}

			throw new NotImplementedException($"TODO: Haven't implemented full level/experience system.");
		}
	}
}
