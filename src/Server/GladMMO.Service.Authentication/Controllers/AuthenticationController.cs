using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore;
using FreecraftCore.Crypto;
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

		public wotlk_authContext TrinityCoreAuthenticationDbContext { get; }

		public AuthenticationController([JetBrains.Annotations.NotNull] wotlk_authContext trinityCoreAuthenticationDbContext)
		{
			TrinityCoreAuthenticationDbContext = trinityCoreAuthenticationDbContext ?? throw new ArgumentNullException(nameof(trinityCoreAuthenticationDbContext));
		}

		[HttpPost, Produces("application/json")]
		public async Task<IActionResult> Exchange()
		{
			var request = HttpContext.GetOpenIddictServerRequest() ??
				throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

			if(request.IsPasswordGrantType())
			{
				if(!await TrinityCoreAuthenticationDbContext.Account.AnyAsync(a => a.Username == request.Username.ToUpper()))
				{
					return Forbid(
						authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
						properties: new AuthenticationProperties(new Dictionary<string, string>
						{
							[OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidGrant,
							[OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "The username/password couple is invalid."
						}));
				}

				Account account = await TrinityCoreAuthenticationDbContext.Account.FirstAsync(a => a.Username == request.Username.ToUpper());

				//TC doesn't store a hash anymore, so we must compute verifier.
				using(WoWSRP6PublicComponentHashServiceProvider hashProvider = new WoWSRP6PublicComponentHashServiceProvider())
				{
					//Compute password hash salted with provided salt
					//x doesn't hash salt and hashed password. It actually hashes salt and hashed authstring which is {0}:{1} username:password.
					byte[] hash = hashProvider
						.Hash(account.Salt, hashProvider.Hash(Encoding.ASCII.GetBytes($"{request.Username.ToUpper()}:{request.Password.ToUpper()}".ToUpper())));

					BigInteger gmod = WoWSRP6ServerCryptoServiceProvider.G.ModPow(hash.ToBigInteger(), WoWSRP6ServerCryptoServiceProvider.N);
					byte[] verifier = gmod.ToCleanByteArray();

					if (!verifier.SequenceEqual(account.Verifier))
					{
						return Forbid(
							authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
							properties: new AuthenticationProperties(new Dictionary<string, string>
							{
								[OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidGrant,
								[OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "The username/password couple is invalid."
							}));
					}
				}

				ClaimsIdentity identity = await GenerateClaimsAsync(account);
				ClaimsPrincipal principal = new ClaimsPrincipal(identity);

				// Note: in this sample, the granted scopes match the requested scope
				// but you may want to allow the user to uncheck specific scopes.
				// For that, simply restrict the list of scopes before calling SetScopes.
				principal.SetScopes(request.GetScopes());

				foreach(var claim in principal.Claims)
				{
					claim.SetDestinations(GetDestinations(claim, principal));
				}

				// Returning a SignInResult will ask OpenIddict to issue the appropriate access/identity tokens.
				return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
			}

			throw new InvalidOperationException("The specified grant type is not supported.");
		}

		protected virtual async Task<ClaimsIdentity> GenerateClaimsAsync(Account user)
		{
			string userId = user.Id.ToString();
			string userName = user.Username;

			ClaimsIdentity id = new ClaimsIdentity("Identity.Application", // REVIEW: Used to match Application scheme
				"name",
				"role");
			id.AddClaim(new Claim("sub", userId));
			id.AddClaim(new Claim("name", userName));

			return id;
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
