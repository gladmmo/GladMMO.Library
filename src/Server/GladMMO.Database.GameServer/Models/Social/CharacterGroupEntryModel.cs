using System; using FreecraftCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GladMMO
{
	[Table("group_entry")]
	public class CharacterGroupEntryModel
	{
		/// <summary>
		/// The id of the group.
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int GroupId { get; private set; }

		/// <summary>
		/// The ID of the character of the leader.
		/// Should also match the id of the entry for <see cref="CharacterGroupMembershipModel"/>.
		/// </summary>
		[ForeignKey(nameof(LeaderCharacter))]
		public int LeaderCharacterId { get; private set; }

		/// <summary>
		/// Navigation property the character leader's group membership entry.
		/// </summary>
		public virtual CharacterEntryModel LeaderCharacter { get; private set; }

		/// <summary>
		/// The exact time that the character joined the guild.
		/// </summary>
		[Required]
		[Column(TypeName = "TIMESTAMP(6)")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public DateTime JoinDate { get; private set; }

		//TODO: A group entry should have some details associated with it such as 5man or maybe raid group or something

		/// <summary>
		/// Creates a new group entry.
		/// With the leader as the <see cref="LeaderCharacterId"/>.
		/// </summary>
		/// <param name="leaderCharacterId">The character id of the leader.</param>
		public CharacterGroupEntryModel(int leaderCharacterId)
		{
			LeaderCharacterId = leaderCharacterId;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		private CharacterGroupEntryModel()
		{
			
		}
	}
}
