using System;
using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
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
	public sealed class SMSG_GOSSIP_MESSAGE_PayloadHandler : BaseGameClientGameMessageHandler<SMSG_GOSSIP_MESSAGE_Payload>
	{
		private IGossipTextContentServiceClient GossipContentClient { get; }

		private IGossipMenuCreateEventPublisher GossipEventPublisher { get; }

		/// <inheritdoc />
		public SMSG_GOSSIP_MESSAGE_PayloadHandler(ILog logger,
			[NotNull] IGossipTextContentServiceClient gossipContentClient,
			[NotNull] IGossipMenuCreateEventPublisher gossipEventPublisher)
			: base(logger)
		{
			GossipContentClient = gossipContentClient ?? throw new ArgumentNullException(nameof(gossipContentClient));
			GossipEventPublisher = gossipEventPublisher ?? throw new ArgumentNullException(nameof(gossipEventPublisher));
		}

		/// <inheritdoc />
		public override async Task HandleMessage(IPeerMessageContext<GamePacketPayload> context, SMSG_GOSSIP_MESSAGE_Payload payload)
		{
			if (Logger.IsDebugEnabled)
			{
				Logger.Debug($"SMSG_GOSSIP_MESSAGE MenuSize: {payload.GossipOptions.Count()} QuestCount: {payload.QuestOptions.Count()}");

				foreach(var menu in payload.GossipOptions)
					Logger.Debug($"Menu: {menu.EntryId} Text: {menu.MenuText}");

				foreach(var quest in payload.QuestOptions)
					Logger.Debug($"Quest: {quest.QuestId} Text: {quest.QuestTitle}");
			}

			//Just assume they want it on the main thread so queue it up on the sync contenxt
			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () => await DispatchEvent(payload));
		}

		private async Task DispatchEvent(SMSG_GOSSIP_MESSAGE_Payload payload)
		{
			string content = String.Empty;
			if(payload.TitleTextId != 0)
			{
				content = await GossipContentClient.GetCreatureGossipTextAsync(payload.TitleTextId);
				Logger.Debug($"Text: {content}");
			}

			//TODO: It's hacky to assume ToArrayAvoidCopy won't allocate
			GossipEventPublisher.PublishEvent(this, new GossipMenuCreateEventArgs(payload.GossipSource, payload.GossipOptions.ToArrayTryAvoidCopy(), payload.QuestOptions.ToArrayTryAvoidCopy(), content));
		}
	}
}