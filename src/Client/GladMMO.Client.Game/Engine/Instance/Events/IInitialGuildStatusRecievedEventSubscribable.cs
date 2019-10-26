using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IInitialGuildStatusRecievedEventSubscribable
	{
		event EventHandler<InitialGuildStatusRecievedEventArgs> OnInitialGuildStatusRecieved;
	}

	public sealed class InitialGuildStatusRecievedEventArgs : EventArgs
	{
		public int GuildId { get; }

		public string GuildName { get; }

		public InitialGuildStatusRecievedEventArgs(int guildId, [NotNull] string guildName)
		{
			if (guildId <= 0) throw new ArgumentOutOfRangeException(nameof(guildId));
			if (string.IsNullOrWhiteSpace(guildName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(guildName));

			GuildId = guildId;
			GuildName = guildName;
		}
	}
}
