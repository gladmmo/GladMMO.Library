using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Glader.Essentials;

namespace GladMMO
{
	public interface IInstanceSceneChangeRequestedEventPublisher : IEventPublisher<IRequestedSceneChangeEventSubscribable, RequestedSceneChangeEventArgs>
	{

	}

	[AdditionalRegisterationAs(typeof(IEventPublisher<IRequestedSceneChangeEventSubscribable, RequestedSceneChangeEventArgs>))]
	[AdditionalRegisterationAs(typeof(IInstanceSceneChangeRequestedEventPublisher))]
	[AdditionalRegisterationAs(typeof(IRequestedSceneChangeEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class GeneralInstanceSceneChangeRequestedEventPublisher : IRequestedSceneChangeEventSubscribable, IGameInitializable, IInstanceSceneChangeRequestedEventPublisher
	{
		public event EventHandler<RequestedSceneChangeEventArgs> OnRequestedSceneChange;

		public void PublishEvent(object sender, RequestedSceneChangeEventArgs eventArgs)
		{
			OnRequestedSceneChange?.Invoke(sender, eventArgs);
		}

		public Task OnGameInitialized()
		{
			//Hack to get it into scene.
			return Task.CompletedTask;
		}
	}
}
