using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class InitializePlayerMovementDataEventListener : BaseSingleEventListenerInitializable<IPlayerSessionClaimedEventSubscribable, PlayerSessionClaimedEventArgs>
	{
		private IEntityGuidMappable<IMovementData> MovementDataMappable { get; }

		public InitializePlayerMovementDataEventListener(IPlayerSessionClaimedEventSubscribable subscriptionService,
			[NotNull] IEntityGuidMappable<IMovementData> movementDataMappable) 
			: base(subscriptionService)
		{
			MovementDataMappable = movementDataMappable ?? throw new ArgumentNullException(nameof(movementDataMappable));
		}

		protected override void OnEventFired(object source, PlayerSessionClaimedEventArgs args)
		{
			//TODO: We should probably handle initial movement data differently.
			MovementDataMappable.AddObject(args.EntityGuid, new PositionChangeMovementData(0, args.SpawnPosition, Vector2.zero, 0.0f));
		}
	}
}
