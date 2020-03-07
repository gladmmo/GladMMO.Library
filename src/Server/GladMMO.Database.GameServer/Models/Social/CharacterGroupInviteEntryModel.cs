using System; using FreecraftCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// The table model is for persisting group invitations
	/// to characters.
	/// </summary>
	[Table("group_invites")]
	public class CharacterGroupInviteEntryModel
	{
		//Don't let the database generate this
		/// <summary>
		/// The invited character's id.
		/// </summary>
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

		/// <summary>
		/// The time at which an invitation to the group will expire.
		/// This prevents claiming it long after an invite should be over.
		/// </summary>
		[Required]
		[Column(TypeName = "TIMESTAMP(6)")]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public DateTime InviteExpirationTime { get; private set; }

		/// <inheritdoc />
		public CharacterGroupInviteEntryModel(int characterId, int groupId, DateTime inviteExpirationTime)
		{
			if(characterId <= 0) throw new ArgumentOutOfRangeException(nameof(characterId));
			if(groupId <= 0) throw new ArgumentOutOfRangeException(nameof(groupId));

			CharacterId = characterId;
			GroupId = groupId;
			InviteExpirationTime = inviteExpirationTime;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		private CharacterGroupInviteEntryModel()
		{
			
		}
	}
}
