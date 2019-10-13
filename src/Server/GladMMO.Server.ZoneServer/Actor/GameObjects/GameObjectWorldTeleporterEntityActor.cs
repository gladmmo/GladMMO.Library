using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;

namespace GladMMO
{
	public sealed class GameObjectWorldTeleporterEntityActor :  BaseEntityActor<GameObjectWorldTeleporterEntityActor, WorldTeleporterGameObjectState>
	{
		public GameObjectWorldTeleporterEntityActor(IEntityActorMessageRouteable<GameObjectWorldTeleporterEntityActor, WorldTeleporterGameObjectState> messageRouter, ILog logger) 
			: base(messageRouter, logger)
		{

		}
	}
}
