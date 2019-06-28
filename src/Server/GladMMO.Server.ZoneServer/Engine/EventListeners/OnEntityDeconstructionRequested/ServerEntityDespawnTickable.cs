using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class ServerEntityDespawnTickable : SharedEntityDespawnTickable<IEntityDeconstructionRequestedEventSubscribable, EntityDeconstructionRequestedEventArgs>
	{
		public ServerEntityDespawnTickable(IEntityDeconstructionRequestedEventSubscribable subscriptionService, 
			ILog logger, 
			IKnownEntitySet knownEntities) 
			: base(subscriptionService, logger, knownEntities)
		{
		}
	}
}
