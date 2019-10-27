using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class GuildMemberJoinedEventModel : BaseSocialModel
	{
		/// <summary>
		/// The guid of who joined the guild.
		/// </summary>
		[JsonProperty]
		public NetworkEntityGuid JoineeGuid { get; private set; }

		/// <summary>
		/// The id of the guild being joined by <see cref="JoineeGuid"/>
		/// </summary>
		[JsonProperty]
		public int GuildId { get; private set; }

		public GuildMemberJoinedEventModel([JetBrains.Annotations.NotNull] NetworkEntityGuid joineeGuid, int guildId)
		{
			if (guildId <= 0) throw new ArgumentOutOfRangeException(nameof(guildId));

			JoineeGuid = joineeGuid ?? throw new ArgumentNullException(nameof(joineeGuid));
			GuildId = guildId;
		}

		[JsonConstructor]
		private GuildMemberJoinedEventModel()
		{
			
		}
	}
}
