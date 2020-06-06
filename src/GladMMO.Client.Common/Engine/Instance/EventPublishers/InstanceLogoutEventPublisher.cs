using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Glader.Essentials;

namespace GladMMO
{
	public interface IInstanceLogoutEventPublisher : IEventPublisher<IInstanceLogoutEventSubscribable, EventArgs>
	{

	}

	[AdditionalRegisterationAs(typeof(IInstanceLogoutEventPublisher))]
	[AdditionalRegisterationAs(typeof(IInstanceLogoutEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class InstanceLogoutEventPublisher : IInstanceLogoutEventSubscribable, IInstanceLogoutEventPublisher, IGameInitializable
	{
		public event EventHandler OnInstanceLogout;

		public void PublishEvent(object sender, EventArgs eventArgs)
		{
			OnInstanceLogout?.Invoke(sender, eventArgs);
		}

		public Task OnGameInitialized()
		{
			return Task.CompletedTask;
		}
	}
}
