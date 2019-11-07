using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class StrategyBasedSpellTargetValidator : ISpellTargetValidator
	{
		private IReadonlySpellDataCollection SpellDataCollection { get; }

		private ISpellEffectTargetValidatorFactory ValidatorFactory { get; }

		public StrategyBasedSpellTargetValidator([NotNull] IReadonlySpellDataCollection spellDataCollection,
			[NotNull] ISpellEffectTargetValidatorFactory validatorFactory)
		{
			SpellDataCollection = spellDataCollection ?? throw new ArgumentNullException(nameof(spellDataCollection));
			ValidatorFactory = validatorFactory ?? throw new ArgumentNullException(nameof(validatorFactory));
		}

		public bool isSpellTargetViable(int spellId, [NotNull] DefaultEntityActorStateContainer actorState)
		{
			if (actorState == null) throw new ArgumentNullException(nameof(actorState));
			if (spellId <= 0) throw new ArgumentOutOfRangeException(nameof(spellId));

			SpellDefinitionDataModel definition = SpellDataCollection.GetSpellDefinition(spellId);

			foreach (var effect in SpellDataCollection.GetEffectsForSpell(spellId))
			{
				ISpellEffectTargetValidator effectTargetValidator = ValidatorFactory.Create(effect);

				//One effect's targets failed given the context. So this fails.
				if (!effectTargetValidator.ValidateTargetContext(definition, effect, actorState))
					return false;
			}

			return true;
		}
	}
}
