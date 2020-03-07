using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IEntityExperienceLevelStrategy
	{
		/// <summary>
		/// Determines the entity level from the provided <see cref="experience"/> value.
		/// </summary>
		/// <param name="experience">The experience.</param>
		/// <returns>The level from the experience.</returns>
		int ComputeLevelFromExperience(int experience);

		/// <summary>
		/// Determines the total experience that is required for a given level.
		/// </summary>
		/// <param name="level">The level.</param>
		/// <returns>Total experience required to reach a specific level.</returns>
		int TotalExperienceRequiredForLevel(int level);
	}
}
