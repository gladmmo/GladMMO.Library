using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using Akka.DI.Core;
using Common.Logging;

namespace GladMMO
{
	[EntityActorMessageHandler(typeof(DefaultWorldActor))]
	public sealed class WorldActorPlayerCreateEntityActorMessageHandler : BaseEntityActorMessageHandler<WorldActorState, CreateGameObjectEntityActorMessage>
	{
		private ILog Logger { get; }

		private IDependencyResolver Resolver { get; }

		private IEntityGuidMappable<IActorRef> ActorRefMappable { get; }

		private IReadonlyEntityGuidMappable<IEntityDataFieldContainer> EntityDataMappable { get; }

		public WorldActorPlayerCreateEntityActorMessageHandler([NotNull] ILog logger,
			[NotNull] IDependencyResolver resolver,
			[NotNull] IEntityGuidMappable<IActorRef> actorRefMappable,
			[NotNull] IReadonlyEntityGuidMappable<IEntityDataFieldContainer> entityDataMappable)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			Resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
			ActorRefMappable = actorRefMappable ?? throw new ArgumentNullException(nameof(actorRefMappable));
			EntityDataMappable = entityDataMappable ?? throw new ArgumentNullException(nameof(entityDataMappable));
		}

		protected override void HandleMessage(EntityActorMessageContext messageContext, WorldActorState state, CreateGameObjectEntityActorMessage message)
		{
			if(message.EntityGuid.EntityType != EntityType.GameObject)
				throw new InvalidOperationException($"Tried to create GameObject Actor for non-GameObject Entity: {message.EntityGuid}");

			//Create the actor and tell it to initialize.
			IActorRef actorRef = state.WorldActorFactory.ActorOf(Resolver.Create<DefaultPlayerEntityActor>(), message.EntityGuid.RawGuidValue.ToString());
			actorRef.Tell(new EntityActorStateInitializeMessage<DefaultEntityActorStateContainer>(new DefaultEntityActorStateContainer(EntityDataMappable.RetrieveEntity(message.EntityGuid), message.EntityGuid)));

			ActorRefMappable.AddObject(message.EntityGuid, actorRef);

			if(Logger.IsInfoEnabled)
				Logger.Info($"Created Player Actor: {typeof(DefaultPlayerEntityActor)} for Entity: {message.EntityGuid}");
		}
	}
}
