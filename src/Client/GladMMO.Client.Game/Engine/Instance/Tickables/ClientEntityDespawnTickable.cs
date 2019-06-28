using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class ClientEntityDespawnTickable : SharedEntityDespawnTickable<INetworkEntityVisibilityLostEventSubscribable, NetworkEntityVisibilityLostEventArgs>
	{
		public ClientEntityDespawnTickable(INetworkEntityVisibilityLostEventSubscribable subscriptionService, 
			ILog logger, 
			IKnownEntitySet knownEntities) 
			: base(subscriptionService, logger, knownEntities)
		{
		}
	}
}
