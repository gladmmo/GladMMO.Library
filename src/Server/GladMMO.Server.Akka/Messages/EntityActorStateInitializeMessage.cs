using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IEntityActorStateInitializeMessage<out TStateType>
	{
		TStateType State { get; }
	}

	public sealed class EntityActorStateInitializeMessage<TStateType> : EntityActorMessage, IEntityActorStateInitializeMessage<TStateType>
		where TStateType : class, IEntityActorStateContainable
	{
		public TStateType State { get; }

		public EntityActorStateInitializeMessage(TStateType state)
		{
			State = state ?? throw new ArgumentNullException(nameof(state));
		}
	}
}
