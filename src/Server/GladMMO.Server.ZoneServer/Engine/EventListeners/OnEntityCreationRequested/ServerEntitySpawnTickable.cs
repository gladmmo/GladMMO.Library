using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;

namespace GladMMO
{
	//So, this will not actually run until the next update tick
	//That means OTHER subscribers to the creation request event can run and the expected order
	//can be assured that they will run BEFORE this runs.
	[GameInitializableOrdering(2)]
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class ServerEntitySpawnTickable : SharedEntitySpawnTickable<IEntityCreationRequestedEventSubscribable, EntityCreationRequestedEventArgs>
	{
		public ServerEntitySpawnTickable(IEntityCreationRequestedEventSubscribable subscriptionService, ILog logger, IKnownEntitySet knownEntities) 
			: base(subscriptionService, logger, knownEntities)
		{

		}
	}
}
