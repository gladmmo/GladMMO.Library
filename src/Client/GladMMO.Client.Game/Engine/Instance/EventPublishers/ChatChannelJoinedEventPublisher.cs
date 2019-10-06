using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Glader.Essentials;

namespace GladMMO
{
	/// <summary>
	/// Simplified interface for <see cref="IEventPublisher{TEventPublisherInterface,TEventArgsType}"/>
	/// for event <see cref="IChatChannelJoinedEventSubscribable"/>.
	/// </summary>
	public interface IChatChannelJoinedEventPublisher : IEventPublisher<IChatChannelJoinedEventSubscribable, ChatChannelJoinedEventArgs>
	{

	}

	[AdditionalRegisterationAs(typeof(IEventPublisher<IChatChannelJoinedEventSubscribable, ChatChannelJoinedEventArgs>))]
	[AdditionalRegisterationAs(typeof(IChatChannelJoinedEventPublisher))]
	[AdditionalRegisterationAs(typeof(IChatChannelJoinedEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class ChatChannelJoinedEventPublisher : IChatChannelJoinedEventPublisher, IChatChannelJoinedEventSubscribable, IGameInitializable
	{
		public event EventHandler<ChatChannelJoinedEventArgs> OnChatChannelJoined;

		public void PublishEvent(object sender, [NotNull] ChatChannelJoinedEventArgs eventArgs)
		{
			if (eventArgs == null) throw new ArgumentNullException(nameof(eventArgs));

			OnChatChannelJoined?.Invoke(sender, eventArgs);
		}

		public async Task OnGameInitialized()
		{

		}
	}
}
