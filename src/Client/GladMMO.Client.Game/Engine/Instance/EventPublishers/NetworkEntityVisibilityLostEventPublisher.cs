using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Glader.Essentials;

namespace GladMMO
{
	public interface INetworkEntityVisibilityLostEventPublisher : IEventPublisher<INetworkEntityVisibilityLostEventSubscribable, NetworkEntityVisibilityLostEventArgs>
	{

	}

	[AdditionalRegisterationAs(typeof(INetworkEntityVisibilityLostEventSubscribable))]
	[AdditionalRegisterationAs(typeof(INetworkEntityVisibilityLostEventPublisher))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class NetworkEntityVisibilityLostEventPublisher : INetworkEntityVisibilityLostEventSubscribable, INetworkEntityVisibilityLostEventPublisher, IGameInitializable
	{
		public event EventHandler<NetworkEntityVisibilityLostEventArgs> OnNetworkEntityVisibilityLost;

		public void PublishEvent(object sender, NetworkEntityVisibilityLostEventArgs eventArgs)
		{
			OnNetworkEntityVisibilityLost?.Invoke(sender, eventArgs);
		}

		//Get it into the scene.
		public Task OnGameInitialized()
		{
			return Task.CompletedTask;
		}
	}
}
