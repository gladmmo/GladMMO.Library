using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;

namespace GladMMO
{
	public sealed class DefaultPlayerEntityActor : BaseEntityActor<DefaultPlayerEntityActor, NetworkedObjectActorState>
	{
		public DefaultPlayerEntityActor(IEntityActorMessageRouteable<DefaultPlayerEntityActor, NetworkedObjectActorState> messageRouter, ILog logger) 
			: base(messageRouter, logger)
		{

		}
	}
}
