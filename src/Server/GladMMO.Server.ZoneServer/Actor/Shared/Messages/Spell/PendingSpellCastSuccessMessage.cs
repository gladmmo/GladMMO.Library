using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Message that indicates the successful cast of a pending spell.
	/// </summary>
	public sealed class PendingSpellCastSuccessMessage : EntityActorMessage
	{
		/// <summary>
		/// The pending spell cast data.
		/// </summary>
		public PendingSpellCastData Pending { get; private set; }

		public PendingSpellCastSuccessMessage([NotNull] PendingSpellCastData pending)
		{
			Pending = pending ?? throw new ArgumentNullException(nameof(pending));
		}
	}
}
