using System;

namespace GladMMO
{
	public sealed class PendingSpellCastCreationContext
	{
		public int SpellId { get; }

		public ObjectGuid CurrentTarget { get; }

		public PendingSpellCastCreationContext(int spellId, [NotNull] ObjectGuid currentTarget)
		{
			if (spellId < 0) throw new ArgumentOutOfRangeException(nameof(spellId));

			SpellId = spellId;
			CurrentTarget = currentTarget ?? throw new ArgumentNullException(nameof(currentTarget));
		}
	}
}