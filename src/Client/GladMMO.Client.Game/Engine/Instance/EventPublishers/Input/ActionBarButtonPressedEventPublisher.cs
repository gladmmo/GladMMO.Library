using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public interface IActionBarButtonPressedEventPublisher : IEventPublisher<IActionBarButtonPressedEventSubscribable, ActionBarButtonPressedEventArgs>
	{

	}

	[AdditionalRegisterationAs(typeof(IActionBarButtonPressedEventSubscribable))]
	[AdditionalRegisterationAs(typeof(IActionBarButtonPressedEventPublisher))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class ActionBarButtonPressedEventPublisher : IActionBarButtonPressedEventPublisher, IActionBarButtonPressedEventSubscribable
	{
		public event EventHandler<ActionBarButtonPressedEventArgs> OnActionBarButtonPressed;

		public void PublishEvent(object sender, ActionBarButtonPressedEventArgs eventArgs)
		{
			//Just forward event.
			OnActionBarButtonPressed?.Invoke(sender, eventArgs);
		}
	}
}
