using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Glader.Essentials;

namespace GladMMO
{
	public interface ICharacterJoinedGuildEventPublisher : IEventPublisher<ICharacterJoinedGuildEventSubscribable, CharacterJoinedGuildEventArgs>
	{

	}

	[AdditionalRegisterationAs(typeof(ICharacterJoinedGuildEventPublisher))]
	[AdditionalRegisterationAs(typeof(ICharacterJoinedGuildEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class CharacterJoinedGuildEventPublisher : ICharacterJoinedGuildEventSubscribable, ICharacterJoinedGuildEventPublisher, IGameInitializable
	{
		public event EventHandler<CharacterJoinedGuildEventArgs> OnCharacterJoinedGuild;

		public void PublishEvent(object sender, CharacterJoinedGuildEventArgs eventArgs)
		{
			OnCharacterJoinedGuild?.Invoke(sender, eventArgs);
		}

		//Hack to get into the scene.
		public Task OnGameInitialized()
		{
			return Task.CompletedTask;
		}
	}
}
