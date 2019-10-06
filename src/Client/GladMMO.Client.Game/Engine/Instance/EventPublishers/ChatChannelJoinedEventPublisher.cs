using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Glader.Essentials;

namespace GladMMO
{
	[AdditionalRegisterationAs(typeof(IEventPublisher<IChatChannelJoinedEventSubscribable, ChatChannelJoinedEventArgs>))]
	[AdditionalRegisterationAs(typeof(IChatChannelJoinedEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class ChatChannelJoinedEventPublisher : IEventPublisher<IChatChannelJoinedEventSubscribable, ChatChannelJoinedEventArgs>, IChatChannelJoinedEventSubscribable, IGameInitializable
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
