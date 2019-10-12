using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Autofac;
using Module = Autofac.Module;

namespace GladMMO
{
	public sealed class ActorSystemDependencyAutofacModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			//TODO: Expose and register this as appart of configuration system
			ActorAssemblyDefinitionConfiguration actorAssemblyConfig = new ActorAssemblyDefinitionConfiguration(Array.Empty<string>());

			//The below loads the actor assemblies defined in the configuration.
			//It then searches for all message handlers and then registers them.
			//It's a complicated process.
			foreach (Assembly actorAssemblyToParse in actorAssemblyConfig.AssemblyNames
				.Select(d => AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => d == a.FullName.ToString()))
				.Where(a => a != null))
			{
				foreach (Type t in actorAssemblyToParse.GetTypes())
				{
					//If they have the handler attribute, we should just register it.
					if (t.GetCustomAttribute<EntityActorMessageHandlerAttribute>() != null)
					{
						//Now we need to find the actor state type
						Type actorStateType = t.GetInterfaces()
							.First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityActorMessageHandler<,>))
							.GenericTypeArguments.First();

						builder.RegisterType(t)
							.AsSelf()
							.As(typeof(IEntityActorMessageHandler<,>).MakeGenericType(new Type[2] {actorStateType, typeof(EntityActorMessage)}))
							.SingleInstance();
					}
				}
			}

			//Below is an open generic registeration of the generic router
			//this makes it EASY to inject into the actors
			builder.RegisterGeneric(typeof(ReflectionBasedGenericMessageRouter<,>))
				.As(typeof(IEntityActorMessageRouteable<,>))
				.SingleInstance();
		}
	}
}
