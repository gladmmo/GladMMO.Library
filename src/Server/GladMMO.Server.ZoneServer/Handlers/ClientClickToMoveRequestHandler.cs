using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;
using UnityEngine;

namespace GladMMO
{
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class ClientClickToMoveRequestHandler : ControlledEntityRequestHandler<ClientSetClickToMovePathRequestPayload>
	{
		private IEntityGuidMappable<IMovementData> MovementDataMap { get; }

		private IEntityGuidMappable<IMovementGenerator<GameObject>> MovementGenerator { get; }

		private IReadonlyNetworkTimeService TimeService { get; }

		public ClientClickToMoveRequestHandler(ILog logger, 
			IReadonlyConnectionEntityCollection connectionIdToEntityMap, 
			IContextualResourceLockingPolicy<NetworkEntityGuid> lockingPolicy, 
			IReadonlyNetworkTimeService timeService, 
			IEntityGuidMappable<IMovementGenerator<GameObject>> movementGenerator, 
			IEntityGuidMappable<IMovementData> movementDataMap) 
			: base(logger, connectionIdToEntityMap, lockingPolicy)
		{
			TimeService = timeService;
			MovementGenerator = movementGenerator;
			MovementDataMap = movementDataMap;
		}

		protected override Task HandleMessage(IPeerSessionMessageContext<GameServerPacketPayload> context, ClientSetClickToMovePathRequestPayload payload, NetworkEntityGuid guid)
		{
			MovementGenerator.ReplaceObject(guid, new PathBasedMovementGenerator(payload.PathData));

			//TODO: Validate the timestamp
			MovementDataMap.ReplaceObject(guid, payload.PathData);

			return Task.CompletedTask;
		}
	}
}
