using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public enum SpellCastingEventChangeState
	{
		Stopped = 0,
		Canceled = 1,
		Casting = 3,
	}

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

		/// <summary>
		/// Indicates if the state is a casting state.
		/// </summary>
		public bool isCasting => State == SpellCastingEventChangeState.Casting;

		/// <summary>
		/// Indicates the new state.
		/// </summary>
		public SpellCastingEventChangeState State { get; }

		/// <summary>
		/// 64bit temporarily unique spell cast identifier.
		/// (Can be used to link events like Start, Go and Cancel within a short period of time).
		/// </summary>
		public long SpellCastIdentifier { get; }

		public SpellCastingStateChangedEventArgs(int castingSpellId, int remainingCastTime, SpellCastingEventChangeState state, long spellCastIdentifier)
		{
			if (castingSpellId < 0) throw new ArgumentOutOfRangeException(nameof(castingSpellId));
			if (remainingCastTime < 0) throw new ArgumentOutOfRangeException(nameof(remainingCastTime));

			CastingSpellId = castingSpellId;
			RemainingCastTime = remainingCastTime;
			State = state;
			SpellCastIdentifier = spellCastIdentifier;
		}
	}
}
