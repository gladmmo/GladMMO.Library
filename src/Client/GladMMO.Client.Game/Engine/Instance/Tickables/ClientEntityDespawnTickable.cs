using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Glader.Essentials;
using Nito.AsyncEx;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class ClientEntityDespawnTickable : SharedEntityDespawnTickable<INetworkEntityVisibilityLostEventSubscribable, NetworkEntityVisibilityLostEventArgs>
	{
		public ClientEntityDespawnTickable(INetworkEntityVisibilityLostEventSubscribable subscriptionService, 
			ILog logger, 
			IKnownEntitySet knownEntities, 
			IReadonlyEntityGuidMappable<AsyncLock> lockMappable) 
			: base(subscriptionService, logger, knownEntities, lockMappable)
		{
		}
	}
}
