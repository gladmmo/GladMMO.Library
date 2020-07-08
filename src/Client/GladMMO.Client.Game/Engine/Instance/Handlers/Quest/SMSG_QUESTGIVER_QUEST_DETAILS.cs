using System;
using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using GladMMO;
using GladNet;
using Nito.AsyncEx;

namespace GladMMO
{
	[AdditionalRegisterationAs(typeof(IQuestWindowCreateEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class SMSG_QUESTGIVER_QUEST_DETAILS_PayloadHandler : BaseGameClientGameMessageHandler<SMSG_QUESTGIVER_QUEST_DETAILS_Payload>, IQuestWindowCreateEventSubscribable
	{
		public event EventHandler<QuestWindowCreateEventArgs> OnQuestWindowCreate;

		private IGossipTextContentServiceClient TextContentService { get; }

		/// <inheritdoc />
		public SMSG_QUESTGIVER_QUEST_DETAILS_PayloadHandler(ILog logger,
			[NotNull] IGossipTextContentServiceClient textContentService)
			: base(logger)
		{
			TextContentService = textContentService ?? throw new ArgumentNullException(nameof(textContentService));
		}

		/// <inheritdoc />
		public override Task HandleMessage(IPeerMessageContext<GamePacketPayload> context, SMSG_QUESTGIVER_QUEST_DETAILS_Payload payload)
		{
			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				ResponseModel<QuestTextContentModel, GameContentQueryResponseCode> response = await TextContentService.GetQuestGossipTextAsync(payload.QuestId);

				//IF we cannot load the content for it, don't broadcast
				if (response.isSuccessful)
					OnQuestWindowCreate?.Invoke(this, new QuestWindowCreateEventArgs(payload.QuestGiver, response.Result));
			});

			return Task.CompletedTask;
		}
	}
}