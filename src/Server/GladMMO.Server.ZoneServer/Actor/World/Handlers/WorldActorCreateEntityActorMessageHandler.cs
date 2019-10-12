using System;
using System.Collections.Generic;
using System.Text;
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
			if(Logger.IsWarnEnabled)
				Logger.Warn($"WorldActor recieved entity actor creation request for Actor: {message.EntityGuid}");
		}
	}
}
