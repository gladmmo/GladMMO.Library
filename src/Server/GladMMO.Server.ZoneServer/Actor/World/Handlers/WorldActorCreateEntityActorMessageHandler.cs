using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using Common.Logging;

namespace GladMMO
{
	[EntityActorMessageHandler(typeof(DefaultWorldActor))]
	public sealed class WorldActorCreateEntityActorMessageHandler : BaseEntityActorMessageHandler<WorldActorState, CreateEntityActorMessage>
	{
		private ILog Logger { get; }

		public WorldActorCreateEntityActorMessageHandler([NotNull] ILog logger)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void HandleMessage(EntityActorMessageContext messageContext, WorldActorState state, CreateEntityActorMessage message)
		{
			//Below we just forward to the appropriate handler.
			switch (message.EntityGuid.EntityType)
			{
				case EntityType.Player:
					messageContext.Entity.Tell(new CreatePlayerEntityActorMessage(message.EntityGuid));
					break;
				case EntityType.GameObject:
					messageContext.Entity.Tell(new CreateGameObjectEntityActorMessage(message.EntityGuid));
					break;
				case EntityType.Creature:
					messageContext.Entity.Tell(new CreateCreatureEntityActorMessage(message.EntityGuid));
					break;
				default:
					throw new ArgumentOutOfRangeException($"Cannot handle EntityType: {message.EntityGuid.EntityType}");
			}
		}
	}
}
