using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class EntityActorStateInitializeMessage<TStateType> : EntityActorMessage
		where TStateType : class, IEntityActorStateContainable
	{
		public TStateType State { get; }

		public EntityActorStateInitializeMessage(TStateType state)
		{
			State = state ?? throw new ArgumentNullException(nameof(state));
		}
	}
}
