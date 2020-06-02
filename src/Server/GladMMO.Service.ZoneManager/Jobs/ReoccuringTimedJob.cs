using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	public abstract class ReOccurringTimedJob : BackgroundService
	{
		private TimedJobConfig JobConfig { get; }

		protected ILogger<ReOccurringTimedJob> Logger { get; }

		protected ReOccurringTimedJob([NotNull] TimedJobConfig jobConfig,
			[NotNull] ILogger<ReOccurringTimedJob> logger)
		{
			JobConfig = jobConfig ?? throw new ArgumentNullException(nameof(jobConfig));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public override Task StartAsync(CancellationToken cancellationToken)
		{
			if(Logger.IsEnabled(LogLevel.Information))
				Logger.LogInformation($"Job: {GetType().Name} is starting.");

			return base.StartAsync(cancellationToken);
		}

		public override Task StopAsync(CancellationToken cancellationToken)
		{
			if(Logger.IsEnabled(LogLevel.Information))
				Logger.LogInformation($"Job: {GetType().Name} is gracefully stopping.");

			return base.StopAsync(cancellationToken);
		}

		protected override async Task ExecuteAsync(CancellationToken cancellationToken)
		{
			//Design decision to better handle tasks that start up.
			//Don't let them run immediately.
			await Task.Delay(JobConfig.IntervalMilliseconds, cancellationToken);

			while(!cancellationToken.IsCancellationRequested)
			{
				await ExecuteJobAsync(cancellationToken);
				await Task.Delay(JobConfig.IntervalMilliseconds, cancellationToken);
			}
		}

		protected abstract Task ExecuteJobAsync(CancellationToken cancellationToken);
	}
}
