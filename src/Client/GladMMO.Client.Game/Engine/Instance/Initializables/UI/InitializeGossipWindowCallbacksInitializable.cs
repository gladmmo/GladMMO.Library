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
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class InitializeGossipWindowCallbacksInitializable : IGameStartable
	{
		private IUIGossipWindow GossipWindow { get; }

		private IPeerPayloadSendService<GamePacketPayload> SendService { get; }

		private LocalPlayerMenuState MenuState { get; }

		private ILog Logger { get; }

		public InitializeGossipWindowCallbacksInitializable(
			[KeyFilter(UnityUIRegisterationKey.GossipWindow)] [NotNull] IUIGossipWindow gossipWindow,
			[NotNull] IPeerPayloadSendService<GamePacketPayload> sendService,
			[NotNull] LocalPlayerMenuState menuState,
			[NotNull] ILog logger)
		{
			GossipWindow = gossipWindow ?? throw new ArgumentNullException(nameof(gossipWindow));
			SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));
			MenuState = menuState ?? throw new ArgumentNullException(nameof(menuState));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
						int questId = MenuState.QuestOptions[index].QuestId;

						await SendService.SendMessage(new CMSG_QUESTGIVER_QUERY_QUEST_Payload(MenuState.CurrentGossipEntity, questId));
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
	}
}
