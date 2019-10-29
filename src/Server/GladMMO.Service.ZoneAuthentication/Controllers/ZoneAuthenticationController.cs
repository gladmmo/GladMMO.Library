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
	//TODO: A lot of code duplication between AuthServer and ZoneAuthServer. Not much value in combining it right now though.
	//From an old OpenIddict OAuth sample and a slightly modified version that I personally use
	//in https://github.com/GladLive/GladLive.Authentication/blob/master/src/GladLive.Authentication.OAuth/Controllers/AuthorizationController.cs
	[Route(AUTHENTICATION_ROUTE_VALUE)]
	public class ZoneAuthenticationController : Controller
	{
		internal const string AUTHENTICATION_ROUTE_VALUE = "api/auth";

		private IOptions<IdentityOptions> IdentityOptions { get; }

		private SignInManager<ZoneServerApplicationUser> SignInManager { get; }

		private UserManager<ZoneServerApplicationUser> UserManager { get; }

		private ILogger<ZoneAuthenticationController> Logger { get; }

		public ZoneAuthenticationController(
			IOptions<IdentityOptions> identityOptions,
			SignInManager<ZoneServerApplicationUser> signInManager,
			UserManager<ZoneServerApplicationUser> userManager, 
			ILogger<ZoneAuthenticationController> logger)
		{
			IdentityOptions = identityOptions;
			SignInManager = signInManager;
			UserManager = userManager;
			Logger = logger;
		}

		internal async Task<IActionResult> Authenticate([JetBrains.Annotations.NotNull] string username,
			[JetBrains.Annotations.NotNull] string password,
			[JetBrains.Annotations.NotNull] IEnumerable<string> scopes)
		{
			if (scopes == null) throw new ArgumentNullException(nameof(scopes));
			if (string.IsNullOrEmpty(username))
				throw new ArgumentException("Value cannot be null or empty.", nameof(username));
			if (string.IsNullOrEmpty(password))
				throw new ArgumentException("Value cannot be null or empty.", nameof(password));

			//We want to log this out for information purposes whenever an auth request begins
			if(Logger.IsEnabled(LogLevel.Information))
				Logger.LogInformation($"Auth Request: {username} {HttpContext.Connection.RemoteIpAddress}:{HttpContext.Connection.RemotePort}");

			var user = await UserManager.FindByNameAsync(username);
			if(user == null)
			{
				return BadRequest(new OpenIdConnectResponse
				{
					Error = OpenIdConnectConstants.Errors.InvalidGrant,
					ErrorDescription = "The username/password couple is invalid."
				});
			}

			// Ensure the user is allowed to sign in.
			if(!await SignInManager.CanSignInAsync(user))
			{
				return BadRequest(new OpenIdConnectResponse
				{
					Error = OpenIdConnectConstants.Errors.InvalidGrant,
					ErrorDescription = "The specified user is not allowed to sign in."
				});
			}

			// Reject the token request if two-factor authentication has been enabled by the user.
			if(UserManager.SupportsUserTwoFactor && await UserManager.GetTwoFactorEnabledAsync(user))
			{
				return BadRequest(new OpenIdConnectResponse
				{
					Error = OpenIdConnectConstants.Errors.InvalidGrant,
					ErrorDescription = "The specified user is not allowed to sign in."
				});
			}

			// Ensure the user is not already locked out.
			if(UserManager.SupportsUserLockout && await UserManager.IsLockedOutAsync(user))
			{
				return BadRequest(new OpenIdConnectResponse
				{
					Error = OpenIdConnectConstants.Errors.InvalidGrant,
					ErrorDescription = "The username/password couple is invalid."
				});
			}

			// Ensure the password is valid.
			if(!await UserManager.CheckPasswordAsync(user, password))
			{
				if(UserManager.SupportsUserLockout)
				{
					await UserManager.AccessFailedAsync(user);
				}

				return BadRequest(new OpenIdConnectResponse
				{
					Error = OpenIdConnectConstants.Errors.InvalidGrant,
					ErrorDescription = "The username/password couple is invalid."
				});
			}

			if(UserManager.SupportsUserLockout)
			{
				await UserManager.ResetAccessFailedCountAsync(user);
			}

			// Create a new authentication ticket.
			var ticket = await CreateTicketAsync(scopes, user);

			return SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);
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
				return await Authenticate(request.Username, request.Password, request.GetScopes());
			}

			return BadRequest(new OpenIdConnectResponse
			{
				Error = OpenIdConnectConstants.Errors.UnsupportedGrantType,
				ErrorDescription = "The specified grant type is not supported."
			});
		}
		
		private async Task<AuthenticationTicket> CreateTicketAsync(IEnumerable<string> scopes, ZoneServerApplicationUser user)
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
			}.Intersect(scopes.Concat(new string[1] { OpenIdConnectConstants.Scopes.OpenId }))); //HelloKitty: Always include the OpenId, it's required for the Playfab authentication

			ticket.SetResources("zoneauth-server");

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
