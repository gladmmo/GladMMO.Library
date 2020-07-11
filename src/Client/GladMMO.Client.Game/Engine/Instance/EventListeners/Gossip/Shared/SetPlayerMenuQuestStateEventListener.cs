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

		public SetPlayerMenuQuestStateEventListener(IQuestWindowCreateEventSubscribable questWindowSubscriptionService,
			IQuestRequirementsWindowCreateEventSubscribable requirementsWindowSubscriptionService,
			[NotNull] LocalPlayerMenuState menuState,
			[NotNull] ILog logger)
		{
			MenuState = menuState ?? throw new ArgumentNullException(nameof(menuState));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));

			requirementsWindowSubscriptionService.OnQuestRequirementWindowCreate += (sender, args) => SetQuestId(args.QuestGiver, args.QuestId);
			questWindowSubscriptionService.OnQuestWindowCreate += (sender, args) => SetQuestId(args.QuestGiver, args.QuestId);
		}

		private void SetQuestId(ObjectGuid questGiver, int questId)
		{
			//TODO: Properly handle quest entry for shared quests.
			//Sooo, complicated abit but we need to cover the case of sharing quests.
			//If the current menu UI state doesn't match the guid of who is offerring the quest then the menu state is technically wrong.
			if (MenuState.CurrentGossipEntity != questGiver)
			{
				//TODO: Construct quest entry here.
				QuestGossipEntry questEntry = null; //new QuestGossipEntry();
				MenuState.Clear();
				MenuState.Update(questGiver, Array.Empty<GossipMenuItem>(), new[] {questEntry});

				if (Logger.IsErrorEnabled)
					Logger.Error($"Encountered Quest Offer from non-current Gossip (probably shared). TODO: Implement case.");
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
