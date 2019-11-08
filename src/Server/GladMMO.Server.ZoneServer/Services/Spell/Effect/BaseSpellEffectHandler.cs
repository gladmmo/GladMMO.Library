using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;

namespace GladMMO
{
	/// <summary>
	/// Base handler for spell effects.
	/// </summary>
	public abstract class BaseSpellEffectHandler : ISpellEffectHandler
	{
		protected IReadonlyEntityGuidMappable<IActorRef> ActorReferenceMappable { get; }

		protected BaseSpellEffectHandler([NotNull] IReadonlyEntityGuidMappable<IActorRef> actorReferenceMappable)
		{
			ActorReferenceMappable = actorReferenceMappable ?? throw new ArgumentNullException(nameof(actorReferenceMappable));
		}

		public abstract void ApplySpellEffect(SpellEffectApplicationContext context);
	}
}
