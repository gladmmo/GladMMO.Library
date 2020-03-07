using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	//For anyone who looks here. I am not proud of this code. Sometimes, there is no way to
	//elegantly express something.
	//"Use a formula!". I based this leveling system on an existing one.
	//There is no formula.
	//Well, 46959e^0.0732x is close but not realistic to use. It's not that accurate either.
	public sealed class DefaultEntityExperienceLevelStrategy : IEntityExperienceLevelStrategy
	{
		public int ComputeLevelFromExperience(int experience)
		{
			if (experience < 0) throw new ArgumentOutOfRangeException(nameof(experience));

			if (experience >= 35200)
				return 11;

			if (experience >= 27600)
				return 10;

			if (experience >= 21100)
				return 9;

			if (experience >= 15700)
				return 8;

			if (experience >= 11200)
				return 7;

			if (experience >= 7600)
				return 6;

			if (experience >= 4800)
				return 5;

			if (experience >= 2700)
				return 4;

			if (experience >= 1300)
				return 3;

			if (experience >= 400)
				return 2;

			if (experience < 400)
				return 1;

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
				case 4:
					return 2700;
				case 5:
					return 4800;
				case 6:
					return 7600;
				case 7:
					return 11200;
				case 8:
					return 15700;
				case 9:
					return 21100;
				case 10:
					return 27600;
				case 11:
					return 35200;
			}

			throw new NotImplementedException($"TODO: Haven't implemented full level/experience system.");
		}
	}
}
