using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Glader.Essentials;

namespace GladMMO
{
	public interface ICharacterFriendAddedEventPublisher : IEventPublisher<ICharacterFriendAddedEventSubscribable, CharacterFriendAddedEventArgs>
	{

	}

	[AdditionalRegisterationAs(typeof(ICharacterFriendAddedEventSubscribable))]
	[AdditionalRegisterationAs(typeof(ICharacterFriendAddedEventPublisher))]
	[AdditionalRegisterationAs(typeof(IEventPublisher<ICharacterFriendAddedEventSubscribable, CharacterFriendAddedEventArgs>))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class CharacterFriendAddedEventPublisher : ICharacterFriendAddedEventSubscribable, IEventPublisher<ICharacterFriendAddedEventSubscribable, CharacterFriendAddedEventArgs>, IGameInitializable
	{
		public event EventHandler<CharacterFriendAddedEventArgs> OnCharacterFriendAdded;

		public void PublishEvent(object sender, CharacterFriendAddedEventArgs eventArgs)
		{
			OnCharacterFriendAdded?.Invoke(sender, eventArgs);
		}

		//Hack
		public Task OnGameInitialized()
		{
			return Task.CompletedTask;
		}
	}
}
