using System; using FreecraftCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class SpellLevelLearnedDefinition
	{
		/// <summary>
		/// The character class type this learned spell is defined for.
		/// </summary>
		[JsonProperty]
		public EntityPlayerClassType CharacterClassType { get; private set; }

		/// <summary>
		/// Indicates what level the spell should be learned at.
		/// </summary>
		[JsonProperty]
		public int LevelLearned { get; private set; }

		/// <summary>
		/// The ID of the spell that should be learned.
		/// </summary>
		[JsonProperty]
		public int SpellId { get; private set; }

		public SpellLevelLearnedDefinition(EntityPlayerClassType characterClassType, int levelLearned, int spellId)
		{
			if (!Enum.IsDefined(typeof(EntityPlayerClassType), characterClassType)) throw new InvalidEnumArgumentException(nameof(characterClassType), (int) characterClassType, typeof(EntityPlayerClassType));
			if (levelLearned < 0) throw new ArgumentOutOfRangeException(nameof(levelLearned));
			if (spellId <= 0) throw new ArgumentOutOfRangeException(nameof(spellId));

			CharacterClassType = characterClassType;
			LevelLearned = levelLearned;
			SpellId = spellId;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		internal SpellLevelLearnedDefinition()
		{
			
		}
	}
}
