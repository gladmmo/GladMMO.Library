using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class LogQuestTurnInToConsoleEventListener : BaseQuestTurnInEventListener
	{
		private IChatTextMessageRecievedEventPublisher ChatMessageEventPublisher { get; }

		public LogQuestTurnInToConsoleEventListener(IQuestTurnInEventSubscribable subscriptionService,
			[NotNull] IChatTextMessageRecievedEventPublisher chatMessageEventPublisher) 
			: base(subscriptionService)
		{
			ChatMessageEventPublisher = chatMessageEventPublisher ?? throw new ArgumentNullException(nameof(chatMessageEventPublisher));
		}

		protected override void OnEventFired(object source, QuestTurnInEventArgs args)
		{
			//Log the rewards
			ChatMessageEventPublisher.PublishEvent(this, new TextChatEventArgs($"Quest {args.QuestId} Completed! TODO: Name", ChatChannelType.GamePlay));

			if (args.Reward.Experience > 0)
				ChatMessageEventPublisher.PublishEvent(this, new TextChatEventArgs($"Experience Earned: {args.Reward.Experience}", ChatChannelType.GamePlay));

			if (args.Reward.Money > 0)
				ChatMessageEventPublisher.PublishEvent(this, new TextChatEventArgs($"Earned Gold: {args.Reward.Money}", ChatChannelType.GamePlay));
		}
	}
}
