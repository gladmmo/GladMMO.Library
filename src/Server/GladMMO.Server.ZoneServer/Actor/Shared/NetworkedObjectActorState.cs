using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Base type for all networked object actor states.
	/// </summary>
	public class NetworkedObjectActorState : DefaultEntityActorStateContainer
	{
		/// <summary>
		/// Entity's interest collection.
		/// </summary>
		public InterestCollection Interest { get; }

		public NetworkedObjectActorState(IEntityDataFieldContainer entityData, ObjectGuid entityGuid, [NotNull] InterestCollection interest) 
			: base(entityData, entityGuid)
		{
			Interest = interest ?? throw new ArgumentNullException(nameof(interest));
		}
	}
}
