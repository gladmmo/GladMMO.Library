using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Glader.Essentials;

namespace GladMMO
{
	public interface IActionBarButtonStateChangedEventPublisher : IEventPublisher<IActionBarButtonStateChangedEventSubscribable, ActionBarButtonStateChangedEventArgs>
	{

	}

	[AdditionalRegisterationAs(typeof(IActionBarButtonStateChangedEventSubscribable))]
	[AdditionalRegisterationAs(typeof(IActionBarButtonStateChangedEventPublisher))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class ActionBarButtonStateChangedEventPublisher : IActionBarButtonStateChangedEventPublisher, IActionBarButtonStateChangedEventSubscribable, IGameInitializable
	{
		public event EventHandler<ActionBarButtonStateChangedEventArgs> OnActionBarButtonStateChanged;

		public void PublishEvent(object sender, ActionBarButtonStateChangedEventArgs eventArgs)
		{
			OnActionBarButtonStateChanged?.Invoke(sender, eventArgs);
		}

		public Task OnGameInitialized()
		{
			return Task.CompletedTask;
		}
	}
}
