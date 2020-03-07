using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class CharacterGuildMembershipStatusResponse
	{
		[JsonProperty]
		public int GuildId { get; private set; }

		public CharacterGuildMembershipStatusResponse(int guildId)
		{
			if (guildId <= 0) throw new ArgumentOutOfRangeException(nameof(guildId));

			GuildId = guildId;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		private CharacterGuildMembershipStatusResponse()
		{
			
		}
	}
}
