using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Spell level learned spells are spells that ALL characters of a specific class and level
	/// should automatically learn/know.
	/// </summary>
	[Table("spell_levellearned")]
	public class SpellLevelLearned
	{
		/// <summary>
		/// The unique id of the level learned spell.
		/// Doesn't really indicate anything specific.
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int SpellLearnId { get; private set; }

		/// <summary>
		/// The character class type this learned spell is defined for.
		/// </summary>
		[Required]
		public EntityPlayerClassType CharacterClassType { get; private set; }

		/// <summary>
		/// Indicates what level the spell should be learned at.
		/// </summary>
		[Range(0, int.MaxValue)]
		[Required]
		public int LevelLearned { get; private set; }

		/// <summary>
		/// The ID of the spell that should be learned.
		/// </summary>
		[Required]
		[ForeignKey(nameof(Spell))]
		public int SpellId { get; private set; }

		//Navigation property
		/// <summary>
		/// The learned spell navigation property.
		/// </summary>
		public virtual SpellEffectEntryModel Spell { get; private set; }

		public SpellLevelLearned(EntityPlayerClassType characterClassType, int levelLearned, int spellId)
		{
			if (!Enum.IsDefined(typeof(EntityPlayerClassType), characterClassType)) throw new InvalidEnumArgumentException(nameof(characterClassType), (int) characterClassType, typeof(EntityPlayerClassType));
			if (levelLearned < 0) throw new ArgumentOutOfRangeException(nameof(levelLearned));
			if (spellId < 0) throw new ArgumentOutOfRangeException(nameof(spellId));

			CharacterClassType = characterClassType;
			LevelLearned = levelLearned;
			SpellId = spellId;
		}

		/// <summary>
		/// EF Ctor.
		/// </summary>
		internal SpellLevelLearned()
		{
			
		}
	}
}
