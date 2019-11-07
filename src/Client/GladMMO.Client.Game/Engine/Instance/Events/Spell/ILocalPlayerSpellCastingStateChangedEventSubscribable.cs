using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface ILocalPlayerSpellCastingStateChangedEventSubscribable
	{
		event EventHandler<SpellCastingStateChangedEventArgs> OnSpellCastingStateChanged;
	}

	public sealed class SpellCastingStateChangedEventArgs : EventArgs
	{
		public int CastingSpellId { get; }

		public bool isCasting => CastingSpellId != 0;

		public SpellCastingStateChangedEventArgs(int castingSpellId)
		{
			if (castingSpellId < 0) throw new ArgumentOutOfRangeException(nameof(castingSpellId));

			CastingSpellId = castingSpellId;
		}
	}
}
