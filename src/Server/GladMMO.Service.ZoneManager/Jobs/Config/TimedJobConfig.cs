using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladMMO
{
	public abstract class TimedJobConfig
	{
		public int IntervalMilliseconds { get; }

		protected TimedJobConfig(int intervalMilliseconds)
		{
			if (intervalMilliseconds <= 0) throw new ArgumentOutOfRangeException(nameof(intervalMilliseconds));

			IntervalMilliseconds = intervalMilliseconds;
		}
	}

	public sealed class TimedJobConfig<T> : TimedJobConfig
	{
		public TimedJobConfig(int intervalMilliseconds) 
			: base(intervalMilliseconds)
		{

		}
	}
}
