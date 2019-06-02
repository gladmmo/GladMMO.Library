using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Group membership model for characters.
	/// </summary>
	[Table("group_members")]
	public class CharacterGroupMembershipModel
	{
		//Don't let the database generate this
		[Key]
		[ForeignKey(nameof(Character))]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int CharacterId { get; private set; }
		
		/// <summary>
		/// Navigation property to the character this entry is for.
		/// </summary>
		public virtual CharacterEntryModel Character { get; private set; }

		//This is not unique.
		/// <summary>
		/// The ID of the group the <see cref="Character"/> is associated with.
		/// </summary>
		[ForeignKey(nameof(Group))]
		public int GroupId { get; private set; }

		/// <summary>
		/// Navigation property to the group the <see cref="Character"/> is apart of.
		/// </summary>
		public virtual GuildEntryModel Group { get; private set; }

		/// <inheritdoc />
		public CharacterGroupMembershipModel(int characterId, int groupId)
		{
			if(characterId <= 0) throw new ArgumentOutOfRangeException(nameof(characterId));
			if(groupId <= 0) throw new ArgumentOutOfRangeException(nameof(groupId));

			CharacterId = characterId;
			GroupId = groupId;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		private CharacterGroupMembershipModel()
		{
			
		}
	}
}
