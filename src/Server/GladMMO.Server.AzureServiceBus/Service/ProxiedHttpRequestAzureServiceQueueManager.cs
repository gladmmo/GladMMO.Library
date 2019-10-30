using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace GladMMO
{
	/// <summary>
	/// Implementation of <see cref="BaseAzureServiceQueueManager"/> that treats
	/// a queue as an HTTP request proxy and forwards packaged requests.
	/// </summary>
	public class ProxiedHttpRequestAzureServiceQueueManager : BaseAzureServiceQueueManager
	{
		private IAzureServiceQueueProxiedHttpClient ProxyClient { get; }

		private ILog Logger { get; }

		public ProxiedHttpRequestAzureServiceQueueManager(IQueueClient serviceQueue,
			[NotNull] IAzureServiceQueueProxiedHttpClient proxyClient,
			[NotNull] ILog logger) 
			: base(serviceQueue)
		{
			ProxyClient = proxyClient ?? throw new ArgumentNullException(nameof(proxyClient));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override async Task HandleQueueExceptionAsync(ExceptionReceivedEventArgs exceptionEventArgs)
		{
			if(Logger.IsErrorEnabled)
				Logger.Error($"Encountered AzureQueue Error. Context: {exceptionEventArgs.ExceptionReceivedContext} Reason: {exceptionEventArgs.Exception.ToString()}");
		}

		protected override Task HandleQueueIncomingMessageAsync(Message message, CancellationToken cancellationToken)
		{
			//TODO: Handle failure case.
			//Assume all messages in this queue are proxied request models.
			ProxiedHttpRequestModel httpRequestModel = JsonConvert.DeserializeObject<ProxiedHttpRequestModel>(Encoding.UTF8.GetString(message.Body));

			if(Logger.IsInfoEnabled)
				Logger.Info($"Sending proxied Azure Service Queue HTTP Request. Method: {httpRequestModel.Method.ToString()} Route: {httpRequestModel.Route} Content: {httpRequestModel.SerializedJsonBody}");

			switch (httpRequestModel.Method)
			{
				case ProxiedHttpMethod.Post:
					return ProxyClient.SendProxiedPostAsync(httpRequestModel.SerializedJsonBody, httpRequestModel.Route, httpRequestModel.AuthorizationToken);
				case ProxiedHttpMethod.Put:
					return ProxyClient.SendProxiedPutAsync(httpRequestModel.SerializedJsonBody, httpRequestModel.Route, httpRequestModel.AuthorizationToken);
				case ProxiedHttpMethod.Patch:
					return ProxyClient.SendProxiedPatchAsync(httpRequestModel.SerializedJsonBody, httpRequestModel.Route, httpRequestModel.AuthorizationToken);
				default:
					throw new ArgumentOutOfRangeException($"Cannot handle MethodType: {httpRequestModel.Method}");
			}
		}
	}
}
