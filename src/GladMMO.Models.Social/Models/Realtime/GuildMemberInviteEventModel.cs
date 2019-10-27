using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class GuildMemberInviteEventModel : BaseSocialModel
	{
		/// <summary>
		/// The GuildId of the guild inviting the player.
		/// </summary>
		[JsonProperty]
		public int GuildId { get; private set; }

		/// <summary>
		/// The Entity Guid of whoever is inviting the player.
		/// </summary>
		[JsonProperty]
		public NetworkEntityGuid InviterGuid { get; private set; }

		public GuildMemberInviteEventModel(int guildId, [JetBrains.Annotations.NotNull] NetworkEntityGuid inviterGuid)
		{
			if (guildId <= 0) throw new ArgumentOutOfRangeException(nameof(guildId));

			GuildId = guildId;
			InviterGuid = inviterGuid ?? throw new ArgumentNullException(nameof(inviterGuid));
		}

		[JsonConstructor]
		private GuildMemberInviteEventModel()
		{
			
		}
	}
}
