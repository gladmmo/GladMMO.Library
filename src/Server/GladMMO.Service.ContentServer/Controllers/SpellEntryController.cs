using System;
using System.Collections.Generic;
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

		[HttpGet("default")]
		[ResponseCache(Duration = 5000)]
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
	}
}
