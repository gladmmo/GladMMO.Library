using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Akka.Actor;

namespace GladMMO
{
	/// <summary>
	/// Base handler for spell effects.
	/// </summary>
	public abstract class BaseSpellEffectHandler : ISpellEffectHandler
	{
		protected IReadonlyEntityGuidMappable<IActorRef> ActorReferenceMappable { get; }

		private ThreadLocal<Random> RandomGenerator { get; }

		protected BaseSpellEffectHandler([NotNull] IReadonlyEntityGuidMappable<IActorRef> actorReferenceMappable)
		{
			ActorReferenceMappable = actorReferenceMappable ?? throw new ArgumentNullException(nameof(actorReferenceMappable));
			RandomGenerator = new ThreadLocal<Random>(() => new Random());
		}

		public abstract void ApplySpellEffect(SpellEffectApplicationContext context);

		//TODO: Support spell school definition.
		/// <summary>
		/// Applies damage to the <see cref="entity"/> associated with the provided guid.
		/// Applies <see cref="damageAmount"/> of the damage.
		/// </summary>
		/// <param name="entity">Entity to deal damage to.</param>
		/// <param name="damageAmount">The amount of damage to deal.</param>
		/// <param name="damageSourceEntity">The damage source entity.</param>
		protected void ApplyDamage([NotNull] ObjectGuid entity, int damageAmount, ObjectGuid damageSourceEntity = null)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			if (damageAmount < 0) throw new ArgumentOutOfRangeException(nameof(damageAmount));

			IActorRef actorRef = ActorReferenceMappable.RetrieveEntity(entity);

			if(damageSourceEntity == null || damageSourceEntity == ObjectGuid.Empty)
				actorRef.Tell(new DamageEntityActorCurrentHealthMessage(damageAmount));
			else
			{
				IActorRef sourceRef = ActorReferenceMappable.RetrieveEntity(damageSourceEntity);
				actorRef.Tell(new DamageEntityActorCurrentHealthMessage(damageAmount), sourceRef);
			}
		}

		//TODO: Support spell school definition. For heals might be a Dark heal or Holy heal or Nature heal.
		/// <summary>
		/// Applies a heal to the <see cref="entity"/> associated with the provided guid.
		/// Applies <see cref="healAmount"/> of the heal.
		/// </summary>
		/// <param name="entity">Entity to heal.</param>
		/// <param name="healAmount">The amount to heal.</param>
		/// <param name="healSourceEntity">The healing source entity.</param>
		protected void ApplyHeal([NotNull] ObjectGuid entity, int healAmount, ObjectGuid healSourceEntity = null)
		{
			if(entity == null) throw new ArgumentNullException(nameof(entity));
			if(healAmount < 0) throw new ArgumentOutOfRangeException(nameof(healAmount));

			IActorRef actorRef = ActorReferenceMappable.RetrieveEntity(entity);

			if(healSourceEntity == null || healSourceEntity == ObjectGuid.Empty)
				actorRef.Tell(new HealEntityActorCurrentHealthMessage(healAmount));
			else
			{
				IActorRef sourceRef = ActorReferenceMappable.RetrieveEntity(healSourceEntity);
				actorRef.Tell(new HealEntityActorCurrentHealthMessage(healAmount), sourceRef);
			}
		}

		/// <summary>
		/// Generates a randomized value based on <see cref="effect"/> base value
		/// and random component.
		/// </summary>
		/// <param name="effect">The spell effect.</param>
		/// <returns>Random value depending on base points and dice roll.</returns>
		protected int RollEffectValue([NotNull] SpellEffectDefinitionDataModel effect)
		{
			if (effect == null) throw new ArgumentNullException(nameof(effect));

			return RandomGenerator.Value.Next(effect.EffectBasePoints, effect.EffectBasePoints + 1 + effect.EffectPointsDiceRange); //+1 is due to exclusive.
		}

		/// <summary>
		/// Computes the additive value of level scaling based on application target's level.
		/// Determined by spell effect BasePointsAdditiveLevelModifier.
		/// </summary>
		/// <param name="context">The spell application context.</param>
		/// <returns>The additive value of level scaling.</returns>
		protected int ComputeAdditiveLevelScaling([NotNull] SpellEffectApplicationContext context)
		{
			if (context == null) throw new ArgumentNullException(nameof(context));

			return (int)((float)context.ApplicationTargetEntityData.GetFieldValue<int>(BaseObjectField.UNIT_FIELD_LEVEL) * context.SpellEffectData.SpellEffect.BasePointsAdditiveLevelModifier);
		}
	}
}
