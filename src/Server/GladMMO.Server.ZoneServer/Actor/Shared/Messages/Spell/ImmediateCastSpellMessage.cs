using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Message for an entity actor that should begin a cast
	/// of a spell immediately.
	/// </summary>
	public sealed class ImmediateCastSpellMessage : EntityActorMessage
	{
		public IPendingSpellCastData PendingSpellData { get; }

		public ImmediateCastSpellMessage([NotNull] IPendingSpellCastData pendingSpellData)
		{
			PendingSpellData = pendingSpellData ?? throw new ArgumentNullException(nameof(pendingSpellData));
		}
	}
}
