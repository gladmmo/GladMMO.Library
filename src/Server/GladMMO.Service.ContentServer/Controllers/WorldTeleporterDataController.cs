using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	[Route("api/[controller]")]
	public sealed class WorldTeleporterDataController : InstanceObjectDataController<GameObjectWorldTeleporterEntryModel, WorldTeleporterInstanceModel, IWorldTeleporterGameObjectEntryRepository>
	{
		public WorldTeleporterDataController(IClaimsPrincipalReader claimsReader, ILogger<AuthorizationReadyController> logger) 
			: base(claimsReader, logger)
		{

		}

		public override Task<IActionResult> CreateObjectInstance(long worldId, IWorldTeleporterGameObjectEntryRepository instanceObjectRepository, IFactoryCreatable<GameObjectWorldTeleporterEntryModel, WorldInstanceableEntryModelCreationContext> defaultInstanceEntryModelFactory, ITypeConverterProvider<GameObjectWorldTeleporterEntryModel, WorldTeleporterInstanceModel> tableToNetworkModelConverter)
		{
			return Task.FromResult((IActionResult)BadRequest("This request is not valid for World Teleporters."));
		}

		public override async Task<IActionResult> UpdateObjectInstance(WorldTeleporterInstanceModel model, int objectId, IWorldTeleporterGameObjectEntryRepository instanceObjectRepository, ITypeConverterProvider<WorldTeleporterInstanceModel, GameObjectWorldTeleporterEntryModel> networkToTableConverter)
		{
			//So, this one is abit different since there is no created instance.
			//The client needs to create it, we don't assign a default behavior.
			//So it's possible it DOESN'T exist.
			if(await instanceObjectRepository.ContainsAsync(objectId))
				return await base.UpdateObjectInstance(model, objectId, instanceObjectRepository, networkToTableConverter);
			else
			{
				//it doesn't exist, we need to try to create it.
				GameObjectWorldTeleporterEntryModel entryModel = networkToTableConverter.Convert(model);

				await instanceObjectRepository.TryCreateAsync(entryModel);
			}

			return Ok();
		}
	}
}
