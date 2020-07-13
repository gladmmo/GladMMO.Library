using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using FreecraftCore;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public class SetPlayerMenuQuestStateEventListener : IGameInitializable
	{
		private LocalPlayerMenuState MenuState { get; }

		private ILog Logger { get; }

		public SetPlayerMenuQuestStateEventListener([NotNull] IQuestWindowCreateEventSubscribable questWindowSubscriptionService,
			[NotNull] IQuestRequirementsWindowCreateEventSubscribable requirementsWindowSubscriptionService,
			[NotNull] IQuestCompleteWindowCreateEventSubscribable completeWindowSubscriptionService,
			[NotNull] LocalPlayerMenuState menuState,
			[NotNull] ILog logger)
		{
			if (questWindowSubscriptionService == null) throw new ArgumentNullException(nameof(questWindowSubscriptionService));
			if (requirementsWindowSubscriptionService == null) throw new ArgumentNullException(nameof(requirementsWindowSubscriptionService));
			if (completeWindowSubscriptionService == null) throw new ArgumentNullException(nameof(completeWindowSubscriptionService));
			MenuState = menuState ?? throw new ArgumentNullException(nameof(menuState));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));

			requirementsWindowSubscriptionService.OnQuestRequirementWindowCreate += (sender, args) => SetQuestId(args.QuestGiver, args.QuestId);
			questWindowSubscriptionService.OnQuestWindowCreate += (sender, args) => SetQuestId(args.QuestGiver, args.QuestId);
			completeWindowSubscriptionService.OnQuestCompleteWindowCreate += (sender, args) => SetQuestId(args.QuestGiver, args.QuestId);
		}

		private void SetQuestId(ObjectGuid questGiver, int questId)
		{
			//TODO: Properly handle quest entry for shared quests.
			//Sooo, complicated abit but we need to cover the case of sharing quests.
			//If the current menu UI state doesn't match the guid of who is offerring the quest then the menu state is technically wrong.
			if (MenuState.CurrentGossipEntity != questGiver)
			{
				//TODO: Construct quest entry here.
				QuestGossipEntry questEntry = new QuestGossipEntry(questId, 3, 1, QuestFlags.NONE, false, "TODO: Auto-share title");
				MenuState.Clear();
				MenuState.Update(questGiver, Array.Empty<GossipMenuItem>(), new[] {questEntry});

				if (Logger.IsWarnEnabled)
					Logger.Warn($"Encountered Quest Offer from non-current Gossip (probably shared). TODO: Properly Implement case!");
			}

			//Set the current offered quest state.
			MenuState.SelectQuestByQuestId(questId);
		}

		public Task OnGameInitialized()
		{
			return Task.CompletedTask;
		}
	}
}
