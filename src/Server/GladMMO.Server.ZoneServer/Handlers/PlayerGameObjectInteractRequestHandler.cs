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
	public sealed class PlayerGameObjectInteractRequestHandler : ControlledEntityRequestHandler<ClientInteractGameObjectRequestPayload>
	{
		private IReadonlyEntityGuidMappable<IActorRef> ActorReferenceMappable { get; }

		/// <inheritdoc />
		public PlayerGameObjectInteractRequestHandler(
			ILog logger,
			IReadonlyConnectionEntityCollection connectionIdToEntityMap,
			IContextualResourceLockingPolicy<NetworkEntityGuid> lockingPolicy,
			[NotNull] IReadonlyEntityGuidMappable<IActorRef> actorReferenceMappable)
			: base(logger, connectionIdToEntityMap, lockingPolicy)
		{
			ActorReferenceMappable = actorReferenceMappable ?? throw new ArgumentNullException(nameof(actorReferenceMappable));
		}

		/// <inheritdoc />
		protected override Task HandleMessage(IPeerSessionMessageContext<GameServerPacketPayload> context, ClientInteractGameObjectRequestPayload payload, NetworkEntityGuid guid)
		{
			if (!ActorReferenceMappable.ContainsKey(payload.TargetGameObjectGuid))
			{
				if(Logger.IsWarnEnabled)
					Logger.Warn($"Client: {guid} attempted to interact with unknown actor {payload.TargetGameObjectGuid}.");
			}
			else
			{
				ProjectVersionStage.AssertBeta();
				//TODO: Race condition where THIS CAN FAIL since IActorRefs are removed currently.
				IActorRef interactable = ActorReferenceMappable.RetrieveEntity(payload.TargetGameObjectGuid);
				IActorRef playerRef = ActorReferenceMappable.RetrieveEntity(guid);

				//Important to indicate that the player itself is sending it.
				interactable.Tell(new InteractWithEntityActorMessage(guid), playerRef);
			}

			return Task.CompletedTask;
		}
	}
}