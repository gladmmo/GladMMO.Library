using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GladMMO
{
	[Table("character_appearance")]
	public class CharacterAppearanceModel
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

		[Key]
		[ForeignKey(nameof(AvatarModel))]
		public int AvatarModelId { get; private set; }

		/// <summary>
		/// Navigation property to the avatar model entry table.
		/// </summary>
		public virtual AvatarEntryModel AvatarModel { get; private set; }

		public CharacterAppearanceModel(int characterId, int avatarModelId)
		{
			if (characterId <= 0) throw new ArgumentOutOfRangeException(nameof(characterId));
			if (avatarModelId <= 0) throw new ArgumentOutOfRangeException(nameof(avatarModelId));

			CharacterId = characterId;
			AvatarModelId = avatarModelId;
		}
	}
}
