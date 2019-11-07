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
		/// <summary>
		/// The ID of the spell to cast.
		/// </summary>
		public int SpellId { get; private set; }

		/// <summary>
		/// The in-time snapshot of the entity's current target.
		/// </summary>
		public NetworkEntityGuid SnapshotEntityTarget { get; }

		public ImmediateCastSpellMessage(int spellId, [NotNull] NetworkEntityGuid snapshotEntityTarget)
		{
			if(spellId <= 0) throw new ArgumentOutOfRangeException(nameof(spellId));

			SpellId = spellId;
			SnapshotEntityTarget = snapshotEntityTarget ?? throw new ArgumentNullException(nameof(snapshotEntityTarget));
		}
	}
}
