using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;

namespace GladMMO
{
	public interface IPendingSpellCastData
	{
		/// <summary>
		/// The UTC tick time of the spell cast start.
		/// </summary>
		long StartTime { get; }

		/// <summary>
		/// The UTC tick time of the spell cast finish casting.
		/// </summary>
		long ExpectedCastTime { get; }

		/// <summary>
		/// The spell id of the pending cast.
		/// </summary>
		int SpellId { get; }

		/// <summary>
		/// The in-time snapshot of the entity's current target.
		/// </summary>
		NetworkEntityGuid SnapshotEntityTarget { get; }
	}

	public sealed class PendingSpellCastData : IPendingSpellCastData
	{
		/// <summary>
		/// The UTC tick time of the spell cast start.
		/// </summary>
		public long StartTime { get; }

		/// <summary>
		/// The UTC tick time of the spell cast finish casting.
		/// </summary>
		public long ExpectedCastTime { get; }

		/// <summary>
		/// The spell id of the pending cast.
		/// </summary>
		public int SpellId { get; }

		/// <summary>
		/// Cancel token for the pending cast timer.
		/// </summary>
		internal ICancelable PendingCancel { get; }

		/// <summary>
		/// The castime.
		/// </summary>
		public TimeSpan CastTime { get; }

		/// <summary>
		/// The in-time snapshot of the entity's current target.
		/// </summary>
		public NetworkEntityGuid SnapshotEntityTarget { get; }

		public bool isCastCanceled => PendingCancel.IsCancellationRequested;

		public bool isInstantCast => CastTime.Ticks == 0;

		public bool isCompleted => isInstantCast || isCastCanceled || (StartTime + CastTime.Ticks) >= ExpectedCastTime;

		public PendingSpellCastData(long startTime, long expectedCastTime, int spellId, [NotNull] ICancelable pendingCancel, TimeSpan castTime, [NotNull] NetworkEntityGuid snapshotEntityTarget)
		{
			if (spellId <= 0) throw new ArgumentOutOfRangeException(nameof(spellId));
			if (startTime <= 0) throw new ArgumentOutOfRangeException(nameof(startTime));
			if (expectedCastTime <= 0) throw new ArgumentOutOfRangeException(nameof(expectedCastTime));

			StartTime = startTime;
			ExpectedCastTime = expectedCastTime;
			SpellId = spellId;
			PendingCancel = pendingCancel ?? throw new ArgumentNullException(nameof(pendingCancel));
			CastTime = castTime;
			SnapshotEntityTarget = snapshotEntityTarget ?? throw new ArgumentNullException(nameof(snapshotEntityTarget));
		}
	}
}
