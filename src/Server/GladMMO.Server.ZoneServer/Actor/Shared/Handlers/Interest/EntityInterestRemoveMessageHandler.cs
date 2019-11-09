using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	[EntityActorMessageHandler(typeof(DefaultCreatureEntityActor))]
	[EntityActorMessageHandler(typeof(DefaultPlayerEntityActor))]
	[EntityActorMessageHandler(typeof(DefaultGameObjectEntityActor))]
	public sealed class EntityInterestRemoveMessageHandler : BaseEntityActorMessageHandler<DefaultEntityActorStateContainer, EntityInterestRemoveMessage>
	{
		protected override void HandleMessage(EntityActorMessageContext messageContext, DefaultEntityActorStateContainer state, EntityInterestRemoveMessage message)
		{

		}
	}
}
