using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IPlayerGroupLeftEventSubscribable
	{
		event EventHandler<PlayerLeftGroupEventArgs> OnPlayerLeftGroup;
	}

	public sealed class PlayerLeftGroupEventArgs : EventArgs
	{
		public NetworkEntityGuid PlayerGuid { get; }

		/// <inheritdoc />
		public PlayerLeftGroupEventArgs([NotNull] NetworkEntityGuid playerGuid)
		{
			PlayerGuid = playerGuid ?? throw new ArgumentNullException(nameof(playerGuid));
		}
	}
}
