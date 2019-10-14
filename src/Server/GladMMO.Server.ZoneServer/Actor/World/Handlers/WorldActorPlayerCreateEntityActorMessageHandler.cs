using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using Akka.DI.Core;
using Common.Logging;

namespace GladMMO
{
	[EntityActorMessageHandler(typeof(DefaultWorldActor))]
	public sealed class WorldActorPlayerCreateEntityActorMessageHandler : BaseEntityActorMessageHandler<WorldActorState, CreatePlayerEntityActorMessage>
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

		protected override void HandleMessage(EntityActorMessageContext messageContext, WorldActorState state, CreatePlayerEntityActorMessage message)
		{
			if(message.EntityGuid.EntityType != EntityType.Player)
				throw new InvalidOperationException($"Tried to create Player Actor for non-Player Entity: {message.EntityGuid}");

			try
			{
				IActorRef actor = CreateActor(state, message);

				//If it succeeded, add the death watch message
				state.DeathWatchService.WatchWith(actor, new KillPlayerActorMessage(message.EntityGuid));
			}
			catch (Exception e)
			{
				if(Logger.IsErrorEnabled)
					Logger.Error($"Failed to create Actor: {e.Message}\n\nStack: {e.StackTrace}");
				throw;
			}
		}

		private IActorRef CreateActor(WorldActorState state, CreatePlayerEntityActorMessage message)
		{
			//Create the actor and tell it to initialize.
			IActorRef actorRef = state.WorldActorFactory.ActorOf(Resolver.Create<DefaultPlayerEntityActor>(), message.EntityGuid.RawGuidValue.ToString());
			actorRef.Tell(new EntityActorStateInitializeMessage<DefaultEntityActorStateContainer>(new DefaultEntityActorStateContainer(EntityDataMappable.RetrieveEntity(message.EntityGuid), message.EntityGuid)));

			ActorRefMappable.AddObject(message.EntityGuid, actorRef);

			if (Logger.IsInfoEnabled)
				Logger.Info($"Created Player Actor: {typeof(DefaultPlayerEntityActor)} for Entity: {message.EntityGuid}");

			return actorRef;
		}
	}
}
