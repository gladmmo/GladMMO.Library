using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Akka.Actor;
using Akka.DI.AutoFac;
using Akka.DI.Core;
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
			//TODO: Support actually loading unloaded assemblies (like 3rd party user assemblies)
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

			//Create the root of the actor system.
			ActorSystem actorSystem = ActorSystem.Create("Root");
			builder.RegisterInstance(actorSystem)
				.AsSelf()
				.As<IActorRefFactory>()
				.SingleInstance();

			//Creates the autofac dependency resolver that can be used to actually resolve
			//the Actor's dependencies.
			builder.Register(context => new AutoFacDependencyResolver(context.Resolve<ILifetimeScope>(), actorSystem))
				.As<IDependencyResolver>()
				.AsSelf()
				.SingleInstance();

			//This creates the World actor.
			builder.Register(context =>
				{
					IDependencyResolver resolver = context.Resolve<IDependencyResolver>();
					IActorRef worldActorReference = actorSystem.ActorOf(resolver.Create<DefaultWorldActor>(), "World");

					//TODO: Eventually we should treat the world as a network object.
					worldActorReference.Tell(new EntityActorStateInitializeMessage<DefaultEntityActorStateContainer>(new DefaultEntityActorStateContainer(new EntityFieldDataCollection(8), NetworkEntityGuid.Empty)));

					return new WorldActorReferenceAdapter(worldActorReference);
				})
				.As<IWorldActorRef>()
				.SingleInstance();
		}
	}
}
