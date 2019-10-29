using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	[Route("api/registeration")]
	public class RegisterationController : Controller
	{
		private UserManager<GuardiansApplicationUser> UserManager { get; }

		private ILogger<RegisterationController> Logger { get; }

		//I know, this is ridiculous but it's the only way to get Playfab integration functional.
		private IAuthenticationService AuthenticationServiceClient { get; }

		/// <inheritdoc />
		public RegisterationController([NotNull] UserManager<GuardiansApplicationUser> userManager,
			[NotNull] ILogger<RegisterationController> logger,
			[NotNull] IAuthenticationService authenticationServiceClient)
		{
			UserManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			AuthenticationServiceClient = authenticationServiceClient ?? throw new ArgumentNullException(nameof(authenticationServiceClient));
		}

#warning Dont ever deploy this for real
		[HttpPost]
		public async Task<IActionResult> RegisterDev([FromQuery] string username, [FromQuery] string password)
		{
			if(string.IsNullOrWhiteSpace(username))
				return BadRequest("Invalid username");

			if(string.IsNullOrWhiteSpace(password))
				return BadRequest("Invalid password.");

			//We want to log this out for information purposes whenever an auth request begins
			if(Logger.IsEnabled(LogLevel.Information))
				Logger.LogInformation($"Register Request: {username} {HttpContext.Connection.RemoteIpAddress}:{HttpContext.Connection.RemotePort}");

			GuardiansApplicationUser user = new GuardiansApplicationUser()
			{
				UserName = username,
				Email = "dev@dev.com"
			};

			IdentityResult identityResult = await UserManager.CreateAsync(user, password);

			if (identityResult.Succeeded)
			{
				PlayerAccountJWTModel PlayerAccountJWTModel = await AuthenticationServiceClient.TryAuthenticate(new AuthenticationRequestModel(username, password));
				await UserManager.AddClaimAsync(user, new Claim(GladMMOPlayfabConstants.PLAYFAB_JWT_CLAIM_TYPE, PlayerAccountJWTModel.PlayfabId));

				//At this point, the account has the PlayFab id claim so it's ready for use.
				return Ok();
			}
			else
				return BadRequest(identityResult.Errors.Aggregate("", (s, error) => $"{s} {error.Code}:{error.Description}"));
		}
	}
}
