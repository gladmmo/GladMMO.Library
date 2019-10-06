using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Glader.Essentials;

namespace GladMMO
{
	[AdditionalRegisterationAs(typeof(IEventPublisher<IChatTextMessageRecievedEventSubscribable, TextChatEventArgs>))]
	[AdditionalRegisterationAs(typeof(IChatTextMessageRecievedEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class TextChatMessageEventPublisher : IEventPublisher<IChatTextMessageRecievedEventSubscribable, TextChatEventArgs>, IChatTextMessageRecievedEventSubscribable, IGameInitializable
	{
		public event EventHandler<TextChatEventArgs> OnTextChatMessageRecieved;

		public void PublishEvent(object sender, [NotNull] TextChatEventArgs eventArgs)
		{
			if (eventArgs == null) throw new ArgumentNullException(nameof(eventArgs));

			OnTextChatMessageRecieved?.Invoke(sender, eventArgs);
		}

		public async Task OnGameInitialized()
		{

		}
	}
}
