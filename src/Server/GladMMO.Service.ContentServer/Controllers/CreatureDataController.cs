using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

		//TODO: We should make it so it requires ZoneServer authorization to query this
		//[AuthorizeJwt(GuardianApplicationRole.ZoneServer)]
		[ProducesJson]
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
		}

		//TODO: Create a converter type
		private static CreatureInstanceModel BuildDatabaseNPCEntryToTransportNPC(CreatureEntryModel creature)
		{
			NetworkEntityGuidBuilder guidBuilder = new NetworkEntityGuidBuilder();

			NetworkEntityGuid guid = guidBuilder.WithId(creature.CreatureEntryId)
				.WithType(EntityType.Npc)
				.Build();

			//TODO: Create a Vector3 converter
			return new CreatureInstanceModel(guid, creature.CreatureTemplateId, new Vector3(creature.SpawnPosition.X, creature.SpawnPosition.Y, creature.SpawnPosition.Z));
		}
	}
}
