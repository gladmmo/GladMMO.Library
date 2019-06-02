using System;
using Common.Logging;
using GladNet;
using JetBrains.Annotations;

namespace GladMMO
{
	public sealed class ZoneServerApplicationBaseCreationContext
	{
		public ILog Logger { get; }

		public NetworkAddressInfo ServerAddress { get; }

		/// <inheritdoc />
		public ZoneServerApplicationBaseCreationContext([NotNull] ILog logger, [NotNull] NetworkAddressInfo serverAddress)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			ServerAddress = serverAddress ?? throw new ArgumentNullException(nameof(serverAddress));
		}
	}
}