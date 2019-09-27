using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GladMMO.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UnityEngine;

namespace GladMMO
{
	[Route("api/[controller]")]
	public class CreatureTemplateDataController : AuthorizationReadyController
	{
		/// <inheritdoc />
		public CreatureTemplateDataController(IClaimsPrincipalReader claimsReader, ILogger<AuthorizationReadyController> logger) 
			: base(claimsReader, logger)
		{

		}

		[ProducesJson]
		[HttpGet("template/{id}")]
		public async Task<IActionResult> GetCreatureTemplate([FromRoute(Name = "id")] int creatureTemplateId, 
			[FromServices] ICreatureTemplateRepository creatureTemplateRepository,
			[FromServices] ITypeConverterProvider<CreatureTemplateEntryModel, CreatureTemplateModel> tableToNetworkModelConverter)
		{
			//If unknown templateId, then just indicate such.
			if (!await creatureTemplateRepository.ContainsAsync(creatureTemplateId))
				return BuildFailedResponseModel(SceneContentQueryResponseCode.TemplateUnknown);

			//Load the model, convert and send back.
			CreatureTemplateEntryModel entryModel = await creatureTemplateRepository.RetrieveAsync(creatureTemplateId);
			CreatureTemplateModel templateModel = tableToNetworkModelConverter.Convert(entryModel);

			return BuildSuccessfulResponseModel(templateModel);
		}

		[ProducesJson]
		[HttpGet("{world}/template")]
		public async Task<IActionResult> GetCreatureTemplates([FromRoute(Name = "world")] int worldId,
			[FromServices] ICreatureTemplateRepository creatureEntryRepository,
			[FromServices] ITypeConverterProvider<CreatureTemplateEntryModel, CreatureTemplateModel> tableToNetworkModelConverter)
		{
			//We can actually get all the templates FROM the creature instances.
			IReadOnlyCollection<CreatureTemplateEntryModel> models = await creatureEntryRepository.RetrieveTemplatesByWorldIdAsync((int)worldId);

			if(models.Count == 0)
				return BuildFailedResponseModel(CreatureCollectionResponseCode.NoneFound);

			CreatureTemplateModel[] templateModels = models
				.Distinct(DatabaseModelKeyableEquailityComparer<CreatureTemplateEntryModel>.Instance)
				.Select(tableToNetworkModelConverter.Convert)
				.ToArray();

			return BuildSuccessfulResponseModel(new InstanceObjectEntryCollectionModel<CreatureTemplateModel>(templateModels));
		}
	}
}
