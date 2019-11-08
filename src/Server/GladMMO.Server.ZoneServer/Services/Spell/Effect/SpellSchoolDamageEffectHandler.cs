using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Akka.Actor;

namespace GladMMO
{
	[SpellEffectHandler(SpellEffectType.SPELL_EFFECT_SCHOOL_DAMAGE)]
	public sealed class SpellSchoolDamageEffectHandler : BaseSpellEffectHandler
	{
		public SpellSchoolDamageEffectHandler(IReadonlyEntityGuidMappable<IActorRef> actorReferenceMappable) 
			: base(actorReferenceMappable)
		{

		}

		public override void ApplySpellEffect([NotNull] SpellEffectApplicationContext context)
		{
			if (context == null) throw new ArgumentNullException(nameof(context));

			//TODO: Support spell schools AKA Frost damage, nature, physical and etc.
			int damageValue = RollEffectValue(context.SpellEffectData.SpellEffect) + ComputeAdditiveLevelScaling(context);
			ApplyDamage(context.ApplicationTarget, damageValue, context.SpellSource);
		}
	}
}
