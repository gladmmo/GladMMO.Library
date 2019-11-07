using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Configuration;
using Akka.DI.AutoFac;
using Akka.DI.Core;
using Akka.Event;
using Autofac;
using Debug = UnityEngine.Debug;
using Module = Autofac.Module;

namespace GladMMO
{
	public sealed class ActorSystemDependencyAutofacModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			//TODO: Expose and register this as appart of configuration system
			ActorAssemblyDefinitionConfiguration actorAssemblyConfig = new ActorAssemblyDefinitionConfiguration(Array.Empty<string>());

			foreach(var s in actorAssemblyConfig.AssemblyNames)
				Debug.Log($"Actor Assembly: {s}");

			Debug.Log(AppDomain.CurrentDomain.GetAssemblies().Aggregate("Loaded Assemblies: ", (s, assembly) => $"{s} {assembly.GetName().Name}"));

			//The below loads the actor assemblies defined in the configuration.
			//It then searches for all message handlers and then registers them.
			//It's a complicated process.
			//TODO: Support actually loading unloaded assemblies (like 3rd party user assemblies)
			foreach (Assembly actorAssemblyToParse in actorAssemblyConfig.AssemblyNames
				.Select(d => AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => d == a.GetName().Name.ToLower()))
				.Where(a => a != null))
			{
				Debug.Log($"Parsing ActorAssembly: {actorAssemblyToParse.GetName().Name}");
				foreach (Type t in actorAssemblyToParse.GetTypes())
				{
					//If they have the handler attribute, we should just register it.
					if (t.GetCustomAttributes<EntityActorMessageHandlerAttribute>().Any())
					{
						Debug.Log($"Register ActorMessageHandler: {t.Name}");

						//Now we need to find the actor state type
						Type actorStateType = t.GetInterfaces()
							.First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityActorMessageHandler<,>))
							.GenericTypeArguments.First();

						var handlerRegisteration = builder.RegisterType(t)
							.AsSelf()
							.As(typeof(IEntityActorMessageHandler<,>).MakeGenericType(new Type[2] {actorStateType, typeof(EntityActorMessage)}))
							.As<IEntityActorMessageHandler>()
							.SingleInstance();

						foreach(var attri in t.GetCustomAttributes<EntityActorMessageHandlerAttribute>())
						{
							//TODO: Support multiple level inherited types.
							//If the actor has a different state type we should assume it's valid if it can be assigned.
							Type specificActorStateType = attri.TargetActorType.BaseType.GenericTypeArguments.Reverse().First();
							
							if (specificActorStateType != actorStateType)
							{
								if (actorStateType.IsAssignableFrom(specificActorStateType))
								{
									handlerRegisteration = handlerRegisteration
										.As(typeof(IEntityActorMessageHandler<,>).MakeGenericType(new Type[2] { specificActorStateType, typeof(EntityActorMessage) }));
								}
								else
									throw new InvalidOperationException($"Actor: {attri.TargetActorType.Name} attempted to use Handler: {t.Name} but had non-matching state Types: {actorStateType.Name}/{specificActorStateType.Name}");
							}
						}
					}
					else if (typeof(IEntityActor).IsAssignableFrom(t))
					{
						//Don't want to register abstract entities.
						if (!t.IsAbstract)
						{
							Debug.Log($"Register Actor: {t.Name}");
							builder.RegisterType(t)
								.AsSelf();
						}
					}
				}
			}

			//Below is an open generic registeration of the generic router
			//this makes it EASY to inject into the actors
			builder.RegisterGeneric(typeof(ReflectionBasedGenericMessageRouter<,>))
				.As(typeof(IEntityActorMessageRouteable<,>))
				.SingleInstance();

			//Create the root of the actor system.
			builder.RegisterInstance(ActorSystem.Create("Root"))
				.AsSelf()
				.As<ActorSystem>()
				.As<IActorRefFactory>()
				.SingleInstance();

			builder.Register<IScheduler>(context =>
				{
					ActorSystem actorSystem = context.Resolve<ActorSystem>();

					return actorSystem.Scheduler;
				})
				.As<IScheduler>()
				.SingleInstance();

			//Creates the autofac dependency resolver that can be used to actually resolve
			//the Actor's dependencies.
			builder.Register(context =>
				{
					if(!context.IsRegistered<IEntityActorMessageRouteable<DefaultWorldActor, WorldActorState>>())
						Debug.LogError($"CRITICAL dependency for Actor IOC not registered.");

					if(!context.IsRegistered<DefaultWorldActor>())
						Debug.LogError($"CRITICAL dependency for Actor IOC not registered.");

					return new AutoFacDependencyResolver(context.Resolve<ILifetimeScope>(), context.Resolve<ActorSystem>());
				})
				.As<IDependencyResolver>()
				.AsSelf()
				.SingleInstance();

			builder.RegisterType<DefaultWorldActor>()
				.AsSelf();

			builder.RegisterType<UnityAkkaActorLoggerAdapter>()
				.AsSelf()
				.As<ILoggingAdapter>()
				.SingleInstance();

			builder.RegisterType<UnityLoggerActor>()
				.AsSelf();

			builder.RegisterType<DefaultGameObjectEntityActorFactory>()
				.As<IGameObjectEntityActorFactory>()
				.SingleInstance();

			//This creates the World actor.
			builder.Register(context =>
				{
					try
					{
						IDependencyResolver resolver = context.Resolve<IDependencyResolver>();
						ActorSystem actorSystem = context.Resolve<ActorSystem>();
						actorSystem.ActorOf(resolver.Create<UnityLoggerActor>(), "Logger");
						IActorRef worldActorReference = actorSystem.ActorOf(resolver.Create<DefaultWorldActor>(), "World");

						if(worldActorReference.IsNobody())
							Debug.LogError($"FAILED TO CREATE WORLD ACTOR.");

						return new WorldActorReferenceAdapter(worldActorReference);
					}
					catch (Exception e)
					{
						Debug.LogError($"Failed to create WorldActor in IoC. Reason: {e.Message}\n\nStack: {e.StackTrace}");
						throw;
					}
				})
				.As<IWorldActorRef>()
				.SingleInstance();
		}
	}
}
