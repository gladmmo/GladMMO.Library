using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class GuildStatusChangedEventModel : BaseSocialModel
	{
		/// <summary>
		/// Indicates if the player is now guildless.
		/// </summary>
		[JsonIgnore]
		public bool IsGuildless => GuildId == 0;

		/// <summary>
		/// The guid of the entity that has a changing guild status.
		/// </summary>
		[JsonProperty]
		public ObjectGuid EntityGuid { get; private set; }

		/// <summary>
		/// The current id of the guild.
		/// </summary>
		[JsonProperty]
		public int GuildId { get; private set; }

		public GuildStatusChangedEventModel([NotNull] ObjectGuid entityGuid, int guildId)
		{
			if(guildId < 0) throw new ArgumentOutOfRangeException(nameof(guildId));

			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
			GuildId = guildId;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		[JsonConstructor]
		private GuildStatusChangedEventModel()
		{
			
		}
	}
}
