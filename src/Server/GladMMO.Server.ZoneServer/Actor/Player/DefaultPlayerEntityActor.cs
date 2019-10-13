using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;

namespace GladMMO
{
	public sealed class DefaultPlayerEntityActor : BaseEntityActor<DefaultPlayerEntityActor, DefaultEntityActorStateContainer>
	{
		public DefaultPlayerEntityActor(IEntityActorMessageRouteable<DefaultPlayerEntityActor, DefaultEntityActorStateContainer> messageRouter, ILog logger) 
			: base(messageRouter, logger)
		{

		}
	}
}
