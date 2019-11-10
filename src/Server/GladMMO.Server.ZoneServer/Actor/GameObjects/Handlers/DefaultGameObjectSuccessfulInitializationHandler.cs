using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	[EntityActorMessageHandler(typeof(GameObjectAvatarPedestalEntityActor))]
	[EntityActorMessageHandler(typeof(GameObjectWorldTeleporterEntityActor))]
	[EntityActorMessageHandler(typeof(DefaultGameObjectEntityActor))]
	public sealed class DefaultGameObjectSuccessfulInitializationHandler : BaseEntityActorMessageHandler<DefaultGameObjectActorState, EntityActorInitializationSuccessMessage>
	{
		protected override void HandleMessage(EntityActorMessageContext messageContext, DefaultGameObjectActorState state, EntityActorInitializationSuccessMessage message)
		{
			//default gameobject behavior is to not be selectable.
			state.EntityData.AddBaseObjectFieldFlag(BaseObjectFieldFlags.UNIT_FLAG_NOT_SELECTABLE);
		}
	}
}
