using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Primitives;
using AspNet.Security.OpenIdConnect.Server;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;

namespace GladMMO
{
	//From an old OpenIddict OAuth sample and a slightly modified version that I personally use
	//in https://github.com/GladLive/GladLive.Authentication/blob/master/src/GladLive.Authentication.OAuth/Controllers/AuthorizationController.cs
	[Route(AUTHENTICATION_ROUTE_VALUE)]
	public class AuthenticationController : Controller
	{
		internal const string AUTHENTICATION_ROUTE_VALUE = "api/auth";

		private IOptions<IdentityOptions> IdentityOptions { get; }

		private ILogger<AuthenticationController> Logger { get; }

		private wotlk_authContext TrinityCoreDatabaseContext { get; }

		public AuthenticationController(
			ILogger<AuthenticationController> logger, 
			wotlk_authContext trinityCoreDatabaseContext,
			IOptions<IdentityOptions> identityOptions)
		{
			//IdentityOptions = identityOptions;
			Logger = logger;
			TrinityCoreDatabaseContext = trinityCoreDatabaseContext;
			IdentityOptions = identityOptions;
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

			bool isAnyAccountExist = await TrinityCoreDatabaseContext.Account.AnyAsync(a => a.Username == username.ToUpper());
			//Like Identity we check if the user exists.
			if(!isAnyAccountExist)
			{
				return BadRequest(new OpenIdConnectResponse
				{
					Error = OpenIdConnectConstants.Errors.InvalidGrant,
					ErrorDescription = "The username/password couple is invalid."
				});
			}

			//This checks to see if they're banned.
			Account account = await TrinityCoreDatabaseContext
				.Account
				.FirstAsync(a => a.Username == username.ToUpper());

			if(await TrinityCoreDatabaseContext.AccountBanned.AnyAsync(ab => ab.Id == account.Id && ab.Active > 0))
			{
				return BadRequest(new OpenIdConnectResponse
				{
					Error = OpenIdConnectConstants.Errors.InvalidGrant,
					ErrorDescription = "The user couple is banned."
				});
			}

			// Ensure the password is valid.
			if(account.ShaPassHash != CreateHash(username, password))
			{
				return BadRequest(new OpenIdConnectResponse
				{
					Error = OpenIdConnectConstants.Errors.InvalidGrant,
					ErrorDescription = "The username/password couple is invalid."
				});
			}

			// Create a new authentication ticket.
			AuthenticationTicket ticket = await CreateTicketAsync(scopes, account);

			return SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);
		}

		[HttpPost]
		[Produces("application/json")]
		public async Task<IActionResult> Exchange(OpenIddictRequest request)
		{
			return await Authenticate(request.Username, request.Password, request.GetScopes());
		}

		private async Task<AuthenticationTicket> CreateTicketAsync(IEnumerable<string> scopes, Account user)
		{
			// Create a new ClaimsPrincipal containing the claims that
			// will be used to create an id_token, a token or a code.
			ClaimsIdentity identity = await GenerateClaimsAsync(user);

			// Create a new authentication ticket holding the user identity.
			var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity),
				new Microsoft.AspNetCore.Authentication.AuthenticationProperties(),
				OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
			
			// Set the list of scopes granted to the client application.
			ticket.Principal.SetScopes(new[]
			{
				OpenIdConnectConstants.Scopes.OpenId,
				OpenIdConnectConstants.Scopes.Profile,
				OpenIddictConstants.Scopes.Roles
			}.Intersect(scopes.Concat(new string[1] { OpenIdConnectConstants.Scopes.OpenId }))); //HelloKitty: Always include the OpenId, it's required for the Playfab authentication

			ticket.Principal.SetResources("auth-server");

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
				if ((claim.Type == OpenIdConnectConstants.Claims.Name && ticket.Principal.HasScope(OpenIdConnectConstants.Scopes.Profile)) ||
					(claim.Type == OpenIdConnectConstants.Claims.Email && ticket.Principal.HasScope(OpenIdConnectConstants.Scopes.Email)) ||
					(claim.Type == OpenIdConnectConstants.Claims.Role && ticket.Principal.HasScope(OpenIddictConstants.Claims.Role)))
				{
					destinations.Add(OpenIdConnectConstants.Destinations.IdentityToken);
				}

				claim.SetDestinations(destinations);
			}

			return ticket;
		}

		protected virtual async Task<ClaimsIdentity> GenerateClaimsAsync(Account user)
		{
			string userId = user.Id.ToString();
			string userName = user.Username;

			ClaimsIdentity id = new ClaimsIdentity("Identity.Application", // REVIEW: Used to match Application scheme
				IdentityOptions.Value.ClaimsIdentity.UserNameClaimType,
				IdentityOptions.Value.ClaimsIdentity.RoleClaimType);
			id.AddClaim(new Claim(IdentityOptions.Value.ClaimsIdentity.UserIdClaimType, userId));
			id.AddClaim(new Claim(IdentityOptions.Value.ClaimsIdentity.UserNameClaimType, userName));

			return id;
		}

		public string CreateHash(string username, string password)
		{
			if(username == null) throw new ArgumentNullException(nameof(username));
			if(password == null) throw new ArgumentNullException(nameof(password));


			//Insecure, but it's what World of Warcraft uses.
			using(SHA1 sha = new SHA1CryptoServiceProvider())
			{
				string hashInput = $"{username.ToUpper()}:{password.ToUpper()}";
				byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(hashInput));

				return ByteArrayToHexViaLookup32Unsafe(hash);
			}
		}

		//See: https://stackoverflow.com/questions/311165/how-do-you-convert-a-byte-array-to-a-hexadecimal-string-and-vice-versa/24343727#24343727
		private static readonly uint[] _lookup32Unsafe = CreateLookup32Unsafe();
		private static readonly unsafe uint* _lookup32UnsafeP = (uint*)GCHandle.Alloc(_lookup32Unsafe, GCHandleType.Pinned).AddrOfPinnedObject();

		private static uint[] CreateLookup32Unsafe()
		{
			var result = new uint[256];
			for(int i = 0; i < 256; i++)
			{
				string s = i.ToString("X2");
				if(BitConverter.IsLittleEndian)
					result[i] = ((uint)s[0]) + ((uint)s[1] << 16);
				else
					result[i] = ((uint)s[1]) + ((uint)s[0] << 16);
			}

			return result;
		}

		public static unsafe string ByteArrayToHexViaLookup32Unsafe(byte[] bytes)
		{
			var lookupP = _lookup32UnsafeP;
			var result = new char[bytes.Length * 2];
			fixed(byte* bytesP = bytes)
			fixed(char* resultP = result)
			{
				uint* resultP2 = (uint*)resultP;
				for(int i = 0; i < bytes.Length; i++)
				{
					resultP2[i] = lookupP[bytesP[i]];
				}
			}

			return new string(result);
		}
	}
}
