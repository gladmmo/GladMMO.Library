﻿using System;
using System.Collections.Generic;
using System.Text;
using FreecraftCore;

namespace GladMMO
{
	/// <summary>
	/// This is the REAL event to subscribe to
	/// when you want to do this when the local player has actually spawned.
	/// </summary>
	public interface ILocalPlayerSpawnedEventSubscribable
	{
		event EventHandler<LocalPlayerSpawnedEventArgs> OnLocalPlayerSpawned;
	}

	public sealed class LocalPlayerSpawnedEventArgs : EventArgs
	{
		/// <summary>
		/// The entity guid of the local player.
		/// </summary>
		public ObjectGuid EntityGuid { get; }

		/// <inheritdoc />
		public LocalPlayerSpawnedEventArgs([NotNull] ObjectGuid entityGuid)
		{
			//TODO: Validate this.

			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
		}
	}
}
