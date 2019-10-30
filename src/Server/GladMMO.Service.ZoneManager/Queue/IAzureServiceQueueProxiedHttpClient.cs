using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Refit;
using UnityEngine.Scripting;

namespace GladMMO
{
	[Headers("User-Agent: AzureServiceBus")]
	public interface IAzureServiceQueueProxiedHttpClient
	{
		[Post("{route}")]
		[Headers("Content-Type: application/json")]
		Task SendProxiedPostAsync(string jsonBodyContent, [AliasAs("route")] string requestPath, [Header("Authorization")] string authorizationToken);

		[Patch("{route}")]
		[Headers("Content-Type: application/json")]
		Task SendProxiedPatchAsync(string jsonBodyContent, [AliasAs("route")] string requestPath, [Header("Authorization")] string authorizationToken);

		[Put("{route}")]
		[Headers("Content-Type: application/json")]
		Task SendProxiedPutAsync(string jsonBodyContent, [AliasAs("route")] string requestPath, [Header("Authorization")] string authorizationToken);
	}
}
