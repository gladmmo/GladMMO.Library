using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	[EntityActorMessageHandler(typeof(DefaultCreatureEntityActor))]
	[EntityActorMessageHandler(typeof(DefaultPlayerEntityActor))]
	[EntityActorMessageHandler(typeof(DefaultGameObjectEntityActor))]
	public sealed class EntityInterestGainedMessageHandler : BaseEntityActorMessageHandler<DefaultEntityActorStateContainer, EntityInterestGainedMessage>
	{
		protected override void HandleMessage(EntityActorMessageContext messageContext, DefaultEntityActorStateContainer state, EntityInterestGainedMessage message)
		{

		}
	}
}
