using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Glader.Essentials;

namespace GladMMO
{
	//This is just a debugging component.
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class DebugLogChatMessagesEventListener : BaseSingleEventListenerInitializable<IChatChannelJoinedEventSubscribable, ChatChannelJoinedEventArgs>
	{
		private ILog Logger { get; }

		public DebugLogChatMessagesEventListener(IChatChannelJoinedEventSubscribable subscriptionService,
			[NotNull] ILog logger) 
			: base(subscriptionService)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void OnEventFired(object source, ChatChannelJoinedEventArgs args)
		{
			args.Channel.OnChannelTextMessageRecieved += (s, message) => Logger.Debug($"Recieved Message. Sender: {message.Sender.Name} Content: {message.Message}")
		}
	}
}
