using System;
using System.Collections.Generic;
using System.Reflection;

namespace GladMMO
{
	public sealed class SingletonSpellEffectHandlerFactory : ISpellEffectHandlerFactory
	{
		private Dictionary<SpellEffectType, ISpellEffectHandler> SelectorMap { get; } = new Dictionary<SpellEffectType, ISpellEffectHandler>(200);

		public SingletonSpellEffectHandlerFactory([NotNull] IEnumerable<ISpellEffectHandler> handlers)
		{
			if(handlers == null) throw new ArgumentNullException(nameof(handlers));

			foreach (var handler in handlers)
			{
				SpellEffectHandlerAttribute handlerAttribute = handler.GetType().GetCustomAttribute<SpellEffectHandlerAttribute>();
				SelectorMap.Add(handlerAttribute.EffectType, handler);
			}
		}

		public ISpellEffectHandler Create([NotNull] ISpellEffectPairable context)
		{
			if (context == null) throw new ArgumentNullException(nameof(context));

			if (!SelectorMap.ContainsKey(context.SpellEffect.EffectType))
				throw new InvalidOperationException($"Cannot load Spell Effect Handler for Effect: {context.SpellEffect.SpellEffectId} in Spell: {context.Spell.SpellId}:{context.Spell.SpellName} with EffectType: {context.SpellEffect.EffectType}");

			return SelectorMap[context.SpellEffect.EffectType];
		}
	}
}