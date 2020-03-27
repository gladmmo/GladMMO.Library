using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenIddict.Core;

namespace GladMMO
{
	[Route("api/register")]
	public class ZoneServerRegistrationController : AuthorizationReadyController
	{
		private UserManager<ZoneServerApplicationUser> UserManager { get; }

		/// <inheritdoc />
		public ZoneServerRegistrationController([NotNull] UserManager<ZoneServerApplicationUser> userManager,
			[NotNull] ILogger<ZoneServerRegistrationController> logger,
			[FromServices] IClaimsPrincipalReader claimsReader)
		 : base(claimsReader, logger)
		{
			UserManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
		}

		//TODO: Require player account role.
		[HttpPost]
		[AuthorizeJwt] //Zoneservers actually need to register themselves with authorization from a user account.
		[ProducesJson]
		public async Task<IActionResult> CreateZoneServerAccount([FromBody] [JetBrains.Annotations.NotNull] ZoneServerAccountRegistrationRequest request)
		{
			if (request == null) throw new ArgumentNullException(nameof(request));

			//We want to log this out for information purposes whenever an auth request begins
			if(Logger.IsEnabled(LogLevel.Information))
				Logger.LogInformation($"Zone Register Request by UserAccount: {ClaimsReader.GetAccountName(this.User)}:{ClaimsReader.GetAccountIdInt(this.User)} {HttpContext.Connection.RemoteIpAddress}:{HttpContext.Connection.RemotePort}");

			//TODO: Create cryptographically secure random bytes for password and user.
			string zoneUserName = Guid.NewGuid().ToString();
			string zonePassword = Guid.NewGuid().ToString();

			ZoneServerApplicationUser user = new ZoneServerApplicationUser(ClaimsReader.GetAccountIdInt(this.User))
			{
				UserName = zoneUserName,
				Email = "dev@dev.com", //TODO: Real email???
			};

			IdentityResult identityResult = await UserManager.CreateAsync(user, zonePassword);

			if (identityResult.Succeeded)
			{
				//Adds the vrgid account owner claim.
				await UserManager.AddClaimAsync(user, new Claim(GladMMOAuthConstants.ACCOUNT_ID_OWNER_CLAIM_NAME, ClaimsReader.GetAccountId(this.User)));
				
				//Here we inherit each role the user account has to the zoneserver account
				foreach (var claim in User.Claims)
					if (claim.Type == "role") //TODO: Is there a constant for this??
						if(claim.Value.ToLower() != GladMMOAuthConstants.PLAYERACCOUNT_AUTHORIZATION_ROLE) //DO NOT add the player role.
							await UserManager.AddToRoleAsync(user, claim.Value);

				//Also add the ZoneServer role
				await UserManager.AddToRoleAsync(user, GladMMOAuthConstants.ZONESERVER_AUTHORIZATION_ROLE);

				//At this point, the account has the PlayFab id claim so it's ready for use.
				return Json(new ZoneServerAccountRegistrationResponse(int.Parse(user.Id), zoneUserName, zonePassword));
			}
			else
				return BadRequest(identityResult.Errors.Aggregate("", (s, error) => $"{s} {error.Code}:{error.Description}"));
		}
	}
}
