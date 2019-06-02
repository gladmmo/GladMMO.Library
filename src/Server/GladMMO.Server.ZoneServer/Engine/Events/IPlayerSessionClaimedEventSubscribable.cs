using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public interface IPlayerSessionClaimedEventSubscribable
	{
		event EventHandler<PlayerSessionClaimedEventArgs> OnSuccessfulSessionClaimed;
	}

	public sealed class PlayerSessionClaimedEventArgs : EventArgs
	{
		public NetworkEntityGuid EntityGuid { get; }

		public Vector3 SpawnPosition { get; }

		/// <summary>
		/// Context about the network session.
		/// </summary>
		public PlayerEntitySessionContext SessionContext { get; }

		/// <inheritdoc />
		public PlayerSessionClaimedEventArgs([NotNull] NetworkEntityGuid entityGuid, Vector3 spawnPosition, [NotNull] PlayerEntitySessionContext sessionContext)
		{
			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
			SpawnPosition = spawnPosition;
			SessionContext = sessionContext ?? throw new ArgumentNullException(nameof(sessionContext));
		}
	}
}
