using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	public sealed class ExpiredZoneServerCleanupJob : ReOccurringTimedJob
	{
		private IZoneServerRepository ZoneServerRepository { get; }

		public ExpiredZoneServerCleanupJob(TimedJobConfig<ExpiredZoneServerCleanupJob> jobConfig, 
			ILogger<ReOccurringTimedJob> logger,
			[NotNull] IZoneServerRepository zoneServerRepository) 
			: base(jobConfig, logger)
		{
			ZoneServerRepository = zoneServerRepository ?? throw new ArgumentNullException(nameof(zoneServerRepository));
		}

		protected override async Task ExecuteJobAsync(CancellationToken cancellationToken)
		{
			//All we need to do is cleanup the expired zones
			try
			{
				await ZoneServerRepository.CleanupExpiredZonesAsync(cancellationToken);
			}
			catch (Exception e)
			{
				if(Logger.IsEnabled(LogLevel.Error))
					Logger.LogError($"Failed to cleanup expired zones. Reason: {e.ToString()}");

				throw;
			}
		}
	}
}
