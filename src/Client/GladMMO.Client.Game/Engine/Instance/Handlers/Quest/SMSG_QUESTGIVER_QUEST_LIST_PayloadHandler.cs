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
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class SMSG_QUESTGIVER_QUEST_LIST_PayloadHandler : BaseGameClientGameMessageHandler<SMSG_QUESTGIVER_QUEST_LIST_Payload>
	{
		private IGossipMenuCreateEventPublisher GossipEventPublisher { get; }

		/// <inheritdoc />
		public SMSG_QUESTGIVER_QUEST_LIST_PayloadHandler(ILog logger,
			[NotNull] IGossipMenuCreateEventPublisher gossipEventPublisher)
			: base(logger)
		{
			GossipEventPublisher = gossipEventPublisher ?? throw new ArgumentNullException(nameof(gossipEventPublisher));
		}

		/// <inheritdoc />
		public override Task HandleMessage(IPeerMessageContext<GamePacketPayload> context, SMSG_QUESTGIVER_QUEST_LIST_Payload payload)
		{
			//Just assume they want it on the main thread so queue it up on the sync contenxt
			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () => await DispatchEvent(payload));
			return Task.CompletedTask;
		}

		private async Task DispatchEvent(SMSG_QUESTGIVER_QUEST_LIST_Payload payload)
		{
			string content = payload.Greeting.GreetingText;

			//TODO: It's hacky to assume ToArrayAvoidCopy won't allocate
			GossipEventPublisher.PublishEvent(this, new GossipMenuCreateEventArgs(payload.QuestGiver, Array.Empty<GossipMenuItem>(), payload.Entries.ToArrayTryAvoidCopy(), content));
		}
	}
}