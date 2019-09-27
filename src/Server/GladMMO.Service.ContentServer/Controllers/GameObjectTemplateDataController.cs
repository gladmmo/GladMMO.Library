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
	public class GameObjectTemplateDataController : TemplateObjectDataController<GameObjectTemplateEntryModel, GameObjectTemplateModel, IGameObjectTemplateRepository>
	{
		/// <inheritdoc />
		public GameObjectTemplateDataController(IClaimsPrincipalReader claimsReader, ILogger<AuthorizationReadyController> logger) 
			: base(claimsReader, logger)
		{

		}
	}
}
