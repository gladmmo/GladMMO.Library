using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;

namespace GladMMO
{
	public sealed class DefaultCreatureEntityActor : BaseEntityActor<DefaultCreatureEntityActor, DefaultCreatureActorState>
	{
		public DefaultCreatureEntityActor(IEntityActorMessageRouteable<DefaultCreatureEntityActor, DefaultCreatureActorState> messageRouter, ILog logger) 
			: base(messageRouter, logger)
		{

		}
	}
}
