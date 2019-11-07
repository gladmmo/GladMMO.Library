using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Autofac;
using Module = Autofac.Module;

namespace GladMMO
{
	public sealed class SpellSystemDependencyAutofacModule : Module
	{
		protected override void Load([NotNull] ContainerBuilder builder)
		{
			if (builder == null) throw new ArgumentNullException(nameof(builder));

			//TODO: Expose and register this as appart of configuration system
			ActorAssemblyDefinitionConfiguration actorAssemblyConfig = new ActorAssemblyDefinitionConfiguration(Array.Empty<string>());

			RegisterSpellTargetValidators(actorAssemblyConfig, builder);
			RegisterSpellTargetSelectors(actorAssemblyConfig, builder);

			//EffectTargetSelectorRoutedSpellCastDispatcher
			builder.RegisterType<EffectTargetSelectorRoutedSpellCastDispatcher>()
				.As<ISpellCastDispatcher>()
				.SingleInstance();
		}

		private void RegisterSpellTargetSelectors([NotNull] ActorAssemblyDefinitionConfiguration actorAssemblyConfig, [NotNull] ContainerBuilder builder)
		{
			if (actorAssemblyConfig == null) throw new ArgumentNullException(nameof(actorAssemblyConfig));
			if (builder == null) throw new ArgumentNullException(nameof(builder));

			//SingletonSpellEffectTargetSelectorFactory : ISpellEffectTargetSelectorFactory
			builder.RegisterType<SingletonSpellEffectTargetSelectorFactory>()
				.As<ISpellEffectTargetSelectorFactory>()
				.SingleInstance();

			foreach(Assembly assemblyToParse in actorAssemblyConfig.AssemblyNames
				.Select(d => AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => d == a.GetName().Name.ToLower()))
				.Where(a => a != null))
			{
				foreach(Type t in assemblyToParse.GetTypes())
				{
					//If they have the handler attribute, we should just register it.
					if(typeof(ISpellEffectTargetSelector).IsAssignableFrom(t) && t.GetCustomAttributes<SpellEffectTargetingStrategyAttribute>().Any() && !t.IsAbstract)
					{
						builder.RegisterType(t)
							.As<ISpellEffectTargetSelector>()
							.SingleInstance();
					}
				}
			}
		}

		private void RegisterSpellTargetValidators([NotNull] ActorAssemblyDefinitionConfiguration actorAssemblyConfig, [NotNull] ContainerBuilder builder)
		{
			if (actorAssemblyConfig == null) throw new ArgumentNullException(nameof(actorAssemblyConfig));
			if (builder == null) throw new ArgumentNullException(nameof(builder));

			//SingletonSpellEffectTargetValidatorFactory : ISpellEffectTargetValidatorFactory
			builder.RegisterType<SingletonSpellEffectTargetValidatorFactory>()
				.As<ISpellEffectTargetValidatorFactory>()
				.SingleInstance();

			//StrategyBasedSpellTargetValidator : ISpellTargetValidator
			builder.RegisterType<StrategyBasedSpellTargetValidator>()
				.As<ISpellTargetValidator>()
				.SingleInstance();

			foreach(Assembly assemblyToParse in actorAssemblyConfig.AssemblyNames
				.Select(d => AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => d == a.GetName().Name.ToLower()))
				.Where(a => a != null))
			{
				foreach(Type t in assemblyToParse.GetTypes())
				{
					//If they have the handler attribute, we should just register it.
					if(typeof(ISpellEffectTargetValidator).IsAssignableFrom(t) && t.GetCustomAttributes<SpellEffectTargetingStrategyAttribute>().Any() && !t.IsAbstract)
					{
						builder.RegisterType(t)
							.As<ISpellEffectTargetValidator>()
							.SingleInstance();
					}
				}
			}
		}
	}
}
