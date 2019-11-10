using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;

namespace GladMMO
{
	public sealed class DefaultPlayerEntityActor : BaseEntityActor<DefaultPlayerEntityActor, DefaultPlayerEntityActorState>
	{
		public DefaultPlayerEntityActor(IEntityActorMessageRouteable<DefaultPlayerEntityActor, DefaultPlayerEntityActorState> messageRouter, ILog logger) 
			: base(messageRouter, logger)
		{

		}
	}
}
