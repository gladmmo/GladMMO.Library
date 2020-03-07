using System; using FreecraftCore;
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
			: base(claimsReader, logger, UserContentType.GameObject)
		{

		}

		protected override GameObjectModelEntryModel GenerateNewModel()
		{
			int userId = ClaimsReader.GetAccountIdInt(User);
			return new GameObjectModelEntryModel(userId, HttpContext.Connection.RemoteIpAddress.ToString(), Guid.NewGuid());
		}
	}
}
