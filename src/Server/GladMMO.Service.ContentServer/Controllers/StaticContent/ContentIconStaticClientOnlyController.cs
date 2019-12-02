using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	[Route("api/ContentIcon")]
	public sealed class ContentIconStaticClientOnlyController : StaticClientOnlyContentController<ContentIconEntryModel, IContentIconEntryModelRepository, ContentIconInstanceModel>
	{
		public ContentIconStaticClientOnlyController(IClaimsPrincipalReader claimsReader, ILogger<AuthorizationReadyController> logger) 
			: base(claimsReader, logger)
		{

		}
	}
}
