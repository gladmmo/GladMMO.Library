using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IPlayerWorldTeleporterRequestedEventSubscribable
	{
		event EventHandler<PlayerSessionClaimedEventArgs> OnWorldTeleporterRequested;
	}

	public sealed class PlayerWorldTeleporterRequestEventArgs : EventArgs
	{
		public int WorldTeleporterEntryId { get; }

		public PlayerWorldTeleporterRequestEventArgs(int worldTeleporterEntryId)
		{
			if (worldTeleporterEntryId <= 0) throw new ArgumentOutOfRangeException(nameof(worldTeleporterEntryId));

			WorldTeleporterEntryId = worldTeleporterEntryId;
		}
	}
}
