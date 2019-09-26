using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	[Route("api/[controller]")]
	public sealed class GameObjectModelController : BaseCustomContentController<GameObjectModelEntryModel>
	{
		public GameObjectModelController(IClaimsPrincipalReader claimsReader, ILogger<AuthorizationReadyController> logger) 
			: base(claimsReader, logger, UserContentType.Creature)
		{

		}

		protected override GameObjectModelEntryModel GenerateNewModel()
		{
			int userId = ClaimsReader.GetUserIdInt(User);
			return new GameObjectModelEntryModel(userId, HttpContext.Connection.RemoteIpAddress.ToString(), Guid.NewGuid());
		}
	}
}
