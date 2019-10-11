using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IEntityActorMessageHandler<in TEntityStateType, in TEntityMessageType>
		where TEntityStateType : IEntityActorStateContainable
		where TEntityMessageType : EntityActorMessage
	{
		void HandleMessage(EntityActorMessageContext messageContext, TEntityStateType state, TEntityMessageType message);
	}
}
