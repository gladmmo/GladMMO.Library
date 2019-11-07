using System;
using System.Collections.Generic;
using System.Reflection;

namespace GladMMO
{
	public sealed class SingletonSpellEffectTargetValidatorFactory : ISpellEffectTargetValidatorFactory
	{
		private Dictionary<long, ISpellEffectTargetValidator> ValidatorMap { get; } = new Dictionary<long, ISpellEffectTargetValidator>(200);

		public SingletonSpellEffectTargetValidatorFactory([NotNull] IEnumerable<ISpellEffectTargetValidator> validators)
		{
			if (validators == null) throw new ArgumentNullException(nameof(validators));

			foreach (var validator in validators)
			foreach (var validatorAttribute in validator.GetType().GetCustomAttributes<SpellEffectTargetingStrategyAttribute>())
				ValidatorMap.Add(validatorAttribute.TargetTypeKey, validator);
		}

		public ISpellEffectTargetValidator Create([NotNull] SpellEffectDefinitionDataModel context)
		{
			if (context == null) throw new ArgumentNullException(nameof(context));

			long computedKeyValue = SpellEffectTargetingStrategyAttribute.ComputeKey(context.EffectTargetingType, context.AdditionalEffectTargetingType);

			if (ValidatorMap.ContainsKey(computedKeyValue))
				return ValidatorMap[computedKeyValue];
			else
				throw new KeyNotFoundException($"Key {computedKeyValue} for Effect target validator: {context.SpellEffectId} with TargetType: {context.EffectTargetingType}:{context.AdditionalEffectTargetingType} not found.");
		}
	}
}