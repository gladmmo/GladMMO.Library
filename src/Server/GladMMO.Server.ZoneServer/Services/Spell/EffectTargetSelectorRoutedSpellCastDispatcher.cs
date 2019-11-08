using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using Common.Logging;

namespace GladMMO
{
	public sealed class EffectTargetSelectorRoutedSpellCastDispatcher : ISpellCastDispatcher
	{
		private IReadonlySpellDataCollection SpellDataCollection { get; }

		private ISpellEffectTargetSelectorFactory EffectTargetSelectorFactory { get; }

		private ILog Logger { get; }

		private ISpellEffectApplicationMessageFactory SpellEffectApplicationMessageFactory { get; }

		private IReadonlyEntityGuidMappable<IActorRef> ActorReferenceMappable { get; }

		public EffectTargetSelectorRoutedSpellCastDispatcher([NotNull] IReadonlySpellDataCollection spellDataCollection, 
			[NotNull] ISpellEffectTargetSelectorFactory effectSelectorFactory,
			[NotNull] ILog logger,
			[NotNull] ISpellEffectApplicationMessageFactory spellEffectApplicationMessageFactory,
			[NotNull] IReadonlyEntityGuidMappable<IActorRef> actorReferenceMappable)
		{
			SpellDataCollection = spellDataCollection ?? throw new ArgumentNullException(nameof(spellDataCollection));
			EffectTargetSelectorFactory = effectSelectorFactory ?? throw new ArgumentNullException(nameof(effectSelectorFactory));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			SpellEffectApplicationMessageFactory = spellEffectApplicationMessageFactory ?? throw new ArgumentNullException(nameof(spellEffectApplicationMessageFactory));
			ActorReferenceMappable = actorReferenceMappable ?? throw new ArgumentNullException(nameof(actorReferenceMappable));
		}

		public void DispatchSpellCast([NotNull] IPendingSpellCastData pendingSpellCast, DefaultEntityActorStateContainer casterData)
		{
			if (pendingSpellCast == null) throw new ArgumentNullException(nameof(pendingSpellCast));

			if (!SpellDataCollection.ContainsSpellDefinition(pendingSpellCast.SpellId))
				throw new InvalidOperationException($"Tried to cast Spell: {pendingSpellCast.SpellId} but no definition exists.");

			IActorRef casterActorReference = ActorReferenceMappable.RetrieveEntity(casterData.EntityGuid);
			SpellDefinitionDataModel spellDefinition = SpellDataCollection.GetSpellDefinition(pendingSpellCast.SpellId);

			//Each spell can have N effects with individual unique targeting attributes.
			//So we need to handle each spell effect seperately, compute their effects/targets
			//and send an effect application message to the involved actors.
			foreach (SpellEffectIndex effectIndex in spellDefinition.EnumerateSpellEffects())
			{
				SpellEffectDefinitionDataModel effectDefinition = SpellDataCollection.GetSpellEffectDefinition(spellDefinition.GetSpellEffectId(effectIndex));

				SpellEffectTargetContext targetContext = EffectTargetSelectorFactory
					.Create(effectDefinition)
					.CalculateTargets(spellDefinition, effectDefinition, casterData, pendingSpellCast);

				ApplySpellEffectMessage spellEffectApplicationMessage = SpellEffectApplicationMessageFactory.Create(new SpellEffectApplicationMessageCreationContext(casterData.EntityGuid, pendingSpellCast.SpellId, effectIndex));

				//For each actor target in the target context
				//we need to send the spell application message for handling
				foreach(var target in targetContext.SpellEffectTargets)
				{
					if(Logger.IsDebugEnabled)
						Logger.Debug($"Entity: {casterData.EntityGuid} casted spell with effect that targets Target: {target.Path.Name}");

					target.Tell(spellEffectApplicationMessage, casterActorReference);
				}
			}
		}
	}
}
