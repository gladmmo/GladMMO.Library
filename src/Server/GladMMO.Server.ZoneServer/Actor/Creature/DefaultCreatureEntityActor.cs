using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;

namespace GladMMO
{
	public sealed class DefaultCreatureEntityActor : BaseEntityActor<DefaultCreatureEntityActor, DefaultEntityActorStateContainer>
	{
		public DefaultCreatureEntityActor(IEntityActorMessageRouteable<DefaultCreatureEntityActor, DefaultEntityActorStateContainer> messageRouter, ILog logger) 
			: base(messageRouter, logger)
		{

		}
	}
}
