using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Message that indicates a pending spell finished waiting for cast time.
	/// </summary>
	public sealed class PendingSpellCastFinishedWaitingMessage : EntityActorMessage
	{
		/// <summary>
		/// The pending spell cast data.
		/// </summary>
		public PendingSpellCastData Pending { get; private set; }

		public PendingSpellCastFinishedWaitingMessage([NotNull] PendingSpellCastData pending)
		{
			Pending = pending ?? throw new ArgumentNullException(nameof(pending));
		}
	}
}
