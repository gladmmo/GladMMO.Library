using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;

namespace GladMMO
{
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class PlayerGameObjectInteractRequestHandler : ControlledEntityRequestHandler<ClientInteractGameObjectRequestPayload>
	{
		private IReadonlyEntityGuidMappable<IWorldInteractable> WorldInteractables { get; }

		/// <inheritdoc />
		public PlayerGameObjectInteractRequestHandler(
			ILog logger,
			IReadonlyConnectionEntityCollection connectionIdToEntityMap,
			IContextualResourceLockingPolicy<NetworkEntityGuid> lockingPolicy,
			[NotNull] IReadonlyEntityGuidMappable<IWorldInteractable> worldInteractables)
			: base(logger, connectionIdToEntityMap, lockingPolicy)
		{
			WorldInteractables = worldInteractables ?? throw new ArgumentNullException(nameof(worldInteractables));
		}

		/// <inheritdoc />
		protected override Task HandleMessage(IPeerSessionMessageContext<GameServerPacketPayload> context, ClientInteractGameObjectRequestPayload payload, NetworkEntityGuid guid)
		{
			if (!WorldInteractables.ContainsKey(payload.TargetGameObjectGuid))
			{
				if(Logger.IsWarnEnabled)
					Logger.Warn($"Client: {guid} attempted to interact with unknown interactable.");
			}
			else
			{
				IWorldInteractable interactable = WorldInteractables.RetrieveEntity(payload.TargetGameObjectGuid);

				ProjectVersionStage.AssertBeta();
				//TODO: We probably shouldn't interact with networked objects off of the main thread without aquiring a resource lock on them
				//TODO: Somewhere, somehow, there should be like a distance check or a check to see if they CAN interact with this object.
				interactable.Interact(guid);
			}

			return Task.CompletedTask;
		}
	}
}