using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Nito.AsyncEx;

namespace GladMMO
{
	public sealed class PlayerSessionDestructor : IObjectDestructorable<PlayerSessionDeconstructionContext>
	{
		private IObjectDestructorable<PlayerEntityDestructionContext> PlayerEntityDestructor { get; }

		private IConnectionEntityCollection ConnectionToEntityMap { get; }

		private ISessionCollection SessionCollection { get; }

		/// <inheritdoc />
		public PlayerSessionDestructor(
			[NotNull] IObjectDestructorable<PlayerEntityDestructionContext> playerEntityDestructor,
			[NotNull] IConnectionEntityCollection connectionToEntityMap,
			[NotNull] ISessionCollection sessionCollection)
		{
			PlayerEntityDestructor = playerEntityDestructor ?? throw new ArgumentNullException(nameof(playerEntityDestructor));
			ConnectionToEntityMap = connectionToEntityMap ?? throw new ArgumentNullException(nameof(connectionToEntityMap));
			SessionCollection = sessionCollection ?? throw new ArgumentNullException(nameof(sessionCollection));
		}

		/// <inheritdoc />
		public bool Destroy([NotNull] PlayerSessionDeconstructionContext obj)
		{
			if(obj == null) throw new ArgumentNullException(nameof(obj));

			//If the connection doesn't own an entity then we have nothing to do here.
			if(!OwnsEntityToDestruct(obj.ConnectionId))
				return false;

			NetworkEntityGuid entityGuid = ConnectionToEntityMap[obj.ConnectionId];

			//An entity exists, so we just pass it along but we also must remove it from the map afterwards.
			bool result = PlayerEntityDestructor.Destroy(new PlayerEntityDestructionContext(entityGuid));

			//We need to unregister BOTH of these collections, session collection orginally was cleanedup
			//immediately on disconnect. Now it is not and must be done here.
			ConnectionToEntityMap.Remove(obj.ConnectionId);
			SessionCollection.Unregister(obj.ConnectionId);

			return result;
		}

		/// <inheritdoc />
		public bool OwnsEntityToDestruct(int connectionId)
		{
			//The idea here is that the destructor just checks to see if
			//an entity is owned by this session/connection. If it is then it
			//will send the guid off to the real service that does destruction.
			return ConnectionToEntityMap.ContainsKey(connectionId);
		}
	}
}
