using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IPlayerWorldTeleporterRequestedEventSubscribable
	{
		event EventHandler<PlayerWorldTeleporterRequestEventArgs> OnWorldTeleporterRequested;
	}

	public sealed class PlayerWorldTeleporterRequestEventArgs : EventArgs
	{
		public NetworkEntityGuid TeleportingPlayer { get; }

		public int WorldTeleporterEntryId { get; }

		public PlayerWorldTeleporterRequestEventArgs(int worldTeleporterEntryId, [NotNull] NetworkEntityGuid teleportingPlayer)
		{
			if (worldTeleporterEntryId <= 0) throw new ArgumentOutOfRangeException(nameof(worldTeleporterEntryId));

			WorldTeleporterEntryId = worldTeleporterEntryId;
			TeleportingPlayer = teleportingPlayer ?? throw new ArgumentNullException(nameof(teleportingPlayer));
		}
	}
}
