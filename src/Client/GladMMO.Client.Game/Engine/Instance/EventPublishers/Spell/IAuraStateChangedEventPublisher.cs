using System;
using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Glader.Essentials;

namespace GladMMO
{

	/// <summary>
	/// Contract for an <see cref="IEventPublisher{TEventPublisherInterface,TEventArgsType}"/> implementation for <see cref="IAuraStateChangedEventSubscribable"/>
	/// and <see cref="AuraStateChangedEventArgs"/>.
	/// </summary>
	public interface IAuraStateChangedEventPublisher : IEventPublisher<IAuraStateChangedEventSubscribable, AuraStateChangedEventArgs>
	{

	}

	/// <summary>
	/// Default <see cref="IAuraStateChangedEventPublisher"/> implementation.
	/// </summary>
	[AdditionalRegisterationAs(typeof(IAuraStateChangedEventSubscribable))]
	[AdditionalRegisterationAs(typeof(IAuraStateChangedEventPublisher))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class AuraStateChangedEventPublisher : IAuraStateChangedEventPublisher, IAuraStateChangedEventSubscribable, IGameInitializable
	{
		public event EventHandler<AuraStateChangedEventArgs> OnAuraStateChanged;

		public void PublishEvent(object sender, AuraStateChangedEventArgs eventArgs)
		{
			//Just forward event.
			OnAuraStateChanged?.Invoke(sender, eventArgs);
		}

		public Task OnGameInitialized()
		{
			return Task.CompletedTask;
		}
	}
}