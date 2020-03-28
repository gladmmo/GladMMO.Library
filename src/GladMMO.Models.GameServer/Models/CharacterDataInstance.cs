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

		public CharacterDataInstance(int experience, int level)
		{
			if (experience < 0) throw new ArgumentOutOfRangeException(nameof(experience));
			if (level < 0) throw new ArgumentOutOfRangeException(nameof(level));

			Experience = experience;
			Level = level;
		}

		//Serializer ctor
		private CharacterDataInstance()
		{

		}
	}
}
