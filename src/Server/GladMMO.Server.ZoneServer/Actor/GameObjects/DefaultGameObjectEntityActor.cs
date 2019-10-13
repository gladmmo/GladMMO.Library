using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;

namespace GladMMO
{
	public sealed class DefaultGameObjectEntityActor : BaseEntityActor<DefaultGameObjectEntityActor, DefaultGameObjectActorState>
	{
		public DefaultGameObjectEntityActor(IEntityActorMessageRouteable<DefaultGameObjectEntityActor, DefaultGameObjectActorState> messageRouter, ILog logger) 
			: base(messageRouter, logger)
		{

		}
	}
}
