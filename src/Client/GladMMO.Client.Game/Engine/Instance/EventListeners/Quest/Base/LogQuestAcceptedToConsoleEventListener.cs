using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class LogQuestAcceptedToConsoleEventListener : BaseSingleEventListenerInitializable<ILocalPlayerQuestAddedEventSubscribable, LocalPlayerQuestAddedEventArgs>
	{
		private IChatTextMessageRecievedEventPublisher ChatPublisher { get; }

		public LogQuestAcceptedToConsoleEventListener(ILocalPlayerQuestAddedEventSubscribable subscriptionService,
			[NotNull] IChatTextMessageRecievedEventPublisher chatPublisher) 
			: base(subscriptionService)
		{
			ChatPublisher = chatPublisher ?? throw new ArgumentNullException(nameof(chatPublisher));
		}

		protected override void OnEventFired(object source, LocalPlayerQuestAddedEventArgs args)
		{
			ChatPublisher.PublishEvent(this, new TextChatEventArgs($"Quest Accepted: {args.QuestId} TODO add name.", ChatChannelType.System));
		}
	}
}
