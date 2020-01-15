using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Common.Logging;
using GladNet;
using JetBrains.Annotations;
using UnityEngine;

namespace GladMMO
{
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class ClientSetClickToMovePathRequestPayloadHandler : ControlledEntityRequestHandler<ClientSetClickToMovePathRequestPayload>
	{
		private IEntityGuidMappable<IMovementData> MovementDataMap { get; }

		private IEntityGuidMappable<IMovementGenerator<GameObject>> MovementGenerator { get; }

		private IReadonlyNetworkTimeService TimeService { get; }

		private IReadonlyEntityGuidMappable<IActorRef> ActorReferenceMappable { get; }

		private IEntityGuidMappable<WorldTransform> TransformMap { get; }

		/// <inheritdoc />
		public ClientSetClickToMovePathRequestPayloadHandler(
			[NotNull] ILog logger, 
			[NotNull] IReadonlyConnectionEntityCollection connectionIdToEntityMap, 
			[NotNull] IEntityGuidMappable<IMovementData> movementDataMap,
			IContextualResourceLockingPolicy<NetworkEntityGuid> lockingPolicy,
			[NotNull] IEntityGuidMappable<IMovementGenerator<GameObject>> movementGenerator,
			[NotNull] IReadonlyNetworkTimeService timeService,
			[NotNull] IReadonlyEntityGuidMappable<IActorRef> actorReferenceMappable,
			[NotNull] IEntityGuidMappable<WorldTransform> transformMap) 
			: base(logger, connectionIdToEntityMap, lockingPolicy)
		{
			MovementDataMap = movementDataMap ?? throw new ArgumentNullException(nameof(movementDataMap));
			MovementGenerator = movementGenerator ?? throw new ArgumentNullException(nameof(movementGenerator));
			TimeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
			ActorReferenceMappable = actorReferenceMappable ?? throw new ArgumentNullException(nameof(actorReferenceMappable));
			TransformMap = transformMap ?? throw new ArgumentNullException(nameof(transformMap));
		}

		/// <inheritdoc />
		protected override Task HandleMessage(IPeerSessionMessageContext<GameServerPacketPayload> context, ClientSetClickToMovePathRequestPayload payload, NetworkEntityGuid guid)
		{
			try
			{
				IMovementGenerator<GameObject> generator = MovementGenerator.RetrieveEntity(guid);

				IMovementData movementData = MovementDataMap.RetrieveEntity(guid);
				PathBasedMovementData changeMovementData = BuildPathData(payload, generator, movementData, guid);

				//If it doesn't have more one point reject it
				if (changeMovementData.MovementPath.Count < 2)
					return Task.CompletedTask;

				MovementDataMap.ReplaceObject(guid, changeMovementData);

				IActorRef playerActorRef = ActorReferenceMappable.RetrieveEntity(guid);

				Vector3 direction3D = (changeMovementData.MovementPath[1] - changeMovementData.MovementPath[0]);
				Vector2 direction2D = new Vector2(direction3D.x, direction3D.z).normalized;
				playerActorRef.TellSelf(new PlayerMovementStateChangedMessage(direction2D));

				//If the generator is running, we should use its initial position instead of the last movement data's position.
				MovementGenerator.ReplaceObject(guid, new PathMovementGenerator(changeMovementData));
			}
			catch(Exception e)
			{
				if(Logger.IsErrorEnabled)
					Logger.Error($"Failed to update MovementData for GUID: {guid} Reason: {e.Message}");

				throw;
			}

			return Task.CompletedTask;
		}

		private PathBasedMovementData BuildPathData(ClientSetClickToMovePathRequestPayload payload, IMovementGenerator<GameObject> generator, IMovementData originalMovementData, NetworkEntityGuid guid)
		{
			//TODO: Sanity check timestamp and position.
			//So, originally we used authorative time and position but now we semi-trust the client.
			//We need to verify the send timestamp is not too far off and also sanity check the position too.
			Vector3[] path = payload.PathData.MovementPath.ToArrayTryAvoidCopy();

			Vector3[] fullPath = new Vector3[path.Length + 1];

			//If we haven't even started the last movement generator from the last click.
			if (generator.isStarted)
			{
				//Force the current position as the start point of the path.
				fullPath[0] = generator.CurrentPosition;
			}
			else
			{
				//We need to use the last WorldTransform because
				//the last movement generator has not started.
				WorldTransform transform = TransformMap.RetrieveEntity(guid);
				fullPath[0] = new Vector3(transform.PositionX, transform.PositionY, transform.PositionZ);
			}
			
			Array.Copy(path, 0, fullPath, 1, path.Length);

			return new PathBasedMovementData(fullPath, TimeService.CurrentLocalTime);
		}
	}
}
