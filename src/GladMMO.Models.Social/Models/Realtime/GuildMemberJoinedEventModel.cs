using System; using FreecraftCore;
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
		public ObjectGuid JoineeGuid { get; private set; }

		public GuildMemberJoinedEventModel([JetBrains.Annotations.NotNull] ObjectGuid joineeGuid)
		{
			JoineeGuid = joineeGuid ?? throw new ArgumentNullException(nameof(joineeGuid));
		}

		[JsonConstructor]
		private GuildMemberJoinedEventModel()
		{
			
		}
	}
}
