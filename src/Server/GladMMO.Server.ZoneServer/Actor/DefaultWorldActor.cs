using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;

namespace GladMMO
{
	public sealed class DefaultWorldActor : BaseEntityActor<DefaultWorldActor, WorldActorState>
	{
		public DefaultWorldActor(IEntityActorMessageRouteable<DefaultWorldActor, WorldActorState> messageRouter, ILog logger) 
			: base(messageRouter, logger)
		{

		}
	}
}
