using System;
using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Glader.Essentials;

namespace GladMMO
{

	/// <summary>
	/// Contract for an <see cref="IEventPublisher{TEventPublisherInterface,TEventArgsType}"/> implementation for <see cref="IGossipMenuCreateEventSubscribable"/>
	/// and <see cref="GossipMenuCreateEventArgs"/>.
	/// </summary>
	public interface IGossipMenuCreateEventPublisher : IEventPublisher<IGossipMenuCreateEventSubscribable, GossipMenuCreateEventArgs>
	{

	}

	/// <summary>
	/// Default <see cref="IGossipMenuCreateEventPublisher"/> implementation.
	/// </summary>
	[AdditionalRegisterationAs(typeof(IGossipMenuCreateEventSubscribable))]
	[AdditionalRegisterationAs(typeof(IGossipMenuCreateEventPublisher))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class GossipMenuCreateEventPublisher : IGossipMenuCreateEventPublisher, IGossipMenuCreateEventSubscribable, IGameInitializable
	{
		public event EventHandler<GossipMenuCreateEventArgs> OnGossipMenuCreate;

		public void PublishEvent(object sender, GossipMenuCreateEventArgs eventArgs)
		{
			//Just forward event.
			OnGossipMenuCreate?.Invoke(sender, eventArgs);
		}

		public Task OnGameInitialized()
		{
			return Task.CompletedTask;
		}
	}
}