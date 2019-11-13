using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Contract for validators that determine spell cast attempt results.
	/// </summary>
	public interface ISpellCastValidator
	{
		/// <summary>
		/// Indicates the spell cast result. Successful means it's fine.
		/// Any other result indicates failure.
		/// </summary>
		/// <param name="actorState">The actor's state.</param>
		/// <param name="spellId"></param>
		/// <returns>The potential failure result of spell cast validation.</returns>
		SpellCastResult ValidateSpellCast(DefaultEntityActorStateContainer actorState, int spellId);
	}
}
