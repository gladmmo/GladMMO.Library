using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using OpenIddict.Abstractions;
using OpenIddict.Core;
using OpenIddict.EntityFrameworkCore.Models;
using OpenIddict.Server.AspNetCore;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace GladMMO
{

	//From an old OpenIddict OAuth sample and a slightly modified version that I personally use
	//in https://github.com/GladLive/GladLive.Authentication/blob/master/src/GladLive.Authentication.OAuth/Controllers/AuthorizationController.cs
	[Route(AUTHENTICATION_ROUTE_VALUE)]
	public class AuthenticationController : Controller
	{
		internal const string AUTHENTICATION_ROUTE_VALUE = "api/auth";

		private readonly OpenIddictApplicationManager<OpenIddictApplication> _applicationManager;
		private readonly OpenIddictAuthorizationManager<OpenIddictAuthorization> _authorizationManager;
		private readonly OpenIddictScopeManager<OpenIddictScope> _scopeManager;
		private readonly SignInManager<GuardiansApplicationUser> _signInManager;
		private readonly UserManager<GuardiansApplicationUser> _userManager;

		public AuthenticationController(
			OpenIddictApplicationManager<OpenIddictApplication> applicationManager,
			OpenIddictAuthorizationManager<OpenIddictAuthorization> authorizationManager,
			OpenIddictScopeManager<OpenIddictScope> scopeManager,
			SignInManager<GuardiansApplicationUser> signInManager,
			UserManager<GuardiansApplicationUser> userManager)
		{
			_applicationManager = applicationManager;
			_authorizationManager = authorizationManager;
			_scopeManager = scopeManager;
			_signInManager = signInManager;
			_userManager = userManager;
		}

		[HttpPost, Produces("application/json")]
		public async Task<IActionResult> Exchange()
		{
			var request = HttpContext.GetOpenIddictServerRequest() ??
				throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

			if(request.IsPasswordGrantType())
			{
				var user = await _userManager.FindByNameAsync(request.Username);
				if(user == null)
				{
					return Forbid(
						authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
						properties: new AuthenticationProperties(new Dictionary<string, string>
						{
							[OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidGrant,
							[OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "The username/password couple is invalid."
						}));
				}

				// Validate the username/password parameters and ensure the account is not locked out.
				var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: true);
				if(!result.Succeeded)
				{
					return Forbid(
						authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
						properties: new AuthenticationProperties(new Dictionary<string, string>
						{
							[OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidGrant,
							[OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "The username/password couple is invalid."
						}));
				}

				var principal = await _signInManager.CreateUserPrincipalAsync(user);

				// Note: in this sample, the granted scopes match the requested scope
				// but you may want to allow the user to uncheck specific scopes.
				// For that, simply restrict the list of scopes before calling SetScopes.
				principal.SetScopes(request.GetScopes());
				principal.SetResources(await _scopeManager.ListResourcesAsync(principal.GetScopes()).ToListAsync());

				foreach(var claim in principal.Claims)
				{
					claim.SetDestinations(GetDestinations(claim, principal));
				}

				// Returning a SignInResult will ask OpenIddict to issue the appropriate access/identity tokens.
				return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
			}

			else if(request.IsAuthorizationCodeGrantType() || request.IsDeviceCodeGrantType() || request.IsRefreshTokenGrantType())
			{
				// Retrieve the claims principal stored in the authorization code/device code/refresh token.
				var principal = (await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme)).Principal;

				// Retrieve the user profile corresponding to the authorization code/refresh token.
				// Note: if you want to automatically invalidate the authorization code/refresh token
				// when the user password/roles change, use the following line instead:
				// var user = _signInManager.ValidateSecurityStampAsync(info.Principal);
				var user = await _userManager.GetUserAsync(principal);
				if(user == null)
				{
					return Forbid(
						authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
						properties: new AuthenticationProperties(new Dictionary<string, string>
						{
							[OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidGrant,
							[OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "The token is no longer valid."
						}));
				}

				// Ensure the user is still allowed to sign in.
				if(!await _signInManager.CanSignInAsync(user))
				{
					return Forbid(
						authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
						properties: new AuthenticationProperties(new Dictionary<string, string>
						{
							[OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidGrant,
							[OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "The user is no longer allowed to sign in."
						}));
				}

				foreach(var claim in principal.Claims)
				{
					claim.SetDestinations(GetDestinations(claim, principal));
				}

				// Returning a SignInResult will ask OpenIddict to issue the appropriate access/identity tokens.
				return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
			}

			throw new InvalidOperationException("The specified grant type is not supported.");
		}

		private IEnumerable<string> GetDestinations(Claim claim, ClaimsPrincipal principal)
		{
			// Note: by default, claims are NOT automatically included in the access and identity tokens.
			// To allow OpenIddict to serialize them, you must attach them a destination, that specifies
			// whether they should be included in access tokens, in identity tokens or in both.

			switch(claim.Type)
			{
				case Claims.Name:
					yield return Destinations.AccessToken;

					if(principal.HasScope(Scopes.Profile))
						yield return Destinations.IdentityToken;

					yield break;

				case Claims.Email:
					yield return Destinations.AccessToken;

					if(principal.HasScope(Scopes.Email))
						yield return Destinations.IdentityToken;

					yield break;

				case Claims.Role:
					yield return Destinations.AccessToken;

					if(principal.HasScope(Scopes.Roles))
						yield return Destinations.IdentityToken;

					yield break;

				// Never include the security stamp in the access and identity tokens, as it's a secret value.
				case "AspNet.Identity.SecurityStamp": yield break;

				default:
					yield return Destinations.AccessToken;
					yield break;
			}
		}
	}
}
