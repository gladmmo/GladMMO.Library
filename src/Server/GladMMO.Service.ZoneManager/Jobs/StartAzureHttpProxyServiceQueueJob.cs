using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace GladMMO
{
	public class StartAzureHttpProxyServiceQueueJob : IHostedService
	{
		private ProxiedHttpRequestAzureServiceQueueManager ProxiedHttpRequestManager { get; }

		public StartAzureHttpProxyServiceQueueJob([NotNull] ProxiedHttpRequestAzureServiceQueueManager proxiedHttpRequestManager)
		{
			ProxiedHttpRequestManager = proxiedHttpRequestManager ?? throw new ArgumentNullException(nameof(proxiedHttpRequestManager));
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			await ProxiedHttpRequestManager.StartAsync();
		}

		public async Task StopAsync(CancellationToken cancellationToken)
		{
			await ProxiedHttpRequestManager.DisposeAsync();
		}
	}
}
