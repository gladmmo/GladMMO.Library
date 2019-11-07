using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class SpellEffectTargetCreationContext
	{
		public int SpellId { get; }

		public SpellEffectIndex EffectIndex { get; }

		public DefaultEntityActorStateContainer ActorState { get; }

		public SpellEffectTargetCreationContext(int spellId, SpellEffectIndex effectIndex, [NotNull] DefaultEntityActorStateContainer actorState)
		{
			if (spellId <= 0) throw new ArgumentOutOfRangeException(nameof(spellId));

			SpellId = spellId;
			EffectIndex = effectIndex;
			ActorState = actorState ?? throw new ArgumentNullException(nameof(actorState));
		}
	}
}
