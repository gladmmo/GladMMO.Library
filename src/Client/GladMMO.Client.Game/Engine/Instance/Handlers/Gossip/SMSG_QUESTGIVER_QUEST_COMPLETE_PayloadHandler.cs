using System;
using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using GladMMO;
using GladNet;

namespace GladMMO
{
	[AdditionalRegisterationAs(typeof(IQuestTurnInEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class SMSG_QUESTGIVER_QUEST_COMPLETE_PayloadHandler : BaseGameClientGameMessageHandler<SMSG_QUESTGIVER_QUEST_COMPLETE_Payload>, IQuestTurnInEventSubscribable
	{
		public event EventHandler<QuestTurnInEventArgs> OnQuestTurnIn;

		/// <inheritdoc />
		public SMSG_QUESTGIVER_QUEST_COMPLETE_PayloadHandler(ILog logger)
			: base(logger)
		{

		}

		/// <inheritdoc />
		public override Task HandleMessage(IPeerMessageContext<GamePacketPayload> context, SMSG_QUESTGIVER_QUEST_COMPLETE_Payload payload)
		{
			OnQuestTurnIn.InvokeMainThread(this, new QuestTurnInEventArgs(payload.QuestId, new QuestRewardModel(payload.ExperienceRewarded, payload.MoneyRewarded)));
			return Task.CompletedTask;
		}
	}
}