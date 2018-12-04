﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace Guardians
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
		Task<JWTModel> TryAuthenticate([UrlEncodedBody] AuthenticationRequestModel request);
	}
}
