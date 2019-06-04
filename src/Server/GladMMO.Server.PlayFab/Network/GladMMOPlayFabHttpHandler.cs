using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GladMMO
{
	public class GladMMOPlayFabHttpHandler : HttpClientHandler
	{
		private GladMMOPlayFabClientConfiguration Config { get; }

		public GladMMOPlayFabHttpHandler(GladMMOPlayFabClientConfiguration config)
		{
			Config = config ?? throw new ArgumentNullException(nameof(config));
		}

		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (request.Headers.Contains(GladMMOPlayfabConstants.PLAYFAB_SECRET_KEY_HEADER))
			{
				Config.AssertContainsSecretKey();

				//This will set the authentication token if it's required.
				request.Headers.Remove(GladMMOPlayfabConstants.PLAYFAB_SECRET_KEY_HEADER);
				request.Headers.Add(GladMMOPlayfabConstants.PLAYFAB_SECRET_KEY_HEADER, Config.PlayFabSecretKey);
			}

			return base.SendAsync(request, cancellationToken);
		}
	}
}
