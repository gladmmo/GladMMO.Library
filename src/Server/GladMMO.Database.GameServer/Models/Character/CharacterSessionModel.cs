using System; using FreecraftCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GladMMO
{
	[Table("character_sessions")]
	public class CharacterSessionModel
	{
		//This should be manually registered as an alternate key
		[Required]
		[Key]
		[ForeignKey(nameof(CharacterEntry))]
		public int CharacterId { get; private set; }

		public CharacterEntryModel CharacterEntry { get; private set; }

		//This should be manually registered as an alternate key
		[Required]
		[ForeignKey(nameof(ZoneEntry))]
		public int ZoneId { get; private set; }

		//Navigation property
		public virtual ZoneInstanceEntryModel ZoneEntry { get; private set; }

		//Initially this will be false. Meaning there will be a short window of time
		//where users may request to claim a session multiple times. Meaning there could be
		//for example 5 clients may be claiming this session, be redirected to where it is it should be claimed
		//but the design should be that only one session is ever accepted. Only one can cause True to be set.
		//All others must be rejected.

		[Required]
		[Column(TypeName = "TIMESTAMP(6)")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public DateTime SessionCreationDate { get; private set; }

		[Required]
		[Column(TypeName = "TIMESTAMP(6)")]
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime SessionLastUpdateDate { get; private set; }

		/// <inheritdoc />
		public CharacterSessionModel(int characterId, int zoneId)
		{
			//We don't check this because tests might provide CLR default (update)
			//if(characterId < 0) throw new ArgumentOutOfRangeException(nameof(characterId));
			if(zoneId < 0) throw new ArgumentOutOfRangeException(nameof(zoneId));

			CharacterId = characterId;
			ZoneId = zoneId;
		}

		public CharacterSessionModel()
		{
			
		}
	}
}
