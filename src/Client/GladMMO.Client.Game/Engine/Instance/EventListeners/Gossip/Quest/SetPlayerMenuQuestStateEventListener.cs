using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using FreecraftCore;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public class SetPlayerMenuQuestStateEventListener : BaseQuestWindowCreateEventListener
	{
		private LocalPlayerMenuState MenuState { get; }

		private ILog Logger { get; }

		public SetPlayerMenuQuestStateEventListener(IQuestWindowCreateEventSubscribable subscriptionService,
			[NotNull] LocalPlayerMenuState menuState,
			[NotNull] ILog logger) 
			: base(subscriptionService)
		{
			MenuState = menuState ?? throw new ArgumentNullException(nameof(menuState));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void OnEventFired(object source, QuestWindowCreateEventArgs args)
		{
			//TODO: Properly handle quest entry for shared quests.
			//Sooo, complicated abit but we need to cover the case of sharing quests.
			//If the current menu UI state doesn't match the guid of who is offerring the quest then the menu state is technically wrong.
			if (MenuState.CurrentGossipEntity != args.QuestGiver)
			{
				//TODO: Construct quest entry here.
				QuestGossipEntry questEntry = null; //new QuestGossipEntry();
				MenuState.Clear();
				MenuState.Update(args.QuestGiver, Array.Empty<GossipMenuItem>(), new []{ questEntry });

				if (Logger.IsErrorEnabled)
					Logger.Error($"Encountered Quest Offer from non-current Gossip (probably shared). TODO: Implement case.");
			}

			//Set the current offered quest state.
			MenuState.SelectQuestByQuestId(args.QuestId);
		}
	}
}
