using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using Akka.Actor.Dsl;
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

		private IReadonlyEntityGuidMappable<InterestCollection> InterestMappable { get; }

		public WorldActorPlayerCreateEntityActorMessageHandler([NotNull] ILog logger,
			[NotNull] IDependencyResolver resolver,
			[NotNull] IEntityGuidMappable<IActorRef> actorRefMappable,
			[NotNull] IReadonlyEntityGuidMappable<IEntityDataFieldContainer> entityDataMappable,
			[NotNull] IReadonlyEntityGuidMappable<InterestCollection> interestMappable)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			Resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
			ActorRefMappable = actorRefMappable ?? throw new ArgumentNullException(nameof(actorRefMappable));
			EntityDataMappable = entityDataMappable ?? throw new ArgumentNullException(nameof(entityDataMappable));
			InterestMappable = interestMappable ?? throw new ArgumentNullException(nameof(interestMappable));
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
			DefaultPlayerEntityActor.InitializeActor(actorRef, new NetworkedObjectActorState(EntityDataMappable.RetrieveEntity(message.EntityGuid), message.EntityGuid, InterestMappable.RetrieveEntity(message.EntityGuid)));

			//Actors aren't removed from this
			//they are just replaced with nobody.
			if (ActorRefMappable.ContainsKey(message.EntityGuid))
			{
				//If nobody, all good. Otherwise THIS SHOULD NEVER HAPPEN
				if(ActorRefMappable[message.EntityGuid].IsNobody())
					ActorRefMappable.ReplaceObject(message.EntityGuid, actorRef);
				else
					throw new InvalidOperationException($"World attempted to spawn multiple of the same player actor/"); //this will literally kill the world actor.
			}
			else
				ActorRefMappable.AddObject(message.EntityGuid, actorRef);

			if (Logger.IsInfoEnabled)
				Logger.Info($"Created Player Actor: {typeof(DefaultPlayerEntityActor)} for Entity: {message.EntityGuid}");

			return actorRef;
		}
	}
}
