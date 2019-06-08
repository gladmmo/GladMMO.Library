using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	//mostly exists becuase local player and remote entity spawns are in different packets.
	/// <summary>
	/// Publishes the provided events as a <see cref="INetworkEntityVisibleEventSubscribable"/> event
	/// </summary>
	public interface INetworkEntityVisibilityEventPublisher
	{
		/// <summary>
		/// Publishes the provided event <see cref="eventArgs"/>
		/// </summary>
		/// <param name="eventArgs">The event args to publish.</param>
		void Publish(NetworkEntityNowVisibleEventArgs eventArgs);
	}
}
