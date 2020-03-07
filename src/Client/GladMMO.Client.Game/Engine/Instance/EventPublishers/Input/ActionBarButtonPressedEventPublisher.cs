using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Glader.Essentials;

namespace GladMMO
{
	public interface IActionBarButtonPressedEventPublisher : IEventPublisher<IActionBarButtonPressedEventSubscribable, ActionBarButtonPressedEventArgs>
	{

	}

	[AdditionalRegisterationAs(typeof(IActionBarButtonPressedEventSubscribable))]
	[AdditionalRegisterationAs(typeof(IActionBarButtonPressedEventPublisher))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class ActionBarButtonPressedEventPublisher : IActionBarButtonPressedEventPublisher, IActionBarButtonPressedEventSubscribable, IGameInitializable
	{
		public event EventHandler<ActionBarButtonPressedEventArgs> OnActionBarButtonPressed;

		public void PublishEvent(object sender, ActionBarButtonPressedEventArgs eventArgs)
		{
			//Just forward event.
			OnActionBarButtonPressed?.Invoke(sender, eventArgs);
		}

		public Task OnGameInitialized()
		{
			return Task.CompletedTask;
		}
	}
}
