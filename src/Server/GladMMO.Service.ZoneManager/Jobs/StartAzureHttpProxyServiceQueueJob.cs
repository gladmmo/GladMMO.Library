using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	public class StartAzureHttpProxyServiceQueueJob : IHostedService
	{
		private ProxiedHttpRequestAzureServiceQueueManager ProxiedHttpRequestManager { get; }

		private ILogger<StartAzureHttpProxyServiceQueueJob> Logger { get; }

		public StartAzureHttpProxyServiceQueueJob([NotNull] ProxiedHttpRequestAzureServiceQueueManager proxiedHttpRequestManager, [NotNull] ILogger<StartAzureHttpProxyServiceQueueJob> logger)
		{
			ProxiedHttpRequestManager = proxiedHttpRequestManager ?? throw new ArgumentNullException(nameof(proxiedHttpRequestManager));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			try
			{
				await ProxiedHttpRequestManager.StartAsync();
			}
			catch (Exception e)
			{
				if(Logger.IsEnabled(LogLevel.Error))
					Logger.LogError($"Failed to start {GetType().Name}. Reason: {e.Message}");
				throw;
			}
		}

		public async Task StopAsync(CancellationToken cancellationToken)
		{
			try
			{
				await ProxiedHttpRequestManager.DisposeAsync();
			}
			catch (Exception e)
			{
				if(Logger.IsEnabled(LogLevel.Error))
					Logger.LogError($"Failed to stop {GetType().Name}. Reason: {e.Message}");
				throw;
			}
		}
	}
}
