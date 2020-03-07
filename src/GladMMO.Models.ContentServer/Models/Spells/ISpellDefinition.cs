using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface ISpellDefinition
	{
		int SpellId { get; }

		//We don't make this unique because users may want to have
		//spells of differing ids but with the same name, they might be different spells.
		string SpellName { get; }

		/// <summary>
		/// Indicates the type of spell: Spell, melee or ranged.
		/// </summary>
		SpellClassType SpellType { get; }

		/// <summary>
		/// Cast time in milliseconds.
		/// </summary>
		int CastTime { get; }

		/// <summary>
		/// The ID of the first spell effect.
		/// </summary>
		int SpellEffectIdOne { get; }

		/// <summary>
		/// The id of content icon used for the spell on actionbars.
		/// </summary>
		int SpellIconId { get; }
	}
}
