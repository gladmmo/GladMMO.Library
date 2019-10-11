using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Contract for types that can route messages.
	/// </summary>
	/// <typeparam name="TEntityStateType"></typeparam>
	public interface IEntityActorMessageRouteable<in TEntityStateType>
		where TEntityStateType : IEntityActorStateContainable
	{
		void RouteMessage(EntityActorMessageContext messageContext, TEntityStateType state, EntityActorMessage message);
	}
}
