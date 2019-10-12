using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	//Reflection will search for all: IEntityActorMessageHandler<TEntityStateType, EntityActorMessage>
	//Meaning it will discover all known message handlers and will grab them if they are marked with the appropriate attribute type.
	public abstract class BaseEntityActorMessageHandler<TEntityStateType, TEntityMessageType> : IEntityActorMessageHandler<TEntityStateType, EntityActorMessage>
		where TEntityStateType : IEntityActorStateContainable 
		where TEntityMessageType : EntityActorMessage
	{
		public void HandleMessage(EntityActorMessageContext messageContext, TEntityStateType state, EntityActorMessage message)
		{
			//Assume caller has verified this will work
			//We downcast so the API consumed is simplier/easier.
			//We need the interface to not be specific so it can be discovered, registered and handled.
			HandleMessage(messageContext, state, (TEntityMessageType) message);
		}

		protected abstract void HandleMessage(EntityActorMessageContext messageContext, TEntityStateType state, TEntityMessageType message);
	}
}
