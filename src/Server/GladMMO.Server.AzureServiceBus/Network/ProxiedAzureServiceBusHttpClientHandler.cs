using System; using FreecraftCore;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace GladMMO
{
	public sealed class ProxiedAzureServiceBusHttpClientHandler : HttpClientHandler
	{
		private IReadonlyAuthTokenRepository AuthTokenProvider { get; }

		private IQueueClient AzureServiceBusClient { get; }

		private ILog Logger { get; }

		/// <inheritdoc />
		public ProxiedAzureServiceBusHttpClientHandler([NotNull] IReadonlyAuthTokenRepository authTokenProvider, IQueueClient azureServiceBusClient, ILog logger)
		{
			AuthTokenProvider = authTokenProvider ?? throw new ArgumentNullException(nameof(authTokenProvider));
			AzureServiceBusClient = azureServiceBusClient ?? throw new ArgumentNullException(nameof(azureServiceBusClient));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			ProxiedHttpMethod method;

			switch (request.Method.Method)
			{
				case "POST":
					method = ProxiedHttpMethod.Post;
					break;
				case "PUT":
					method = ProxiedHttpMethod.Put;
					break;
				case "PATCH":
					method = ProxiedHttpMethod.Patch;
					break;
				default:
					throw new InvalidOperationException($"Cannot handle MethodType: {request.Method.Method}");
			}

			ProxiedHttpRequestModel proxiedRequestModel = new ProxiedHttpRequestModel(method, await request.Content.ReadAsStringAsync(), $"{AuthTokenProvider.RetrieveType()} {AuthTokenProvider.Retrieve()}", request.RequestUri.AbsolutePath);

			try
			{
				await AzureServiceBusClient.SendAsync(new Message(UTF8Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(proxiedRequestModel))));
			}
			catch (Exception e)
			{
				if(Logger.IsErrorEnabled)
					Logger.Error($"Failed to send proxied Azure Service Bus HTTP Message. Reason: {e.ToString()}");

				return new HttpResponseMessage(HttpStatusCode.BadRequest){Content = new StringContent($"Client Exception: {e.ToString()}")};
			}

			//Indicate accepted since it's not technically handled.
			return new HttpResponseMessage(HttpStatusCode.Accepted);
		}
	}
}