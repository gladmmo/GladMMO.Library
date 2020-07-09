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
	public sealed class InitializeQuestAcceptCallbackInitializable : IGameStartable
	{
		private IUIQuestWindow QuestWindow { get; }

		private IPeerPayloadSendService<GamePacketPayload> SendService { get; }

		private LocalPlayerMenuState MenuState { get; }

		public InitializeQuestAcceptCallbackInitializable(
			[KeyFilter(UnityUIRegisterationKey.QuestWindow)] [NotNull] IUIQuestWindow questWindow,
			[NotNull] IPeerPayloadSendService<GamePacketPayload> sendService,
			[NotNull] LocalPlayerMenuState menuState)
		{
			QuestWindow = questWindow ?? throw new ArgumentNullException(nameof(questWindow));
			SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));
			MenuState = menuState ?? throw new ArgumentNullException(nameof(menuState));
		}

		public async Task OnGameStart()
		{
			QuestWindow.AcceptButton.AddOnClickListenerAsync(async () =>
			{
				//Disable multiple clients, not an exploit issue but just better to not allow them to click accept multiple times.
				QuestWindow.AcceptButton.IsInteractable = false;
				await SendService.SendMessage(new CMSG_QUESTGIVER_ACCEPT_QUEST_Payload(MenuState.CurrentGossipEntity, MenuState.SelectedQuest.QuestId));
			});
		}
	}
}
