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

		public GuildMemberJoinedEventModel([JetBrains.Annotations.NotNull] NetworkEntityGuid joineeGuid)
		{
			JoineeGuid = joineeGuid ?? throw new ArgumentNullException(nameof(joineeGuid));
		}

		[JsonConstructor]
		private GuildMemberJoinedEventModel()
		{
			
		}
	}
}
