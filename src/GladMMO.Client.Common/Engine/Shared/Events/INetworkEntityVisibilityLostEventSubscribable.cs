using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface INetworkEntityVisibilityLostEventSubscribable
	{
		event EventHandler<NetworkEntityVisibilityLostEventArgs> OnNetworkEntityVisibilityLost;
	}

	public sealed class NetworkEntityVisibilityLostEventArgs : EventArgs, IEntityGuidContainer
	{
		/// <summary>
		/// Entity guid of the entity that visibility was lost for.
		/// </summary>
		public NetworkEntityGuid EntityGuid { get; }

		/// <inheritdoc />
		public NetworkEntityVisibilityLostEventArgs([NotNull] NetworkEntityGuid entityGuid)
		{
			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
		}
	}
}
