using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace GladMMO
{
	//We're gonna use AWS SQS most liely but this works for a local test queue
	public sealed class LocalInMemoryZoneInstanceWorkQueue : IZoneInstanceWorkQueue
	{
		private ConcurrentQueue<ZoneInstanceWorkEntry> MemoryBackedQueue { get; }

		/// <inheritdoc />
		public LocalInMemoryZoneInstanceWorkQueue([NotNull] ConcurrentQueue<ZoneInstanceWorkEntry> memoryBackedQueue)
		{
			MemoryBackedQueue = memoryBackedQueue ?? throw new ArgumentNullException(nameof(memoryBackedQueue));
		}

		public LocalInMemoryZoneInstanceWorkQueue(params ZoneInstanceWorkEntry[] entries)
		{
			if(entries == null || entries.Length == 0)
				MemoryBackedQueue = new ConcurrentQueue<ZoneInstanceWorkEntry>();
			else
				MemoryBackedQueue = new ConcurrentQueue<ZoneInstanceWorkEntry>(entries);
		}

		public LocalInMemoryZoneInstanceWorkQueue()
		{
			MemoryBackedQueue = new ConcurrentQueue<ZoneInstanceWorkEntry>();
		}

		/// <inheritdoc />
		public bool isEmpty => MemoryBackedQueue.IsEmpty;

		/// <inheritdoc />
		public Task<ZoneInstanceWorkEntry> DequeueAsync(CancellationToken token)
		{
			return DequeueAsync();
		}

		/// <inheritdoc />
		public Task<ZoneInstanceWorkEntry> DequeueAsync()
		{
			MemoryBackedQueue.TryDequeue(out var outVal);
			return Task.FromResult(outVal);
		}
	}
}
