using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class ChatMessageHandlerTickable : EventQueueBasedTickable<IChatTextMessageRecievedEventSubscribable, TextChatEventArgs>
	{
		private IChatMessageBoxReciever ChatReciever { get; }

		public ChatMessageHandlerTickable(IChatTextMessageRecievedEventSubscribable subscriptionService, 
			ILog logger,
			[NotNull] IChatMessageBoxReciever chatReciever) 
			: base(subscriptionService, false, logger) //don't service all at once, for performance reasons.
		{
			ChatReciever = chatReciever ?? throw new ArgumentNullException(nameof(chatReciever));
		}

		protected override void HandleEvent(TextChatEventArgs args)
		{
			//TODO: Handle tabs one day... nah
			ChatReciever.ReceiveChatMessage(1, args.Message);
		}
	}
}
