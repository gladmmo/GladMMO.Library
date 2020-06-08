using System; using FreecraftCore;
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
		/// <summary>
		/// Indicates the remaining time in milliseconds until cast complete if <see cref="isCasting"/> is true.
		/// Potentially 0 if <see cref="isCasting"/> is false.
		/// </summary>
		public int RemainingCastTime { get; }

		/// <summary>
		/// The spell id. If <see cref="isCasting"/> is false then 0.
		/// </summary>
		public int CastingSpellId { get; }

		public bool isCasting => CastingSpellId != 0;

		public SpellCastingStateChangedEventArgs(int castingSpellId, int remainingCastTime)
		{
			if (castingSpellId < 0) throw new ArgumentOutOfRangeException(nameof(castingSpellId));
			if (remainingCastTime < 0) throw new ArgumentOutOfRangeException(nameof(remainingCastTime));

			CastingSpellId = castingSpellId;
			RemainingCastTime = remainingCastTime;
		}
	}
}
