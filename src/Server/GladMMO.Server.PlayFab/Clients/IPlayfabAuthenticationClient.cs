using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlayFab.ClientModels;
using Refit;

namespace GladMMO
{
	[Headers("User-Agent: GuardiansServer")]
	public interface IPlayfabAuthenticationClient
	{
		//Full path: POST https://titleId.playfabapi.com/Client/LoginWithOpenIdConnect
		//See: https://docs.microsoft.com/en-us/rest/api/playfab/client/authentication/loginwithopenidconnect?view=playfab-rest
		/// <summary>
		/// Attempts to login/authenticate with a provide OpenId value.
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[Post("/Client/LoginWithOpenIdConnect")]
		Task<PlayFabResultModel<GladMMOPlayFabLoginResult>> LoginWithOpenId(LoginWithOpenIdConnectRequest request);
	}
}
