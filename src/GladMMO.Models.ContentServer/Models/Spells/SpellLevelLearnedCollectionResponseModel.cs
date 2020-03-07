using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class SpellLevelLearnedCollectionResponseModel
	{
		[JsonProperty(PropertyName = "LevelLearnedSpells")]
		private SpellLevelLearnedDefinition[] _LevelLearnedSpells;

		/// <summary>
		/// Collection of all level learned spells.
		/// </summary>
		[JsonIgnore]
		public IReadOnlyCollection<SpellLevelLearnedDefinition> LevelLearnedSpells => _LevelLearnedSpells;

		public SpellLevelLearnedCollectionResponseModel([NotNull] SpellLevelLearnedDefinition[] levelLearnedSpells)
		{
			_LevelLearnedSpells = levelLearnedSpells ?? throw new ArgumentNullException(nameof(levelLearnedSpells));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		[JsonConstructor]
		internal SpellLevelLearnedCollectionResponseModel()
		{

		}
	}
}
