using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;

namespace GladMMO
{
	public sealed class SpellEffectTargetContext
	{
		public IEnumerable<IActorRef> SpellEffectTargets { get; }

		public SpellEffectTargetContext([NotNull] IEnumerable<IActorRef> spellEffectTargets)
		{
			SpellEffectTargets = spellEffectTargets ?? throw new ArgumentNullException(nameof(spellEffectTargets));
		}
	}
}
