using System; using FreecraftCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	[Route("api/[controller]")]
	public class SpellEntryController : AuthorizationReadyController
	{
		private ISpellEntryRepository SpellEntryRepository { get; }

		public SpellEntryController(IClaimsPrincipalReader claimsReader, ILogger<AuthorizationReadyController> logger,
			[NotNull] ISpellEntryRepository spellEntryRepository) 
			: base(claimsReader, logger)
		{
			SpellEntryRepository = spellEntryRepository ?? throw new ArgumentNullException(nameof(spellEntryRepository));
		}

		//TODO: Renable caching when we're done with development.
		[HttpGet("default")]
		//[ResponseCache(Duration = 5000)]
		public async Task<IActionResult> GetDefaultSpellDataAsync([FromServices] ITypeConverterProvider<SpellEntryModel, SpellDefinitionDataModel> spellEntryConverter,
			[FromServices] ITypeConverterProvider<SpellEffectEntryModel, SpellEffectDefinitionDataModel> spellEffectConverter)
		{
			SpellEntryModel[] entryModels = await SpellEntryRepository.RetrieveAllDefaultAsync(true);
			SpellEffectEntryModel[] effectModels = entryModels.Select(s => s.SpellEffectOne)
				.Distinct(new ContentIdentifiableEqualityComparer<SpellEffectEntryModel>())
				.ToArray();

			SpellDefinitionDataModel[] transportableEntryModels = entryModels.Select(spellEntryConverter.Convert)
				.ToArray();

			SpellEffectDefinitionDataModel[] transportableEffectEntryModels = effectModels.Select(spellEffectConverter.Convert)
				.ToArray();

			return Json(new SpellDefinitionCollectionResponseModel(transportableEntryModels, transportableEffectEntryModels));
		}

		//TODO: Renable caching when we're done with development.
		[HttpGet("levellearned")]
		//[ResponseCache(Duration = 5000)]
		public async Task<IActionResult> GetLevelLearnedSpellsAsync([FromServices] ILevelLearnedSpellRepository levelLearnedSpellRepository, 
			[FromServices] ITypeConverterProvider<SpellLevelLearned, SpellLevelLearnedDefinition> converter)
		{
			SpellLevelLearned[] levelLearneds = await levelLearnedSpellRepository.RetrieveAllAsync();

			return Json(CreatedSpellLevelLearnedCollectionResponse(levelLearneds, converter));
		}

		[HttpGet("levellearned/{class}")]
		//[ResponseCache(Duration = 5000)]
		public async Task<IActionResult> GetLevelLearnedSpellsForClassAsync([FromRoute(Name = "class")] EntityPlayerClassType classType, [FromServices] ILevelLearnedSpellRepository levelLearnedSpellRepository,
			[FromServices] ITypeConverterProvider<SpellLevelLearned, SpellLevelLearnedDefinition> converter)
		{
			if (!Enum.IsDefined(typeof(EntityPlayerClassType), classType)) throw new InvalidEnumArgumentException(nameof(classType), (int) classType, typeof(EntityPlayerClassType));

			SpellLevelLearned[] levelLearneds = await levelLearnedSpellRepository.RetrieveAllAsync(classType);

			return Json(CreatedSpellLevelLearnedCollectionResponse(levelLearneds, converter));
		}

		[HttpGet("levellearned/{class}/{level}")]
		//[ResponseCache(Duration = 5000)]
		public async Task<IActionResult> GetLevelLearnedSpellsForClassAndLevelAsync([FromRoute(Name = "class")] EntityPlayerClassType classType, [FromRoute] int level, [FromServices] ILevelLearnedSpellRepository levelLearnedSpellRepository,
			[FromServices] ITypeConverterProvider<SpellLevelLearned, SpellLevelLearnedDefinition> converter)
		{
			if(!Enum.IsDefined(typeof(EntityPlayerClassType), classType)) throw new InvalidEnumArgumentException(nameof(classType), (int)classType, typeof(EntityPlayerClassType));
			if (level < 0)
				return Json(new SpellLevelLearnedCollectionResponseModel(Array.Empty<SpellLevelLearnedDefinition>()));

			SpellLevelLearned[] levelLearneds = await levelLearnedSpellRepository.RetrieveAllAsync(classType, level);

			return Json(CreatedSpellLevelLearnedCollectionResponse(levelLearneds, converter));
		}

		private SpellLevelLearnedCollectionResponseModel CreatedSpellLevelLearnedCollectionResponse([NotNull] SpellLevelLearned[] learnedSpells, [FromServices] [NotNull] ITypeConverterProvider<SpellLevelLearned, SpellLevelLearnedDefinition> converter)
		{
			if (learnedSpells == null) throw new ArgumentNullException(nameof(learnedSpells));
			if (converter == null) throw new ArgumentNullException(nameof(converter));

			SpellLevelLearnedDefinition[] spellLevelLearnedDefinitions = learnedSpells
				.Select(converter.Convert)
				.ToArrayTryAvoidCopy();

			return new SpellLevelLearnedCollectionResponseModel(spellLevelLearnedDefinitions);
		}
	}
}
