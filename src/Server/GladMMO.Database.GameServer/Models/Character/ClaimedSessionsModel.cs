using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GladMMO
{
	[Table("claimed_sessions")]
	public class ClaimedSessionsModel
	{
		[Key]
		[Required]
		[ForeignKey(nameof(Session))]
		public int CharacterId { get; private set; }

		/// <summary>
		/// Navigation property to the linked session
		/// </summary>
		public virtual CharacterSessionModel Session { get; private set; }

		[Required]
		[Column(TypeName = "TIMESTAMP(6)")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public DateTime SessionCreationDate { get; private set; }

		/// <inheritdoc />
		public ClaimedSessionsModel(int characterId)
		{
			if(characterId < 0) throw new ArgumentOutOfRangeException(nameof(characterId));

			CharacterId = characterId;
		}

		public ClaimedSessionsModel()
		{
			
		}
	}
}
