﻿using System;
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

		//TODO: Eventually we need to require authorization, because they need to own the world.
		//[AuthorizeJwt]
		[ProducesJson]
		[HttpPost("instance?world={worldId}")]
		public async Task<IActionResult> CreateCreatureInstance([FromQuery(Name = "worldId")] long worldId,
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
