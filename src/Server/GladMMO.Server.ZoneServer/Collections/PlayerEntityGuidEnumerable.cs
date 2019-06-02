using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace GladMMO
{
	public interface IPlayerEntityGuidEnumerable : IEnumerable<NetworkEntityGuid>
	{

	}

	public sealed class PlayerEntityGuidEnumerable : IPlayerEntityGuidEnumerable
	{
		private ISessionCollection ConnectedSessions { get; }

		private IReadonlyConnectionEntityCollection ConnectionEntities { get; }

		/// <inheritdoc />
		public PlayerEntityGuidEnumerable([NotNull] ISessionCollection connectedSessions, [NotNull] IReadonlyConnectionEntityCollection connectionEntities)
		{
			ConnectedSessions = connectedSessions ?? throw new ArgumentNullException(nameof(connectedSessions));
			ConnectionEntities = connectionEntities ?? throw new ArgumentNullException(nameof(connectionEntities));
		}

		/// <inheritdoc />
		public IEnumerator<NetworkEntityGuid> GetEnumerator()
		{
			//Provides an enumerator that will produce all NetworkEntityGuids associated with players.
			foreach(var session in ConnectedSessions)
			{
				//If the session with the ID does not have an entity associated with it then a player
				//for the session is not in the world
				if(!ConnectionEntities.ContainsKey(session.Details.ConnectionId))
					continue;

				yield return ConnectionEntities[session.Details.ConnectionId];
			}
		}

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
