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
		[Column(Order = 1)]
		[Key]
		[Range(0, long.MaxValue)]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CreatureTemplateId { get; private set; }

		//TODO: Should we like tables somehow with auth? Probably not?
		/// <summary>
		/// Key for the account associated with the creation/registeration of this creature template.
		/// </summary>
		[Column(Order = 2)]
		[Required]
		[Range(0, int.MaxValue)]
		public int AccountId { get; private set; }

		/// <summary>
		/// The primary unique 64bit integer key used for the
		/// creature model's unique ID.
		/// </summary>
		[Column(Order = 3)]
		[ForeignKey(nameof(CreatureModelEntry))]
		[Range(0, long.MaxValue)]
		public long ModelId { get; private set; }

		//Navigation property
		public virtual CreatureModelEntryModel CreatureModelEntry { get; private set; }

		/// <summary>
		/// The name of the Creature. Will be the one the client sees, the one the NameQuery will return.
		/// </summary>
		[Column(Order = 4)]
		[Required]
		public string CreatureName { get; private set; }

		//Min/Max level based on TrinityCore.
		/// <summary>
		/// The minimum level of the creature if the creature has a level range.
		/// </summary>
		[Column(Order = 5)]
		[Required]
		[Range(1, 255)]
		public int MinimumLevel { get; private set; }

		/// <summary>
		/// The maximum level of the creature if the creature has a level range. When added to world, a level in chosen in the specified level range.
		/// </summary>
		[Column(Order = 6)]
		[Required]
		[Range(1, 255)]
		public int MaximumLevel { get; private set; }

		public CreatureTemplateEntryModel(int accountId, long modelId, [NotNull] string creatureName, int minimumLevel, int maximumLevel)
		{
			if (accountId <= 0) throw new ArgumentOutOfRangeException(nameof(accountId));
			if (modelId <= 0) throw new ArgumentOutOfRangeException(nameof(modelId));
			if (string.IsNullOrWhiteSpace(creatureName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(creatureName));
			if (minimumLevel <= 0) throw new ArgumentOutOfRangeException(nameof(minimumLevel));
			if (maximumLevel <= 0) throw new ArgumentOutOfRangeException(nameof(maximumLevel));

			AccountId = accountId;
			ModelId = modelId;
			CreatureName = creatureName;
			MinimumLevel = minimumLevel;
			MaximumLevel = maximumLevel;
		}

		protected CreatureTemplateEntryModel()
		{

		}
	}
}
