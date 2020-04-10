using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class CharacterDataInstance
	{
		//We may have more stuff eventually, right now we only have the model id.
		[JsonProperty]
		public int Experience { get; private set; }

		/// <summary>
		/// The current level of the character.
		/// </summary>
		[JsonProperty]
		public int Level { get; private set; }

		/// <summary>
		/// The ID of the map the character is in.
		/// </summary>
		[JsonProperty]
		public int MapId { get; private set; }

		public CharacterDataInstance(int experience, int level, int mapId)
		{
			if (experience < 0) throw new ArgumentOutOfRangeException(nameof(experience));
			if (level < 0) throw new ArgumentOutOfRangeException(nameof(level));
			if (mapId < 0) throw new ArgumentOutOfRangeException(nameof(mapId));

			Experience = experience;
			Level = level;
			MapId = mapId;
		}

		//Serializer ctor
		private CharacterDataInstance()
		{

		}
	}
}
