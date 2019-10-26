using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IGuildStatusChangedEventSubscribable
	{
		event EventHandler<GuildStatusChangedEventArgs> OnGuildStatusChanged;
	}

	public sealed class GuildStatusChangedEventArgs : EventArgs
	{
		/// <summary>
		/// Indicates if the player is now guildless.
		/// </summary>
		public bool IsGuildless => GuildId == 0;

		/// <summary>
		/// The guid of the entity that has a changing guild status.
		/// </summary>
		public NetworkEntityGuid EntityGuid { get; }

		/// <summary>
		/// The current id of the guild.
		/// </summary>
		public int GuildId { get; }

		public GuildStatusChangedEventArgs([NotNull] NetworkEntityGuid entityGuid, int guildId)
		{
			if (guildId < 0) throw new ArgumentOutOfRangeException(nameof(guildId));

			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
			GuildId = guildId;
		}
	}
}
