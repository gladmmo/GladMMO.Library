using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac.Features.AttributeFilters;
using Common.Logging;
using FreecraftCore;
using Glader.Essentials;
using GladNet;

namespace GladMMO
{
	//TODO: Refactor into click registeration and click handler service.
	[AdditionalRegisterationAs(typeof(IQuestCompleteWindowCreateEventSubscribable))]
	[AdditionalRegisterationAs(typeof(IQuestRequirementsWindowCreateEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class InitializeGossipWindowCallbacksInitializable : IGameStartable, IQuestRequirementsWindowCreateEventSubscribable, IQuestCompleteWindowCreateEventSubscribable
	{
		private IUIGossipWindow GossipWindow { get; }

		private IPeerPayloadSendService<GamePacketPayload> SendService { get; }

		private LocalPlayerMenuState MenuState { get; }

		private ILog Logger { get; }

		private LocalPlayerQuestDataContainer QuestDataContainer { get; }

		public event EventHandler<QuestRequirementsWindowCreateEventArgs> OnQuestRequirementWindowCreate;

		public event EventHandler<QuestCompleteWindowCreateEventArgs> OnQuestCompleteWindowCreate;

		private IGossipTextContentServiceClient GossipTextDataClient { get; }

		public InitializeGossipWindowCallbacksInitializable(
			[KeyFilter(UnityUIRegisterationKey.GossipWindow)] [NotNull] IUIGossipWindow gossipWindow,
			[NotNull] IPeerPayloadSendService<GamePacketPayload> sendService,
			[NotNull] LocalPlayerMenuState menuState,
			[NotNull] ILog logger,
			[NotNull] LocalPlayerQuestDataContainer questDataContainer,
			[NotNull] IGossipTextContentServiceClient gossipTextDataClient)
		{
			GossipWindow = gossipWindow ?? throw new ArgumentNullException(nameof(gossipWindow));
			SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));
			MenuState = menuState ?? throw new ArgumentNullException(nameof(menuState));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			QuestDataContainer = questDataContainer ?? throw new ArgumentNullException(nameof(questDataContainer));
			GossipTextDataClient = gossipTextDataClient ?? throw new ArgumentNullException(nameof(gossipTextDataClient));
		}

		public Task OnGameStart()
		{
			for(int i = 0; i < GossipWindow.GossipMenuButtons.Count; i++)
				GossipWindow.GossipMenuButtons[i].AddOnClickListenerAsync(async () =>
				{
					//TODO: Send gossip menu selection for i.
				});

			for(int i = 0; i < GossipWindow.GossipQuestButtons.Count; i++)
			{
				//Yes, this is needed
				int capturedIndex = i;

				GossipWindow.GossipQuestButtons[i].AddOnClickListenerAsync(async () =>
				{
					int index = 0;
					//Find our index/position in the activated quest gossip list
					for(int j = 0; j < GossipWindow.GossipQuestButtons.Count; j++)
					{
						//We found our index!
						if(GossipWindow.GossipQuestButtons[j] == GossipWindow.GossipQuestButtons[capturedIndex])
							break;

						if(GossipWindow.GossipQuestButtons[j].isActive)
							index++;
					}

					try
					{
						QuestGossipEntry questOption = MenuState.QuestOptions[index];

						await SendGossipClickAsync(questOption);
					}
					catch (Exception e)
					{
						if (Logger.IsErrorEnabled)
							Logger.Error($"Failed to send {nameof(CMSG_QUESTGIVER_QUERY_QUEST_Payload)}. Tried Index: {index} QuestOptionsCount: {MenuState.QuestOptions.Count} Error: {e.Message}");
					}
				});
			}

			return Task.CompletedTask;
		}

		private async Task SendGossipClickAsync(QuestGossipEntry questOption)
		{
			int questId = questOption.QuestId;

			//Depending on the quest state, we need to send DIFFERENT packets.
			//If the quest is available/notstarted we need to send CMSG_QUESTGIVER_QUERY_QUEST_Payload
			if (QuestDataContainer.HasStartedQuest(questId))
			{
				//TODO: Send complete quest packet. Strangley it's more like Try Complete.
				if (QuestDataContainer.IsQuestComplete(questId))
				{
					//We don't send TC a packet, because we don't really need to.
					//We just say HEY the quest is done and show the complete screen.
					var completeTextModel = await GossipTextDataClient.GetQuestCompleteGossipTextAsync(questId);

					if (completeTextModel.isSuccessful)
						OnQuestCompleteWindowCreate?.Invoke(this, new QuestCompleteWindowCreateEventArgs(MenuState.CurrentGossipEntity, questId, completeTextModel.Result));
				}
				else
				{
					//Not complete, and we know it, no need to send "Try Complete" packet
					//it will redirect us to the screen for requirements, incomplete or required items page.
					//So we should just do that.
					var incompleteQuestTextModel = await GossipTextDataClient.GetQuestIncompleteGossipTextAsync(questId);

					if (incompleteQuestTextModel.isSuccessful)
						OnQuestRequirementWindowCreate?.Invoke(this, new QuestRequirementsWindowCreateEventArgs(MenuState.CurrentGossipEntity, questId, incompleteQuestTextModel.Result));
				}
			}
			else
				await SendService.SendMessage(new CMSG_QUESTGIVER_QUERY_QUEST_Payload(MenuState.CurrentGossipEntity, questId));
		}
	}
}
