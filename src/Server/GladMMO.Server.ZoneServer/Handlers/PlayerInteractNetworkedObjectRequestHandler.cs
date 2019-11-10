using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Common.Logging;
using GladNet;

namespace GladMMO
{
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class PlayerInteractNetworkedObjectRequestHandler : ControlledEntityRequestHandler<ClientInteractNetworkedObjectRequestPayload>
	{
		private IReadonlyEntityGuidMappable<IActorRef> ActorReferenceMappable { get; }

		/// <inheritdoc />
		public PlayerInteractNetworkedObjectRequestHandler(
			ILog logger,
			IReadonlyConnectionEntityCollection connectionIdToEntityMap,
			IContextualResourceLockingPolicy<NetworkEntityGuid> lockingPolicy,
			[NotNull] IReadonlyEntityGuidMappable<IActorRef> actorReferenceMappable)
			: base(logger, connectionIdToEntityMap, lockingPolicy)
		{
			ActorReferenceMappable = actorReferenceMappable ?? throw new ArgumentNullException(nameof(actorReferenceMappable));
		}

		/// <inheritdoc />
		protected override Task HandleMessage(IPeerSessionMessageContext<GameServerPacketPayload> context, ClientInteractNetworkedObjectRequestPayload payload, NetworkEntityGuid guid)
		{
			//Special case here that indicates the client wants to clear their target.
			if (payload.TargetObjectGuid == NetworkEntityGuid.Empty)
			{
				IActorRef playerRef = ActorReferenceMappable.RetrieveEntity(guid);
				playerRef.Tell(new SetEntityActorTargetMessage(payload.TargetObjectGuid));
				//Just send the empty set target to the player entity
				return Task.CompletedTask;
			}

			if (!ActorReferenceMappable.ContainsKey(payload.TargetObjectGuid))
			{
				if(Logger.IsWarnEnabled)
					Logger.Warn($"Client: {guid} attempted to interact with unknown actor {payload.TargetObjectGuid}.");
			}
			else
			{
				IActorRef interactable = ActorReferenceMappable.RetrieveEntity(payload.TargetObjectGuid);
				IActorRef playerRef = ActorReferenceMappable.RetrieveEntity(guid);

				switch (payload.InteractionType)
				{
					case ClientInteractNetworkedObjectRequestPayload.InteractType.Interaction:
						//Important to indicate that the player itself is sending it.
						interactable.Tell(new InteractWithEntityActorMessage(guid), playerRef);
						break;
					case ClientInteractNetworkedObjectRequestPayload.InteractType.Selection:
						//TODO: This can technically cause a race condition for target selection if client selects too fast.
						interactable.Tell(new EntityActorSelectedMessage(guid), playerRef);
						break;
					default:
						throw new ArgumentOutOfRangeException($"Client used unknown interaction Type: {(int)payload.InteractionType}");
				}
				
			}

			return Task.CompletedTask;
		}
	}
}