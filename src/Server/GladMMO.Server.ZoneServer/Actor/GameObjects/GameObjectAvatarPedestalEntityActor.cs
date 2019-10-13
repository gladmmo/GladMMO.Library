using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;

namespace GladMMO
{
	public sealed class GameObjectAvatarPedestalEntityActor : BaseEntityActor<GameObjectAvatarPedestalEntityActor, BehaviourGameObjectState<AvatarPedestalInstanceModel>>
	{
		public GameObjectAvatarPedestalEntityActor(IEntityActorMessageRouteable<GameObjectAvatarPedestalEntityActor, BehaviourGameObjectState<AvatarPedestalInstanceModel>> messageRouter, ILog logger) 
			: base(messageRouter, logger)
		{

		}
	}
}
