using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Common.Logging;

namespace GladMMO
{
	[EntityActorMessageHandler(typeof(GameObjectAvatarPedestalEntityActor))]
	public sealed class WorldTeleporterInteractionMessageHandler : BaseEntityActorMessageHandler<BehaviourGameObjectState<WorldTeleporterInstanceModel>, InteractWithEntityActorMessage>
	{
		private ILog Logger { get; }

		public WorldTeleporterInteractionMessageHandler([NotNull] ILog logger, [NotNull] IZoneServerToGameServerClient zoneServerDataClient)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void HandleMessage(EntityActorMessageContext messageContext, BehaviourGameObjectState<WorldTeleporterInstanceModel> state, InteractWithEntityActorMessage message)
		{
			if(Logger.IsDebugEnabled)
				Logger.Debug($"Entity: {message.EntityInteracting} Interacted with World Teleporter: {state.EntityGuid}");

			//Only players should be able to interact with this.
			if(message.EntityInteracting.EntityType != EntityType.Player)
				return;

			//Right now there is no validation, just teleport them.
			messageContext.Sender.Tell(new WorldTeleportPlayerEntityActorMessage(state.BehaviourData.RemoteSpawnPointId));
		}
	}
}
