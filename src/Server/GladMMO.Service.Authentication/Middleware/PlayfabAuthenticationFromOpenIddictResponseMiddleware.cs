using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using PlayFab;
using PlayFab.ClientModels;
using Refit;
using Reinterpret.Net;

namespace GladMMO
{
	public class PlayfabAuthenticationFromOpenIddictResponseMiddleware
	{
		private RequestDelegate Next { get; }

		private IPlayfabAuthenticationClient AuthenticationClient { get; }

		public PlayfabAuthenticationFromOpenIddictResponseMiddleware(RequestDelegate next,
			[NotNull] IPlayfabAuthenticationClient authenticationClient,
			[NotNull] UserManager<GuardiansApplicationUser> userManager)
		{
			Next = next ?? throw new ArgumentNullException(nameof(next));
			AuthenticationClient =
				authenticationClient ?? throw new ArgumentNullException(nameof(authenticationClient));
		}

		public async Task Invoke(HttpContext context)
		{
			if (context.Request.Path.HasValue && context.Request.Path.ToUriComponent()
				    .Contains(AuthenticationController.AUTHENTICATION_ROUTE_VALUE))
			{
				var existingBodyStream = context.Response.Body;

				using (var newBodyStream = new MemoryStream())
				{
					// We set the response body to our stream so we can read after the chain of middlewares have been called.
					context.Response.Body = newBodyStream;

					// execute the rest of the pipeline
					await Next(context);

					//Now we should check the content response
					//If it's OK then authentication was successful.
					if (context.Response.StatusCode == StatusCodes.Status200OK)
					{
						newBodyStream.Seek(0, SeekOrigin.Begin);
						ServerSidePlayerAccountJWTModel PlayerAccountJWTModel = Deserialize<ServerSidePlayerAccountJWTModel>(newBodyStream);
						newBodyStream.Seek(0, SeekOrigin.Begin);

						//At this point, we need to actually query PlayFab and authenticate the user.
						GladMMOPlayFabLoginResult playAuthToken = await AuthenticateWithPlayfab(PlayerAccountJWTModel.OpenId);

						if(String.IsNullOrWhiteSpace(playAuthToken.SessionTicket))
							throw new InvalidOperationException($"Encountered Null PlayFab Authentication Token.");

						PlayerAccountJWTModel jwtResponseModel = new PlayerAccountJWTModel(PlayerAccountJWTModel.AccessToken);
						jwtResponseModel.PlayfabAuthenticationToken = playAuthToken.SessionTicket;
						jwtResponseModel.PlayfabId = playAuthToken.PlayFabId;

						//Now we actually have to write the new JWT model to the response stream
						byte[] bytes = JsonConvert.SerializeObject(jwtResponseModel).Reinterpret(Encoding.ASCII);
						newBodyStream.SetLength(bytes.Length);
						context.Response.ContentLength = bytes.Length;
						await newBodyStream.WriteAsync(bytes, 0, bytes.Length);
						newBodyStream.Seek(0, SeekOrigin.Begin);
					}

					//Copy the contents of the new memory stream (which contains the response) to the original stream, which is then returned to the client.
					await newBodyStream.CopyToAsync(existingBodyStream, (int)newBodyStream.Length);
					context.Response.Body = existingBodyStream;
				}
			}
			else
				//Execute like normal
				await Next(context);
		}

		private async Task<GladMMOPlayFabLoginResult> AuthenticateWithPlayfab([JetBrains.Annotations.NotNull] string id_token)
		{
			if (string.IsNullOrWhiteSpace(id_token))
				throw new ArgumentException("Value cannot be null or whitespace.", nameof(id_token));

			try
			{
				//TODO: For GladMMO this needs to be made configurable.
				/*PlayFabResultModel<GladMMOPlayFabLoginResult> result = await AuthenticationClient.LoginWithOpenId(new LoginWithOpenIdConnectRequest()
				{
					ConnectionId = "VRGuardiansOAuth",
					CreateAccount = true,
					IdToken = id_token,
					TitleId = "63815"
				});

				if (result == null)
					throw new InvalidOperationException($"Refit returned invalid {nameof(LoginResult)} model. Was null.");

				//TODO: Don't assume it's successful.
				return result.Data;*/
				return new GladMMOPlayFabLoginResult("TODO-Reimplement-Playerfab-Ticket");
			}
			catch (ApiException e)
			{
				Console.WriteLine($"Error: {e.Message} Response Content: {e.Content}");
				throw;
			}
		}

		public static T Deserialize<T>(Stream s)
		{
			using (StreamReader reader = new StreamReader(s, Encoding.Default, false, (int)s.Length, true))
			using (JsonTextReader jsonReader = new JsonTextReader(reader))
			{
				JsonSerializer ser = new JsonSerializer();
				return ser.Deserialize<T>(jsonReader);
			}
		}
	}
}
