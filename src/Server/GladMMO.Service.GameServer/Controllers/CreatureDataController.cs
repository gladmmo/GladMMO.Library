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
				return Ok(new ZoneServerNPCEntryCollectionResponse(NpcEntryCollectionResponseCode.NoneFound));

			return base.Ok(new ZoneServerNPCEntryCollectionResponse(entryModels.Select(npc => BuildDatabaseNPCEntryToTransportNPC(npc)).ToArray()));
		}

		//TODO: Create a converter type
		private static ZoneServerNpcEntryModel BuildDatabaseNPCEntryToTransportNPC(CreatureEntryModel npc)
		{
			throw new NotImplementedException("TODO: Rewrite NPC crap.");
			/*NetworkEntityGuidBuilder guidBuilder = new NetworkEntityGuidBuilder();

			NetworkEntityGuid guid = guidBuilder.WithId(npc.EntryId)
				.WithType(EntityType.Npc)
				.Build();

			//TODO: Create a Vector3 converter
			return new ZoneServerNpcEntryModel(guid, npc.NpcTemplateId, new Vector3(npc.SpawnPosition.X, npc.SpawnPosition.Y, npc.SpawnPosition.Z), npc.MovementType, npc.MovementData);*/
		}

		//TODO: Conslidate/centralize name query stuff via entity GUID.
		[AllowAnonymous]
		[ProducesJson]
		[ResponseCache(Duration = int.MaxValue)] //NPC names can be cached forever.
		[HttpGet("name/{id}")]
		public async Task<IActionResult> NameQuery([FromRoute(Name = "id")] int npcId, [FromServices] ICreatureTemplateRepository templateRepository)
		{
			if(templateRepository == null) throw new ArgumentNullException(nameof(templateRepository));

			if(npcId < 0)
				return BuildNotFoundUnknownIdResponse();

			bool knownId = await templateRepository.ContainsAsync(npcId);

			//TODO: JSON Response
			if(!knownId)
				return BuildNotFoundUnknownIdResponse();

			//Else if it is a known id we should grab the name of the character
			string name = await templateRepository.RetrieveNameAsync(npcId);

			return Ok(new NameQueryResponse(name));
		}

		private IActionResult BuildNotFoundUnknownIdResponse()
		{
			return NotFound(new NameQueryResponse(NameQueryResponseCode.UnknownIdError));
		}
	}
}
