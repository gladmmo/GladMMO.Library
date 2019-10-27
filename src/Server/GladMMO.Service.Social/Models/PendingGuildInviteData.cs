using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladMMO
{
	/// <summary>
	/// Data model representing a pending guild invite.
	/// </summary>
	public sealed class PendingGuildInviteData
	{
		public DateTime InvitedTimeStamp { get; }

		public int GuildId { get; }

		public const int MAXIMUM_PENDING_GUILD_INVITE_TIME_SECONDS = 60;

		public PendingGuildInviteData(int guildId)
		{
			InvitedTimeStamp = DateTime.UtcNow;
			GuildId = guildId;
		}

		public bool isInviteExpired()
		{
			//If the current time elapsed has been more than the maximum pending time then it's considered
			//an expired invite.
			TimeSpan timeSinceInvite = DateTime.UtcNow - InvitedTimeStamp;

			return timeSinceInvite.TotalSeconds >= MAXIMUM_PENDING_GUILD_INVITE_TIME_SECONDS;
		}
	}
}
