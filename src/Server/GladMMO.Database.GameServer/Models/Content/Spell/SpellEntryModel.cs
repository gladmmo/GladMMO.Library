using System; using FreecraftCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GladMMO
{
	[Table("spell_entry")]
	public class SpellEntryModel : ISpellDefinition, IContentIdentifiable
	{
		public int ContentId => SpellId;

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int SpellId { get; private set; }

		//We don't make this unique because users may want to have
		//spells of differing ids but with the same name, they might be different spells.
		[Required]
		public string SpellName { get; private set; }

		/// <summary>
		/// Indicates the type of spell: Spell, melee or ranged.
		/// </summary>
		[Required]
		public SpellClassType SpellType { get; private set; }

		/// <summary>
		/// Indicates if the spell definition is a default game spell
		/// or if it's a custom user spell.
		/// </summary>
		[Required]
		public bool isDefault { get; private set; }

		/// <summary>
		/// Cast time in milliseconds.
		/// </summary>
		[Required]
		public int CastTime { get; private set; }

		[Required]
		[ForeignKey(nameof(SpellIcon))]
		public int SpellIconId { get; private set; }

		public virtual ContentIconEntryModel SpellIcon { get; private set; }

		/// <summary>
		/// The ID of the first spell effect.
		/// </summary>
		[Required]
		[ForeignKey(nameof(SpellEffectOne))]
		public int SpellEffectIdOne { get; private set; }

		//Navigation property
		/// <summary>
		/// The first spell effect navigation property.
		/// </summary>
		public virtual SpellEffectEntryModel SpellEffectOne { get; private set; }

		public SpellEntryModel([JetBrains.Annotations.NotNull] string spellName, SpellClassType spellType, bool isDefault, int castTime, int spellEffectIdOne, int spellIconId)
		{
			if (string.IsNullOrWhiteSpace(spellName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(spellName));
			if (spellEffectIdOne < 0) throw new ArgumentOutOfRangeException(nameof(spellEffectIdOne));
			if (castTime < 0) throw new ArgumentOutOfRangeException(nameof(castTime));
			if (!Enum.IsDefined(typeof(SpellClassType), spellType)) throw new InvalidEnumArgumentException(nameof(spellType), (int) spellType, typeof(SpellClassType));
			if (spellIconId <= 0) throw new ArgumentOutOfRangeException(nameof(spellIconId));

			SpellName = spellName;
			SpellType = spellType;
			this.isDefault = isDefault;
			CastTime = castTime;
			SpellEffectIdOne = spellEffectIdOne;
			SpellIconId = spellIconId;
		}

		/// <summary>
		/// EF Ctor.
		/// </summary>
		private SpellEntryModel()
		{

		}
	}
}
