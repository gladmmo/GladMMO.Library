using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IPlayerGroupJoinedEventSubscribable
	{
		event EventHandler<PlayerJoinedGroupEventArgs> OnPlayerJoinedGroup;
	}

	public sealed class PlayerJoinedGroupEventArgs : EventArgs
	{
		public ObjectGuid PlayerGuid { get; }

		/// <inheritdoc />
		public PlayerJoinedGroupEventArgs([NotNull] ObjectGuid playerGuid)
		{
			PlayerGuid = playerGuid ?? throw new ArgumentNullException(nameof(playerGuid));
		}
	}
}
