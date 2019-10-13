using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using Akka.DI.Core;
using Common.Logging;

namespace GladMMO
{
	[EntityActorMessageHandler(typeof(DefaultWorldActor))]
	public sealed class WorldActorGameObjectCreateEntityActorMessageHandler : BaseEntityActorMessageHandler<WorldActorState, CreateGameObjectEntityActorMessage>
	{
		private ILog Logger { get; }

		private IGameObjectEntityActorFactory EntityActorFactory { get; }

		private IDependencyResolver Resolver { get; }

		private IEntityGuidMappable<IActorRef> ActorRefMappable { get; }

		public WorldActorGameObjectCreateEntityActorMessageHandler([NotNull] ILog logger,
			[NotNull] IGameObjectEntityActorFactory entityActorFactory,
			[NotNull] IDependencyResolver resolver,
			[NotNull] IEntityGuidMappable<IActorRef> actorRefMappable)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			EntityActorFactory = entityActorFactory ?? throw new ArgumentNullException(nameof(entityActorFactory));
			Resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
			ActorRefMappable = actorRefMappable ?? throw new ArgumentNullException(nameof(actorRefMappable));
		}

		protected override void HandleMessage(EntityActorMessageContext messageContext, WorldActorState state, CreateGameObjectEntityActorMessage message)
		{
			if(message.EntityGuid.EntityType != EntityType.GameObject)
				throw new InvalidOperationException($"Tried to create GameObject Actor for non-GameObject Entity: {message.EntityGuid}");

			//Even visual objects now get actors
			EntityActorCreationResult actorCreationData = EntityActorFactory.Create(message.EntityGuid);

			//Create the actor and tell it to initialize.
			IActorRef actorRef = state.WorldActorFactory.ActorOf(Resolver.Create(actorCreationData.DesiredActorType), message.EntityGuid.RawGuidValue.ToString());
			actorRef.Tell(actorCreationData.InitializationMessage);

			ActorRefMappable.AddObject(message.EntityGuid, actorRef);

			if(Logger.IsInfoEnabled)
				Logger.Info($"Created GameObject Actor: {actorCreationData.DesiredActorType} for Entity: {message.EntityGuid}");
		}
	}
}
