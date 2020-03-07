using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	/// <summary>
	/// Refit interface for proxied HTTP actions using input from AzureServiceBus.
	/// </summary>
	[Headers("User-Agent: AzureServiceBus")]
	public interface IAzureServiceQueueProxiedHttpClient
	{
		//When using ** we cannot aliasas the route parameter. It must match.

		//** ensures that the slashes exist.
		//First slash is required by Refit sadly.
		[Post("/{**route}")]
		[Headers("Content-Type: application/json")]
		Task SendProxiedPostAsync(string route, [Body] string jsonBodyContent, [Header("Authorization")] string authorizationToken);

		[Patch("/{**route}")]
		[Headers("Content-Type: application/json")]
		Task SendProxiedPatchAsync(string route, [Body] string jsonBodyContent, [Header("Authorization")] string authorizationToken);

		[Put("/{**route}")]
		[Headers("Content-Type: application/json")]
		Task SendProxiedPutAsync(string route, [Body] string jsonBodyContent, [Header("Authorization")] string authorizationToken);
	}
}
