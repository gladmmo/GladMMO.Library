using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Akka.Actor;

namespace GladMMO
{
	[SpellEffectHandler(SpellEffectType.SPELL_EFFECT_HEAL)]
	public sealed class SpellHealEffectHandler : BaseSpellEffectHandler
	{
		public SpellHealEffectHandler(IReadonlyEntityGuidMappable<IActorRef> actorReferenceMappable) 
			: base(actorReferenceMappable)
		{

		}

		public override void ApplySpellEffect([NotNull] SpellEffectApplicationContext context)
		{
			if (context == null) throw new ArgumentNullException(nameof(context));

			//TODO: Implement healing power influence.
			//TODO: Support spell schools AKA Holy heal, nature, shadow/dark and etc.
			int healAmount = RollEffectValue(context.SpellEffectData.SpellEffect) + ComputeAdditiveLevelScaling(context);
			ApplyHeal(context.ApplicationTarget, healAmount, context.SpellSource);
		}
	}
}
