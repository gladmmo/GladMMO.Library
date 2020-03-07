using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	//TODO: Automate user-agent SDK version headers
	[Headers("User-Agent: SDK 0.0.1")]
	public interface IZoneAuthenticationService
	{
		[RequiresAuthentication]
		[Post("/api/register")]
		Task<ZoneServerAccountRegistrationResponse> CreateZoneServerAccount([JsonBody] ZoneServerAccountRegistrationRequest request);

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
		Task<JWTModel> TryAuthenticate([UrlEncodedBody] AuthenticationRequestModel request);
	}
}
