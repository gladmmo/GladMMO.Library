using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface INetworkTimeService : IReadonlyNetworkTimeService
	{
		/// <summary>
		/// Sets the time syncronization for the network time service.
		/// This will initialize the properties exposed for time usage.
		/// </summary>
		/// <param name="originalLocalTime">The original local time sent in the syncronization packet.</param>
		/// <param name="remoteTime">The remote time sent back.</param>
		void SetTimeSyncronization(long originalLocalTime, long remoteTime);

		/// <summary>
		/// Recalculates the time stamp for when a time query was sent.
		/// (current local time.
		/// </summary>
		void RecalculateQueryTime();

		/// <summary>
		/// Represents the timestamp from the last time query.
		/// </summary>
		long LastQueryTime { get; }
	}

	public interface IReadonlyNetworkTimeService
	{
		/// <summary>
		/// Caclualtes the RTT (round-trip-time) from a provided initial <see cref="originalLocalTime"/>.
		/// </summary>
		/// <param name="originalLocalTime">The original local time.</param>
		/// <returns>The RTT (2 * latency).</returns>
		long CalculateRoundTripTime(long originalLocalTime);

		/// <summary>
		/// Caclulates the offset between the remote and client clocks.
		/// This offset can be used to convert remote times to
		/// client times.
		/// </summary>
		/// <param name="originalLocalTime"></param>
		/// <param name="remoteTime"></param>
		/// <returns></returns>
		long CalculateTimeOffset(long originalLocalTime, long remoteTime);

		/// <summary>
		/// The current computed time offset.
		/// (Does no computation).
		/// Includes Latency.
		/// </summary>
		long CurrentTimeOffset { get; }

		/// <summary>
		/// The current local time.
		/// (Depending on implementation this could be expensive to call).
		/// </summary>
		long CurrentLocalTime { get; }

		/// <summary>
		/// The current computed latency.
		/// (Does no computation).
		/// </summary>
		long CurrentLatency { get; }

		/// <summary>
		/// The approximated current remote time.
		/// Likely based on the <see cref="CurrentLocalTime"/> and the <see cref="CurrentTimeOffset"/>.
		/// </summary>
		long CurrentRemoteTime { get; }

		/// <summary>
		/// Represents the milliseconds since application startup.
		/// </summary>
		int MillisecondsSinceStartup { get; }
	}
}
