using System;
using System.Collections.Generic;
using System.Text;

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

		public PendingSpellCastData(long startTime, int spellId)
		{
			if (spellId <= 0) throw new ArgumentOutOfRangeException(nameof(spellId));
			if (startTime <= 0) throw new ArgumentOutOfRangeException(nameof(startTime));

			StartTime = startTime;
			SpellId = spellId;
		}
	}
}
