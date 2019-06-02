using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GladMMO
{
	[Route("api/[controller]")]
	public class AuthorizationController : Controller
	{
		
		private IClaimsPrincipalReader ClaimsReader { get; }

		/// <inheritdoc />
		public AuthorizationController(IClaimsPrincipalReader claimsReader)
		{
			if(claimsReader == null) throw new ArgumentNullException(nameof(claimsReader));
			ClaimsReader = claimsReader;
		}

		[AuthorizeJwt]
		[HttpGet("check")]
		[NoResponseCache]
		public IActionResult Check()
		{
			if(ModelState.IsValid && this.User.Identity.IsAuthenticated)
				return Ok();
			else
				return Unauthorized();
		}

		[AuthorizeJwt]
		[HttpGet("user/name")]
		[NoResponseCache]
		public string GetName() 
			=> ClaimsReader.GetUserName(this.User);

		[AuthorizeJwt]
		[HttpGet("user/id")]
		[NoResponseCache]
		public int GetId()
			=> ClaimsReader.GetUserIdInt(this.User);
	}
}
