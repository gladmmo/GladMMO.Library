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
	public class CreatureDataController : AuthorizationReadyController
	{
		/// <inheritdoc />
		public CreatureDataController(IClaimsPrincipalReader claimsReader, ILogger<AuthorizationReadyController> logger) 
			: base(claimsReader, logger)
		{

		}

		//TODO: We need to do authorization so players can't just change anyone's map.
		[HttpPut("instance/{id}")]
		public async Task<IActionResult> UpdateCreatureInstance(
			[FromBody] CreatureInstanceModel model,
			[FromRoute(Name = "id")] int creatureId,
			[FromServices] ICreatureEntryRepository creatureEntryRepository)
		{
			if (model.Guid.EntryId != creatureId)
				return BadRequest();

			CreatureEntryModel entryModel = await creatureEntryRepository.RetrieveAsync(creatureId);
			entryModel.CreatureTemplateId = model.TemplateId;
			entryModel.InitialOrientation = model.YAxisRotation;
			entryModel.SpawnPosition = new Vector3<float>(model.InitialPosition.x, model.InitialPosition.y, model.InitialPosition.z);

			await creatureEntryRepository.UpdateAsync(creatureId, entryModel);

			return Ok();
		}

		[ProducesJson]
		[HttpGet("{world}/instance")]
		public async Task<IActionResult> GetCreatureEntries([FromRoute(Name = "world")] long worldId,
			[FromServices] ICreatureEntryRepository creatureEntryRepository,
			[FromServices] ITypeConverterProvider<CreatureEntryModel, CreatureInstanceModel> tableToNetworkModelConverter)
		{
			IReadOnlyCollection<CreatureEntryModel> models = await creatureEntryRepository.RetrieveAllWorldIdAsync((int)worldId);

			if (models.Count == 0)
				return BuildFailedResponseModel(CreatureCollectionResponseCode.NoneFound);

			CreatureInstanceModel[] instanceModels = models
				.Select(tableToNetworkModelConverter.Convert)
				.ToArray();

			return BuildSuccessfulResponseModel(new InstanceObjectEntryCollectionModel<CreatureInstanceModel>(instanceModels));
		}

		//TODO: Eventually we need to require authorization, because they need to own the world.
		//[AuthorizeJwt]
		[ProducesJson]
		[HttpPost("{world}/instance")]
		public async Task<IActionResult> CreateCreatureInstance([FromRoute(Name = "world")] long worldId,
			[FromServices] ICreatureEntryRepository creatureEntryRepository)
		{
			CreatureEntryModel creatureEntryModel = new CreatureEntryModel(1, new Vector3<float>(0, 0, 0), 0, worldId);
			bool result = await creatureEntryRepository.TryCreateAsync(creatureEntryModel);

			//No known reason that this should fail.
			if (!result)
				return BuildFailedResponseModel(SceneContentQueryResponseCode.GeneralServerError);

			//Otherwise, it has been created so let's get it
			return await GetCreatureInstance(creatureEntryModel.CreatureEntryId, creatureEntryRepository, new CreatureInstanceTableToNetworkTypeConverter());
		}

		[ProducesJson]
		[HttpGet("instance/{id}")]
		public async Task<IActionResult> GetCreatureInstance([FromRoute(Name = "id")] int creatureId,
			[FromServices] ICreatureEntryRepository creatureEntryRepository,
			[FromServices] ITypeConverterProvider<CreatureEntryModel, CreatureInstanceModel> tableToNetworkModelConverter)
		{
			//If unknown templateId, then just indicate such.
			if(!await creatureEntryRepository.ContainsAsync(creatureId))
				return BuildFailedResponseModel(SceneContentQueryResponseCode.TemplateUnknown);

			//Load the model, convert and send back.
			CreatureEntryModel entryModel = await creatureEntryRepository.RetrieveAsync(creatureId);
			CreatureInstanceModel instanceModel = tableToNetworkModelConverter.Convert(entryModel);

			return BuildSuccessfulResponseModel(instanceModel);
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
			[FromServices] ICreatureEntryRepository creatureEntryRepository,
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

			return BuildSuccessfulResponseModel(new CreatureTemplateCollectionModel(templateModels));
		}

		//TODO: We should make it so it requires ZoneServer authorization to query this
		//[AuthorizeJwt(GuardianApplicationRole.ZoneServer)]
		/*[ProducesJson]
		[ResponseCache(Duration = int.MaxValue)]
		[HttpGet("Map/{id}")]
		public async Task<IActionResult> GetNpcsOnMap([FromRoute(Name = "id")] int mapId, [FromServices] ICreatureEntryRepository entryRepository)
		{
			if(entryRepository == null) throw new ArgumentNullException(nameof(entryRepository));

			IReadOnlyCollection<CreatureEntryModel> entryModels = await entryRepository.RetrieveAllWithMapIdAsync(mapId)
				.ConfigureAwait(false);

			//TODO: Should this be an OK?
			if(entryModels.Count == 0)
				return Ok(new CreatureEntryCollectionResponse(CreatureEntryCollectionResponseCode.NoneFound));

			return base.Ok(new CreatureEntryCollectionResponse(entryModels.Select(npc => BuildDatabaseNPCEntryToTransportNPC(npc)).ToArray()));
		}*/
	}
}
