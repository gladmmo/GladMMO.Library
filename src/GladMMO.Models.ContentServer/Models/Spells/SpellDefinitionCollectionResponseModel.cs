using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class SpellDefinitionCollectionResponseModel
	{
		[JsonProperty]
		private SpellDefinitionDataModel[] _SpellEntries { get; set; }

		[JsonProperty]
		private SpellEffectDefinitionDataModel[] _SpellEffects { get; set; }

		[JsonIgnore]
		public IReadOnlyCollection<SpellDefinitionDataModel> SpellEntries => _SpellEntries;

		[JsonIgnore]
		public IReadOnlyCollection<SpellEffectDefinitionDataModel> SpellEffects => _SpellEffects;

		public SpellDefinitionCollectionResponseModel([NotNull] SpellDefinitionDataModel[] spellEntries, [NotNull] SpellEffectDefinitionDataModel[] spellEffects)
		{
			_SpellEntries = spellEntries ?? throw new ArgumentNullException(nameof(spellEntries));
			_SpellEffects = spellEffects ?? throw new ArgumentNullException(nameof(spellEffects));
		}

		[JsonConstructor]
		private SpellDefinitionCollectionResponseModel()
		{
			
		}
	}
}
