using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	/// <summary>
	/// Proxy interface for Authentication Server RPCs.
	/// </summary>
	[Headers("User-Agent: GuardiansClient")]
	public interface IAuthenticationService
	{
		/// <summary>
		/// Authenticate request method. Sends the request model as a URLEncoded body.
		/// See the documentation for information about the endpoint.
		/// https://github.com/HaloLive/Documentation
		/// </summary>
		/// <param name="request">The request model.</param>
		/// <returns>The authentication result.</returns>
		//TODO: Refit doesn't support error code suppresion.
		//[SupressResponseErrorCodes((int)HttpStatusCode.BadRequest)] //OAuth spec returns 400 BadRequest on failed auth
		[Post("/api/auth")]
		Task<PlayerAccountJWTModel> TryAuthenticate([UrlEncodedBody] AuthenticationRequestModel request);

		[Obsolete("This is for DEV only.")]
		[Post("/api/registeration?username={username}&password={password}")]
		Task<string> TryRegister(string username, string password);
	}
}
