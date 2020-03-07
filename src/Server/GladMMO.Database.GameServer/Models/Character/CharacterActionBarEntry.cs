using System; using FreecraftCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GladMMO
{
	[Table("character_actionbar")]
	public class CharacterActionBarEntry : ICharacterActionBarEntry
	{
		//Not unique
		/// <summary>
		/// Character this action bar entry is for.
		/// </summary>
		[ForeignKey(nameof(Character))]
		public int CharacterId { get; private set; }

		//Nav property
		/// <summary>
		/// Character this action bar entry is for.
		/// </summary>
		public virtual CharacterEntryModel Character { get; private set; }

		/// <summary>
		/// The action bar index.
		/// Combined with <see cref="CharacterId"/> represents the composite key.
		/// </summary>
		public ActionBarIndex BarIndex { get; private set; }

		/// <summary>
		/// The potential spell linked to the action bar.
		/// May be null if it's not assigned or an item is located on the action bar
		/// instead of the a spell.
		/// </summary>
		[CanBeNull]
		[ForeignKey(nameof(LinkedSpell))]
		public int? LinkedSpellId { get; private set; }

		/// <summary>
		/// Null unless <see cref="LinkedSpellId"/> is not null.
		/// </summary>
		[CanBeNull]
		public virtual SpellEntryModel LinkedSpell { get; private set; }

		//TODO: Support items.
		[NotMapped]
		public ActionBarIndexType ActionType => LinkedSpellId.HasValue ? ActionBarIndexType.Spell : ActionBarIndexType.Empty;

		[NotMapped]
		public int ActionId => LinkedSpellId.HasValue ? LinkedSpellId.Value : 0;

		public CharacterActionBarEntry(int characterId, ActionBarIndex barIndex, int linkedSpellId)
		{
			if (characterId <= 0) throw new ArgumentOutOfRangeException(nameof(characterId));
			if (!Enum.IsDefined(typeof(ActionBarIndex), barIndex)) throw new InvalidEnumArgumentException(nameof(barIndex), (int) barIndex, typeof(ActionBarIndex));
			if (linkedSpellId <= 0) throw new ArgumentOutOfRangeException(nameof(linkedSpellId));

			CharacterId = characterId;
			BarIndex = barIndex;
			LinkedSpellId = linkedSpellId;
		}

		/// <summary>
		/// EF Ctor.
		/// </summary>
		internal CharacterActionBarEntry()
		{
			
		}
	}
}
