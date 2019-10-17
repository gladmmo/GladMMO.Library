using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Character data is data specific to gameplay
	/// such as experience, level, stats, class and etc.
	/// </summary>
	[Table("character_data")]
	public class CharacterDataModel
	{
		/// <summary>
		/// The ID of the character this entry is related to.
		/// </summary>
		[Key]
		[ForeignKey(nameof(Character))]
		public int CharacterId { get; private set; }

		/// <summary>
		/// Navigation property to the character table.
		/// </summary>
		public virtual CharacterEntryModel Character { get; private set; }

		/// <summary>
		/// The current experience of the player.
		/// </summary>
		public int ExperiencePoints { get; private set; }
		
		//TODO: We can have stuff like player class and other things here too.

		public CharacterDataModel(int characterId, int experiencePoints)
		{
			if (characterId <= 0) throw new ArgumentOutOfRangeException(nameof(characterId));
			if (experiencePoints < 0) throw new ArgumentOutOfRangeException(nameof(experiencePoints));

			CharacterId = characterId;
			ExperiencePoints = experiencePoints;
		}

		/// <summary>
		/// EF constructor.
		/// </summary>
		private CharacterDataModel()
		{

		}
	}
}
