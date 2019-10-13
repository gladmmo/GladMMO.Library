using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Marker interface
	/// </summary>
	public interface IEntityActorMessageRouteable
	{

	}

	/// <summary>
	/// Contract for types that can route messages.
	/// </summary>
	/// <typeparam name="TEntityStateType"></typeparam>
	/// <typeparam name="TActorType">This generic parameter exists mostly for IoC magic.</typeparam>
	public interface IEntityActorMessageRouteable<in TActorType, in TEntityStateType> : IEntityActorMessageRouteable
		where TEntityStateType : IEntityActorStateContainable
	{
		bool RouteMessage(EntityActorMessageContext messageContext, TEntityStateType state, EntityActorMessage message);
	}
}
