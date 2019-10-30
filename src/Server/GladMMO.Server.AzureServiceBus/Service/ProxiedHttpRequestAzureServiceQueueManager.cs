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

		protected override async Task HandleQueueIncomingMessageAsync(Message message, CancellationToken cancellationToken)
		{
			try
			{
				//TODO: Handle failure case.
				//Assume all messages in this queue are proxied request models.
				ProxiedHttpRequestModel httpRequestModel = JsonConvert.DeserializeObject<ProxiedHttpRequestModel>(Encoding.UTF8.GetString(message.Body));

				if(Logger.IsInfoEnabled)
					Logger.Info($"Sending proxied Azure Service Queue HTTP Request. Method: {httpRequestModel.Method.ToString()} Route: {httpRequestModel.Route} Content: {httpRequestModel.SerializedJsonBody}");

				//Skip the first slash.
				string route = httpRequestModel.Route.Remove(0, 1);

				switch(httpRequestModel.Method)
				{
					case ProxiedHttpMethod.Post:
						await ProxyClient.SendProxiedPostAsync(route, httpRequestModel.SerializedJsonBody, httpRequestModel.AuthorizationToken);
						break;
					case ProxiedHttpMethod.Put:
						await ProxyClient.SendProxiedPutAsync(route, httpRequestModel.SerializedJsonBody, httpRequestModel.AuthorizationToken);
						break;
					case ProxiedHttpMethod.Patch:
						await ProxyClient.SendProxiedPatchAsync(route, httpRequestModel.SerializedJsonBody, httpRequestModel.AuthorizationToken);
						break;
					default:
						throw new ArgumentOutOfRangeException($"Cannot handle MethodType: {httpRequestModel.Method}");
				}

				//Indicate that the message has been consumed if we haven't canceled.
				if(!cancellationToken.IsCancellationRequested)
					await ServiceQueue.CompleteAsync(message.SystemProperties.LockToken);
			}
			catch (Exception e)
			{
				//If not canceled, we want to abandon it so that other consumers maybe can handle it.
				if(!cancellationToken.IsCancellationRequested)
					await ServiceQueue.AbandonAsync(message.SystemProperties.LockToken);

				throw;
			}
		}
	}
}
