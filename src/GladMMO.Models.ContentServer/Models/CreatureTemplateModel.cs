using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class CreatureTemplateModel : IObjectTemplateModel
	{
		/// <summary>
		/// The creature's template id.
		/// </summary>
		[JsonProperty]
		public int TemplateId { get; private set; }

		/// <summary>
		/// The ID of the creature's model.
		/// </summary>
		[JsonProperty]
		public long ModelId { get; private set; }

		/// <summary>
		/// The name of the Creature. Will be the one the client sees, the one the NameQuery will return.
		/// </summary>
		[JsonProperty]
		public string CreatureName { get; private set; }

		//Min/Max level based on TrinityCore.
		/// <summary>
		/// The minimum level of the creature if the creature has a level range.
		/// </summary>
		[JsonProperty]
		public int MinimumLevel { get; private set; }

		/// <summary>
		/// The maximum level of the creature if the creature has a level range. When added to world, a level in chosen in the specified level range.
		/// </summary>
		[JsonProperty]
		public int MaximumLevel { get; private set; }

		public CreatureTemplateModel(int creatureTemplateId, long modelId, [NotNull] string creatureName, int minimumLevel, int maximumLevel)
		{
			if (modelId <= 0) throw new ArgumentOutOfRangeException(nameof(modelId));
			if (string.IsNullOrWhiteSpace(creatureName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(creatureName));
			if (minimumLevel <= 0) throw new ArgumentOutOfRangeException(nameof(minimumLevel));
			if (maximumLevel <= 0) throw new ArgumentOutOfRangeException(nameof(maximumLevel));
			if (creatureTemplateId <= 0) throw new ArgumentOutOfRangeException(nameof(creatureTemplateId));

			TemplateId = creatureTemplateId;
			ModelId = modelId;
			CreatureName = creatureName;
			MinimumLevel = minimumLevel;
			MaximumLevel = maximumLevel;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		[JsonConstructor]
		private CreatureTemplateModel()
		{
			
		}
	}
}
