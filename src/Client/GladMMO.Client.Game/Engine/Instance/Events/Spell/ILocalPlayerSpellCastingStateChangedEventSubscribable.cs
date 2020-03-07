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
		/// Indicates the timestamp of the change if <see cref="isCasting"/> is true.
		/// Potentially 0 if <see cref="isCasting"/> is false.
		/// </summary>
		public long CastingStartTimeStamp { get; }

		/// <summary>
		/// The spell id. If <see cref="isCasting"/> is false then 0.
		/// </summary>
		public int CastingSpellId { get; }

		public bool isCasting => CastingSpellId != 0;

		public SpellCastingStateChangedEventArgs(int castingSpellId, long castingTimeStamp)
		{
			if (castingSpellId < 0) throw new ArgumentOutOfRangeException(nameof(castingSpellId));
			if (castingTimeStamp < 0) throw new ArgumentOutOfRangeException(nameof(castingTimeStamp));

			CastingSpellId = castingSpellId;
			CastingStartTimeStamp = castingTimeStamp;
		}
	}
}
