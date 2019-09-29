using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	[Route("api/[controller]")]
	public sealed class WorldTeleporterController : InstanceObjectDataController<GameObjectWorldTeleporterEntryModel, object, IWorldTeleporterGameObjectEntryRepository>
	{
		public WorldTeleporterController(IClaimsPrincipalReader claimsReader, ILogger<AuthorizationReadyController> logger) 
			: base(claimsReader, logger)
		{

		}

		public override Task<IActionResult> CreateObjectInstance(long worldId, IWorldTeleporterGameObjectEntryRepository instanceObjectRepository, IFactoryCreatable<GameObjectWorldTeleporterEntryModel, WorldInstanceableEntryModelCreationContext> defaultInstanceEntryModelFactory, ITypeConverterProvider<GameObjectWorldTeleporterEntryModel, object> tableToNetworkModelConverter)
		{
			return Task.FromResult((IActionResult)BadRequest("This request is not valid for World Teleporters."));
		}
	}
}
