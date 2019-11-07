using System;
using System.Collections.Generic;
using System.Reflection;

namespace GladMMO
{
	public sealed class SingletonSpellEffectTargetSelectorFactory : ISpellEffectTargetSelectorFactory
	{
		private Dictionary<long, ISpellEffectTargetSelector> SelectorMap { get; } = new Dictionary<long, ISpellEffectTargetSelector>(200);

		public SingletonSpellEffectTargetSelectorFactory([NotNull] IEnumerable<ISpellEffectTargetSelector> selectors)
		{
			if(selectors == null) throw new ArgumentNullException(nameof(selectors));

			foreach(var selector in selectors)
			foreach(var selectorAttribute in selector.GetType().GetCustomAttributes<SpellEffectTargetingStrategyAttribute>())
				SelectorMap.Add(selectorAttribute.TargetTypeKey, selector);
		}

		public ISpellEffectTargetSelector Create([NotNull] SpellEffectDefinitionDataModel context)
		{
			if (context == null) throw new ArgumentNullException(nameof(context));

			long computedKeyValue = SpellEffectTargetingStrategyAttribute.ComputeKey(context.EffectTargetingType, context.AdditionalEffectTargetingType);

			if(SelectorMap.ContainsKey(computedKeyValue))
				return SelectorMap[computedKeyValue];
			else
				throw new KeyNotFoundException($"Key {computedKeyValue} for Effect target selector: {context.SpellEffectId} with TargetType: {context.EffectTargetingType}:{context.AdditionalEffectTargetingType} not found.");
		}
	}
}