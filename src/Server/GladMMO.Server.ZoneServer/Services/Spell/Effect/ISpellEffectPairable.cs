using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Contract for types that expose a Spell and SpellEffect pair.
	/// </summary>
	public interface ISpellEffectPairable
	{
		/// <summary>
		/// The actual spell that attached <see cref="SpellEffect"></see>
		/// </summary>
		SpellDefinitionDataModel Spell { get; }

		/// <summary>
		/// The linked spell effect.
		/// </summary>
		SpellEffectDefinitionDataModel SpellEffect { get; }
	}
}
