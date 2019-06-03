using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Primitives;
using AspNet.Security.OpenIdConnect.Server;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OpenIddict.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace GladMMO
{

	//From an old OpenIddict OAuth sample and a slightly modified version that I personally use
	//in https://github.com/GladLive/GladLive.Authentication/blob/master/src/GladLive.Authentication.OAuth/Controllers/AuthorizationController.cs
	[Route(AUTHENTICATION_ROUTE_VALUE)]
	public class AuthenticationController : Controller
	{
		internal const string AUTHENTICATION_ROUTE_VALUE = "api/auth";

		private IOptions<IdentityOptions> IdentityOptions { get; }

		private SignInManager<GuardiansApplicationUser> SignInManager { get; }

		private UserManager<GuardiansApplicationUser> UserManager { get; }

		private ILogger<AuthenticationController> Logger { get; }

		public AuthenticationController(
			IOptions<IdentityOptions> identityOptions,
			SignInManager<GuardiansApplicationUser> signInManager,
			UserManager<GuardiansApplicationUser> userManager, 
			ILogger<AuthenticationController> logger)
		{
			IdentityOptions = identityOptions;
			SignInManager = signInManager;
			UserManager = userManager;
			Logger = logger;
		}

		[HttpPost]
		[Produces("application/json")]
		public async Task<IActionResult> Exchange(OpenIdConnectRequest request)
		{
			Debug.Assert(request.IsTokenRequest(),
				"The OpenIddict binder for ASP.NET Core MVC is not registered. " +
				"Make sure services.AddOpenIddict().AddMvcBinders() is correctly called.");

			if (request.IsPasswordGrantType())
			{
				//We want to log this out for information purposes whenever an auth request begins
				if(Logger.IsEnabled(LogLevel.Information))
					Logger.LogInformation($"Auth Request: {request.Username} {HttpContext.Connection.RemoteIpAddress}:{HttpContext.Connection.RemotePort}");

				var user = await UserManager.FindByNameAsync(request.Username);
				if (user == null)
				{
					return BadRequest(new OpenIdConnectResponse
					{
						Error = OpenIdConnectConstants.Errors.InvalidGrant,
						ErrorDescription = "The username/password couple is invalid."
					});
				}

				// Ensure the user is allowed to sign in.
				if (!await SignInManager.CanSignInAsync(user))
				{
					return BadRequest(new OpenIdConnectResponse
					{
						Error = OpenIdConnectConstants.Errors.InvalidGrant,
						ErrorDescription = "The specified user is not allowed to sign in."
					});
				}

				// Reject the token request if two-factor authentication has been enabled by the user.
				if (UserManager.SupportsUserTwoFactor && await UserManager.GetTwoFactorEnabledAsync(user))
				{
					return BadRequest(new OpenIdConnectResponse
					{
						Error = OpenIdConnectConstants.Errors.InvalidGrant,
						ErrorDescription = "The specified user is not allowed to sign in."
					});
				}

				// Ensure the user is not already locked out.
				if (UserManager.SupportsUserLockout && await UserManager.IsLockedOutAsync(user))
				{
					return BadRequest(new OpenIdConnectResponse
					{
						Error = OpenIdConnectConstants.Errors.InvalidGrant,
						ErrorDescription = "The username/password couple is invalid."
					});
				}

				// Ensure the password is valid.
				if (!await UserManager.CheckPasswordAsync(user, request.Password))
				{
					if (UserManager.SupportsUserLockout)
					{
						await UserManager.AccessFailedAsync(user);
					}

					return BadRequest(new OpenIdConnectResponse
					{
						Error = OpenIdConnectConstants.Errors.InvalidGrant,
						ErrorDescription = "The username/password couple is invalid."
					});
				}

				if (UserManager.SupportsUserLockout)
				{
					await UserManager.ResetAccessFailedCountAsync(user);
				}

				// Create a new authentication ticket.
				var ticket = await CreateTicketAsync(request, user);

				return SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);
			}

			return BadRequest(new OpenIdConnectResponse
			{
				Error = OpenIdConnectConstants.Errors.UnsupportedGrantType,
				ErrorDescription = "The specified grant type is not supported."
			});
		}
		
		private async Task<AuthenticationTicket> CreateTicketAsync(OpenIdConnectRequest request, GuardiansApplicationUser user)
		{
			// Create a new ClaimsPrincipal containing the claims that
			// will be used to create an id_token, a token or a code.
			var principal = await SignInManager.CreateUserPrincipalAsync(user);

			// Create a new authentication ticket holding the user identity.
			var ticket = new AuthenticationTicket(principal,
				new Microsoft.AspNetCore.Authentication.AuthenticationProperties(),
				OpenIdConnectServerDefaults.AuthenticationScheme);

			// Set the list of scopes granted to the client application.
			ticket.SetScopes(new[]
			{
				OpenIdConnectConstants.Scopes.OpenId,
				OpenIdConnectConstants.Scopes.Profile,
				OpenIddictConstants.Scopes.Roles
			}.Intersect(request.GetScopes().Concat(new string[1] { OpenIdConnectConstants.Scopes.OpenId }))); //HelloKitty: Always include the OpenId, it's required for the Playfab authentication

			ticket.SetResources("auth-server");

			// Note: by default, claims are NOT automatically included in the access and identity tokens.
			// To allow OpenIddict to serialize them, you must attach them a destination, that specifies
			// whether they should be included in access tokens, in identity tokens or in both.
			foreach (var claim in ticket.Principal.Claims)
			{
				// Never include the security stamp in the access and identity tokens, as it's a secret value.
				if (claim.Type == IdentityOptions.Value.ClaimsIdentity.SecurityStampClaimType)
				{
					continue;
				}

				var destinations = new List<string>
				{
					OpenIdConnectConstants.Destinations.AccessToken
				};

				// Only add the iterated claim to the id_token if the corresponding scope was granted to the client application.
				// The other claims will only be added to the access_token, which is encrypted when using the default format.
				if ((claim.Type == OpenIdConnectConstants.Claims.Name && ticket.HasScope(OpenIdConnectConstants.Scopes.Profile)) ||
					(claim.Type == OpenIdConnectConstants.Claims.Email && ticket.HasScope(OpenIdConnectConstants.Scopes.Email)) ||
					(claim.Type == OpenIdConnectConstants.Claims.Role && ticket.HasScope(OpenIddictConstants.Claims.Roles)))
				{
					destinations.Add(OpenIdConnectConstants.Destinations.IdentityToken);
				}

				claim.SetDestinations(destinations);
			}

			return ticket;
		}
	}
}
