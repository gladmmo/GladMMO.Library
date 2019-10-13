using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;

namespace GladMMO
{
	public sealed class GameObjectWorldTeleporterEntityActor : BaseEntityActor<GameObjectWorldTeleporterEntityActor, BehaviourGameObjectState<WorldTeleporterInstanceModel>>
	{
		public GameObjectWorldTeleporterEntityActor(IEntityActorMessageRouteable<GameObjectWorldTeleporterEntityActor, BehaviourGameObjectState<WorldTeleporterInstanceModel>> messageRouter, ILog logger) 
			: base(messageRouter, logger)
		{

		}
	}
}
