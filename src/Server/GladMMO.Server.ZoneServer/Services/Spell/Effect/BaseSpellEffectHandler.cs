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
			RandomGenerator = new ThreadLocal<Random>();
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
		protected void ApplyDamage([NotNull] NetworkEntityGuid entity, int damageAmount, NetworkEntityGuid damageSourceEntity = null)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			if (damageAmount < 0) throw new ArgumentOutOfRangeException(nameof(damageAmount));

			IActorRef actorRef = ActorReferenceMappable.RetrieveEntity(entity);

			if(damageSourceEntity == null || damageSourceEntity == NetworkEntityGuid.Empty)
				actorRef.Tell(new DamageEntityActorCurrentHealthMessage(damageAmount));
			else
			{
				IActorRef sourceRef = ActorReferenceMappable.RetrieveEntity(damageSourceEntity);
				actorRef.Tell(new DamageEntityActorCurrentHealthMessage(damageAmount), sourceRef);
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
	}
}
