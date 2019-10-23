using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Database table model for friends.
	/// </summary>
	[Table("character_friends")]
	public class CharacterFriendModel
	{
		/// <summary>
		/// The ID of the character that has the friend.
		/// </summary>
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[ForeignKey(nameof(Character))]
		public int CharacterId { get; private set; }

		/// <summary>
		/// Navigation property to the character table.
		/// </summary>
		public virtual CharacterEntryModel Character { get; private set; }

		/// <summary>
		/// The ID of the friend character.
		/// </summary>
		[ForeignKey(nameof(FriendCharacter))]
		public int FriendCharacterId { get; private set; }

		/// <summary>
		/// Navigation property to the character table.
		/// </summary>
		public virtual CharacterEntryModel FriendCharacter { get; private set; }

		public CharacterFriendModel(int characterId, int friendCharacterId)
		{
			if (characterId <= 0) throw new ArgumentOutOfRangeException(nameof(characterId));
			if (friendCharacterId <= 0) throw new ArgumentOutOfRangeException(nameof(friendCharacterId));

			CharacterId = characterId;
			FriendCharacterId = friendCharacterId;
		}

		/// <summary>
		/// EF Ctor.
		/// </summary>
		protected CharacterFriendModel()
		{
			
		}
	}
}
