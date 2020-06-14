using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using Glader.Essentials;
using Nito.AsyncEx;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class EntitySpawnTickable : SharedEntitySpawnTickable<INetworkEntityVisibleEventSubscribable, NetworkEntityNowVisibleEventArgs>
	{
		public EntitySpawnTickable(INetworkEntityVisibleEventSubscribable subscriptionService, 
			ILog logger, 
			IKnownEntitySet knownEntities,
			IReadonlyEntityGuidMappable<AsyncLock> lockMappable) 
			: base(subscriptionService, logger, knownEntities, lockMappable)
		{

		}
	}
}
