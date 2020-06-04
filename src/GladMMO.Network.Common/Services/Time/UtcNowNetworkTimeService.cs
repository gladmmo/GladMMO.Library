using System; using FreecraftCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GladMMO
{
	//TODO: Unit test, when I'm not lazy.
	public sealed class UtcNowNetworkTimeService : INetworkTimeService
	{
		public long EPOCH_TICKS { get; } = new DateTime(1970, 1, 1).Ticks / TimeSpan.TicksPerMillisecond;

		/// <inheritdoc />
		public long CurrentTimeOffset { get; private set; }

		//This becomes time since system startup in ticks.
		/// <inheritdoc />
		public long CurrentLocalTime => Environment.TickCount;

		/// <inheritdoc />
		public long CurrentLatency { get; private set; }

		/// <inheritdoc />
		public long LastQueryTime { get; private set; }

		/*ct - rt = to
		rt = -(to - ct)
		rt = ct - to*/
		/// <inheritdoc />
		public long CurrentRemoteTime
		{
			get
			{
				long value = (CurrentLocalTime - CurrentTimeOffset);
				return value;
			}
		}

		/// <inheritdoc />
		public int MillisecondsSinceStartup => Environment.TickCount;

		/// <inheritdoc />
		public long CalculateRoundTripTime(long originalLocalTime)
		{
			return CalculateRoundTripTime(originalLocalTime, CurrentLocalTime);
		}

		private long CalculateRoundTripTime(long originalLocalTime, long currentTicks)
		{
			//This is no way this should be negative, but we might want to check.
			return (currentTicks - originalLocalTime);
		}

		/// <inheritdoc />
		public long CalculateTimeOffset(long originalLocalTime, long remoteTime)
		{
			long currentTicks = CurrentLocalTime;

			//time diff is basically the difference between any server timestamps we recieve from the local time.
			//So ServerTime + the timeDiff will be the local time something should be.
			//We remove approxlatency (RTT) from the local client time because we make the assumption that
			//the server actually created the timestamp at AbsoluteServerTime - MessageTravelTime (RTT)
			long timeDiff = (currentTicks - CalculateRoundTripTime(originalLocalTime, currentTicks) / 2) - remoteTime;

			return timeDiff;
		}

		/// <inheritdoc />
		public void SetTimeSyncronization(long originalLocalTime, long remoteTime)
		{
			CurrentTimeOffset = CalculateTimeOffset(originalLocalTime, remoteTime);
			CurrentLatency = CalculateRoundTripTime(originalLocalTime) / 2;
		}

		/// <inheritdoc />
		public void RecalculateQueryTime()
		{
			LastQueryTime = CurrentLocalTime;
		}
	}
}
