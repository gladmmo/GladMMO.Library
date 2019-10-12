using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IEntityActorMessageHandler<in TEntityStateType, in TEntityMessageType>
		where TEntityStateType : IEntityActorStateContainable
		where TEntityMessageType : EntityActorMessage
	{
		/// <summary>
		/// Non-reflection based message type getter.
		/// May not match the signature of the HandleMessage method.
		/// </summary>
		Type MessageType { get; }

		void HandleMessage(EntityActorMessageContext messageContext, TEntityStateType state, TEntityMessageType message);
	}
}
