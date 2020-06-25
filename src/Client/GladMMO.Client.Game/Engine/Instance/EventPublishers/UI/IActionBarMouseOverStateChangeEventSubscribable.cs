using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Glader.Essentials;

namespace GladMMO
{
	public interface IActionBarMouseOverStateChangeEventPublisher : IEventPublisher<IActionBarMouseOverStateChangeEventSubscribable, ActionBarMouseOverStateChangeEventArgs>
	{

	}

	[AdditionalRegisterationAs(typeof(IActionBarMouseOverStateChangeEventSubscribable))]
	[AdditionalRegisterationAs(typeof(IActionBarMouseOverStateChangeEventPublisher))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class ActionBarMouseOverStateChangeEventPublisher : IActionBarMouseOverStateChangeEventSubscribable, IActionBarMouseOverStateChangeEventPublisher, IGameInitializable
	{
		public event EventHandler<ActionBarMouseOverStateChangeEventArgs> OnActionBarMouseOverStateChanged;

		public void PublishEvent(object sender, ActionBarMouseOverStateChangeEventArgs eventArgs)
		{
			throw new NotImplementedException();
		}

		public Task OnGameInitialized()
		{
			return Task.CompletedTask;
		}
	}
}
