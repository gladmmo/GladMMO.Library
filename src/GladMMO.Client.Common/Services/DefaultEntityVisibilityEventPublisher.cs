using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public class DefaultEntityVisibilityEventPublisher : INetworkEntityVisibilityEventPublisher, INetworkEntityVisibleEventSubscribable
	{
		public event EventHandler<NetworkEntityNowVisibleEventArgs> OnNetworkEntityNowVisible;

		public void Publish([NotNull] NetworkEntityNowVisibleEventArgs eventArgs)
		{
			if (eventArgs == null) throw new ArgumentNullException(nameof(eventArgs));

			//Just forward the publishing
			OnNetworkEntityNowVisible?.Invoke(this, eventArgs);
		}
	}
}
