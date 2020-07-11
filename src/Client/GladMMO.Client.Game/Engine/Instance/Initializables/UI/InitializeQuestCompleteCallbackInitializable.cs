using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac.Features.AttributeFilters;
using FreecraftCore;
using Glader.Essentials;
using GladNet;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class InitializeQuestCompleteCallbackInitializable : IGameStartable
	{
		private IUIQuestCompleteWindow QuestCompleteWindow { get; }

		private IPeerPayloadSendService<GamePacketPayload> SendService { get; }

		private LocalPlayerMenuState MenuState { get; }

		public InitializeQuestCompleteCallbackInitializable(
			[KeyFilter(UnityUIRegisterationKey.QuestCompleteWindow)] [NotNull] IUIQuestCompleteWindow questCompleteWindow,
			[NotNull] IPeerPayloadSendService<GamePacketPayload> sendService,
			[NotNull] LocalPlayerMenuState menuState)
		{
			QuestCompleteWindow = questCompleteWindow ?? throw new ArgumentNullException(nameof(questCompleteWindow));
			SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));
			MenuState = menuState ?? throw new ArgumentNullException(nameof(menuState));
		}

		public async Task OnGameStart()
		{
			QuestCompleteWindow.AcceptButton.AddOnClickListenerAsync(async () =>
			{
				//Disable multiple clients, not an exploit issue but just better to not allow them to click accept multiple times.
				QuestCompleteWindow.AcceptButton.IsInteractable = false;
				await SendService.SendMessage(new CMSG_QUESTGIVER_CHOOSE_REWARD_Payload(MenuState.CurrentGossipEntity, MenuState.SelectedQuest.QuestId, 0));
			});
		}
	}
}
