using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class CharacterGuildListResponseModel
	{
		[JsonProperty]
		private int[] _GuildedCharacterIds { get; set; }

		[JsonIgnore]
		public IReadOnlyCollection<int> GuildedCharacterIds => _GuildedCharacterIds;

		public CharacterGuildListResponseModel([JetBrains.Annotations.NotNull] int[] guildedCharacterIds)
		{
			_GuildedCharacterIds = guildedCharacterIds ?? throw new ArgumentNullException(nameof(guildedCharacterIds));
		}

		[JsonConstructor]
		private CharacterGuildListResponseModel()
		{
			
		}
	}
}
