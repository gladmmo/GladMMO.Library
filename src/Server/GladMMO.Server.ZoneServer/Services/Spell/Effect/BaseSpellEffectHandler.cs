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

		//TODO: Support spell school definition.
		/// <summary>
		/// Applies damage to the <see cref="entity"/> associated with the provided guid.
		/// Applies <see cref="damageAmount"/> of the damage.
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="damageAmount"></param>
		protected void ApplyDamage([NotNull] NetworkEntityGuid entity, int damageAmount, NetworkEntityGuid damageSourceEntity = null)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			if (damageAmount < 0) throw new ArgumentOutOfRangeException(nameof(damageAmount));

			IActorRef actorRef = ActorReferenceMappable.RetrieveEntity(entity);

			if(damageSourceEntity == null || damageSourceEntity != NetworkEntityGuid.Empty)
				actorRef.Tell(new DamageEntityActorCurrentHealthMessage(damageAmount));
			else
			{
				IActorRef sourceRef = ActorReferenceMappable.RetrieveEntity(damageSourceEntity);
				actorRef.Tell(new DamageEntityActorCurrentHealthMessage(damageAmount), sourceRef);
			}
		}
	}
}
