using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	[EntityActorMessageHandler(typeof(DefaultCreatureEntityActor))]
	[EntityActorMessageHandler(typeof(DefaultPlayerEntityActor))]
	[EntityActorMessageHandler(typeof(DefaultGameObjectEntityActor))]
	public sealed class EntityInterestRemoveMessageHandler : BaseEntityActorMessageHandler<NetworkedObjectActorState, EntityInterestRemoveMessage>
	{
		protected override void HandleMessage(EntityActorMessageContext messageContext, NetworkedObjectActorState state, EntityInterestRemoveMessage message)
		{
			state.Interest.Remove(message.Entity);
		}
	}
}
