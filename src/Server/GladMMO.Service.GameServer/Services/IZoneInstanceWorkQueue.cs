using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GladMMO
{
	//The concept here is we'll have a populated, or potentially empty, work queue
	//for bringing up zone/instances for end users. This could be locally just a queue
	//or it could be AWS SQS.
	public interface IZoneInstanceWorkQueue
	{
		/// <summary>
		/// Indicates if there are any instance requests
		/// for end users.
		/// </summary>
		bool isEmpty { get; }

		/// <summary>
		/// Could return null if no work entry is found.
		/// </summary>
		/// <param name="token"></param>
		/// <returns></returns>
		Task<ZoneInstanceWorkEntry> DequeueAsync(CancellationToken token);

		/// <summary>
		/// Could return null if no work entry is found.
		/// </summary>
		/// <returns></returns>
		Task<ZoneInstanceWorkEntry> DequeueAsync();
	}
}
