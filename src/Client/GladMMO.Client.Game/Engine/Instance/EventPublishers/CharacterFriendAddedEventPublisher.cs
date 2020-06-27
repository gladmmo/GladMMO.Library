using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Glader.Essentials;

namespace GladMMO
{
	/// <summary>
	/// Contract for an <see cref="IEventPublisher{TEventPublisherInterface,TEventArgsType}"/> implementation for <see cref="ICharacterFriendAddedEventSubscribable"/>
	/// and <see cref="CharacterFriendAddedEventArgs"/>.
	/// </summary>
	public interface ICharacterFriendAddedEventPublisher : IEventPublisher<ICharacterFriendAddedEventSubscribable, CharacterFriendAddedEventArgs>
	{

	}

	/// <summary>
	/// Default <see cref="ICharacterFriendAddedEventPublisher"/> implementation.
	/// </summary>
	[AdditionalRegisterationAs(typeof(ICharacterFriendAddedEventSubscribable))]
	[AdditionalRegisterationAs(typeof(ICharacterFriendAddedEventPublisher))]
	[AdditionalRegisterationAs(typeof(IEventPublisher<ICharacterFriendAddedEventSubscribable, CharacterFriendAddedEventArgs>))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class CharacterFriendAddedEventPublisher : ICharacterFriendAddedEventSubscribable, ICharacterFriendAddedEventPublisher, IGameInitializable
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
