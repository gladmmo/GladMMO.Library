using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;

namespace GladMMO
{
	public sealed class PendingSpellCastData
	{
		/// <summary>
		/// The UTC tick time of the spell cast start.
		/// </summary>
		public long StartTime { get; }

		/// <summary>
		/// The spell id of the pending cast.
		/// </summary>
		public int SpellId { get; }

		internal ICancelable PendingCancel { get; }

		public bool isCastCanceled => PendingCancel.IsCancellationRequested;

		public TimeSpan CastTime { get; }

		public bool isInstantCast => CastTime.Ticks == 0;

		public PendingSpellCastData(long startTime, int spellId, [NotNull] ICancelable pendingCancel, TimeSpan castTime)
		{
			if (spellId <= 0) throw new ArgumentOutOfRangeException(nameof(spellId));
			if (startTime <= 0) throw new ArgumentOutOfRangeException(nameof(startTime));

			StartTime = startTime;
			SpellId = spellId;
			PendingCancel = pendingCancel ?? throw new ArgumentNullException(nameof(pendingCancel));
			CastTime = castTime;
		}
	}
}
