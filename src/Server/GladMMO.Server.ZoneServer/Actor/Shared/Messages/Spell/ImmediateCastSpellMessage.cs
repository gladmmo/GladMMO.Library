using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Message for an entity actor that should begin a cast
	/// of a spell immediately.
	/// </summary>
	public sealed class ImmediateCastSpellMessage : EntityActorMessage
	{
		/// <summary>
		/// The ID of the spell to cast.
		/// </summary>
		public int SpellId { get; private set; }

		public ImmediateCastSpellMessage(int spellId)
		{
			if(spellId <= 0) throw new ArgumentOutOfRangeException(nameof(spellId));

			SpellId = spellId;
		}
	}
}
