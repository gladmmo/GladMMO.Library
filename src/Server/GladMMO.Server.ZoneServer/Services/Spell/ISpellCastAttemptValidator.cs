using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface ISpellCastAttemptValidator
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
