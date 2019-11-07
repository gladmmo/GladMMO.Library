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

		private ISpellEffectTargetSelectorFactory EffectSelectorFactory { get; }

		private ILog Logger { get; }

		public EffectTargetSelectorRoutedSpellCastDispatcher([NotNull] IReadonlySpellDataCollection spellDataCollection, 
			[NotNull] ISpellEffectTargetSelectorFactory effectSelectorFactory,
			[NotNull] ILog logger)
		{
			SpellDataCollection = spellDataCollection ?? throw new ArgumentNullException(nameof(spellDataCollection));
			EffectSelectorFactory = effectSelectorFactory ?? throw new ArgumentNullException(nameof(effectSelectorFactory));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public void DispatchSpellCast([NotNull] IPendingSpellCastData pendingSpellCast, DefaultEntityActorStateContainer casterData)
		{
			if (pendingSpellCast == null) throw new ArgumentNullException(nameof(pendingSpellCast));

			if (!SpellDataCollection.ContainsSpellDefinition(pendingSpellCast.SpellId))
				throw new InvalidOperationException($"Tried to cast Spell: {pendingSpellCast.SpellId} but no definition exists.");

			SpellDefinitionDataModel spellDefinition = SpellDataCollection.GetSpellDefinition(pendingSpellCast.SpellId);

			//No effect exists. We have nothing to do.
			if (spellDefinition.SpellEffectIdOne == 0)
			{
				return;
			}

			//TODO: Handle MULTIPLE effects.
			SpellEffectDefinitionDataModel effectDefinition = SpellDataCollection.GetSpellEffectDefinition(spellDefinition.SpellEffectIdOne);
			ISpellEffectTargetSelector targetSelector = EffectSelectorFactory.Create(effectDefinition);
			SpellEffectTargetContext targetContext = targetSelector.CalculateTargets(spellDefinition, effectDefinition, casterData, pendingSpellCast);

			foreach (var target in targetContext.SpellEffectTargets)
			{
				//TODO: Send spell cast result to them.
				Logger.Info($"Entity: {casterData.EntityGuid} casted spell with effect that targets Target: {target.Path.Name}");
			}
		}
	}
}
