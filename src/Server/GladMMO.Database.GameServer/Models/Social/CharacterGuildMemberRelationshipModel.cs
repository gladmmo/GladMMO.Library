using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GladMMO
{
	[Table("guild_charactermember")]
	public class CharacterGuildMemberRelationshipModel
	{
		//A character should only EVER be in one guild. So it can be the primary key for the table.
		/// <summary>
		/// The ID of the character this entry is related to.
		/// </summary>
		[Key]
		[ForeignKey(nameof(Character))]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int CharacterId { get; private set; }

		/// <summary>
		/// Navigation property to the character table.
		/// </summary>
		public virtual CharacterEntryModel Character { get; private set; }

		/// <summary>
		/// The ID of the guild the character is apart of.
		/// </summary>
		[ForeignKey(nameof(Guild))]
		public int GuildId { get; private set; }

		public virtual GuildEntryModel Guild { get; private set; }

		/// <summary>
		/// The exact time that the character joined the guild.
		/// </summary>
		[Required]
		[Column(TypeName = "TIMESTAMP(6)")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public DateTime JoinDate { get; private set; }

		/// <inheritdoc />
		public CharacterGuildMemberRelationshipModel(int characterId, int guildId)
		{
			if(characterId <= 0) throw new ArgumentOutOfRangeException(nameof(characterId));
			if(guildId <= 0) throw new ArgumentOutOfRangeException(nameof(guildId));

			CharacterId = characterId;
			GuildId = guildId;
		}

		private CharacterGuildMemberRelationshipModel()
		{
			
		}
	}
}
