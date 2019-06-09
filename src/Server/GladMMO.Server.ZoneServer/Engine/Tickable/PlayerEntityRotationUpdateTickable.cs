using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Common.Logging;
using Glader.Essentials;
using JetBrains.Annotations;
using UnityEngine;

namespace GladMMO
{
	[GameInitializableOrdering(1)]
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class PlayerEntityRotationUpdateTickable : EventQueueBasedTickable<IPlayerRotationChangeEventSubscribable, PlayerRotiationChangeEventArgs>
	{
		private IReadonlyEntityGuidMappable<GameObject> GameObjectMappable { get; }

		protected override void HandleEvent(PlayerRotiationChangeEventArgs args)
		{
			GameObjectMappable.RetrieveEntity(args.EntityGuid).transform.Rotate(Vector3.up, args.Rotation);
		}

		public PlayerEntityRotationUpdateTickable(IPlayerRotationChangeEventSubscribable subscriptionService, 
			ILog logger,
			[NotNull] IReadonlyEntityGuidMappable<GameObject> gameObjectMappable) 
			: base(subscriptionService, true, logger)
		{
			GameObjectMappable = gameObjectMappable ?? throw new ArgumentNullException(nameof(gameObjectMappable));
		}
	}
}
