using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface ISpellTargetValidator
	{
		/// <summary>
		/// Indicates if a spell target is valid/viable given the <see cref="actorState"/>
		/// context of an entity actor.
		/// </summary>
		/// <param name="spellId">The id of the spell to validate targets for.</param>
		/// <param name="actorState">The current actor state.</param>
		/// <returns>True if a target is viable for a spell given the current context of actor state.</returns>
		bool isSpellTargetViable(int spellId, DefaultEntityActorStateContainer actorState);
	}
}
