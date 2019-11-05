using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Message for an entity actor that requests it cast a spell.
	/// There is no promise the spell will go off.
	/// </summary>
	public sealed class TryCastSpellMessage : EntityActorMessage
	{
		/// <summary>
		/// The ID of the requested spell to cast.
		/// </summary>
		public int SpellId { get; private set; }

		public TryCastSpellMessage(int spellId)
		{
			if(spellId <= 0) throw new ArgumentOutOfRangeException(nameof(spellId));

			SpellId = spellId;
		}
	}
}
