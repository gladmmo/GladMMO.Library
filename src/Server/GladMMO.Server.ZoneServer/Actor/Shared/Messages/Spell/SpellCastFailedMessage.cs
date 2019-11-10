using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GladMMO
{
	public sealed class SpellCastFailedMessage : EntityActorMessage
	{
		public SpellCastResult Result { get; }

		public int SpellId { get; }

		public SpellCastFailedMessage(SpellCastResult result, int spellId)
		{
			if (!Enum.IsDefined(typeof(SpellCastResult), result)) throw new InvalidEnumArgumentException(nameof(result), (int) result, typeof(SpellCastResult));
			if (spellId <= 0) throw new ArgumentOutOfRangeException(nameof(spellId));

			Result = result;
			SpellId = spellId;
		}
	}
}
