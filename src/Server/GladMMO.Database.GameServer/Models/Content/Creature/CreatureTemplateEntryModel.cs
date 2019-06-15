using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GladMMO
{
	//Conceptually based on TrinityCore's (WoW's): https://trinitycore.atlassian.net/wiki/spaces/tc/pages/2130008/creature+template
	[Table("creature_template")]
	public class CreatureTemplateEntryModel
	{
		/// <summary>
		/// The id of the creature template.
		/// </summary>
		[Key]
		[Range(0, long.MaxValue)]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CreatureTemplateId { get; private set; }

		//TODO: Should we like tables somehow with auth? Probably not?
		/// <summary>
		/// Key for the account associated with the creation/registeration of this creature template.
		/// </summary>
		[Required]
		[Range(0, int.MaxValue)]
		public int AccountId { get; private set; }

		/// <summary>
		/// The primary unique 64bit integer key used for the
		/// creature model's unique ID.
		/// </summary>
		[ForeignKey(nameof(CreatureModelEntry))]
		[Range(0, long.MaxValue)]
		public long ModelId { get; private set; }

		//Navigation property
		public virtual CreatureModelEntryModel CreatureModelEntry { get; private set; }

		public CreatureTemplateEntryModel(int accountId, long modelId)
		{
			if (accountId <= 0) throw new ArgumentOutOfRangeException(nameof(accountId));
			if (modelId <= 0) throw new ArgumentOutOfRangeException(nameof(modelId));

			AccountId = accountId;
			ModelId = modelId;
		}

		protected CreatureTemplateEntryModel(long modelId)
		{
			ModelId = modelId;
		}
	}
}
