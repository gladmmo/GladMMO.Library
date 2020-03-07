﻿using System; using FreecraftCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GladMMO
{
	[Table("character_defaultactionbar")]
	public class CharacterDefaultActionBarEntry : ICharacterActionBarEntry
	{
		//Composite key ClassType and BarIndex.
		/// <summary>
		/// The class type for the default entry.
		/// </summary>
		public EntityPlayerClassType ClassType { get; private set; }

		/// <summary>
		/// The action bar index.
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

		public CharacterDefaultActionBarEntry(EntityPlayerClassType classType, ActionBarIndex barIndex, int linkedSpellId)
		{
			if(!Enum.IsDefined(typeof(ActionBarIndex), barIndex)) throw new InvalidEnumArgumentException(nameof(barIndex), (int)barIndex, typeof(ActionBarIndex));
			if(linkedSpellId <= 0) throw new ArgumentOutOfRangeException(nameof(linkedSpellId));
			if(!Enum.IsDefined(typeof(EntityPlayerClassType), classType)) throw new InvalidEnumArgumentException(nameof(classType), (int) classType, typeof(EntityPlayerClassType));

			BarIndex = barIndex;
			LinkedSpellId = linkedSpellId;
		}

		/// <summary>
		/// EF Ctor.
		/// </summary>
		internal CharacterDefaultActionBarEntry()
		{

		}
	}
}
