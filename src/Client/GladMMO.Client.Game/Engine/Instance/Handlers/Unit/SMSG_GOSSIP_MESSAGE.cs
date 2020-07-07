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

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class SMSG_GOSSIP_MESSAGE_PayloadHandler : BaseGameClientGameMessageHandler<SMSG_GOSSIP_MESSAGE_Payload>
	{
		/// <inheritdoc />
		public SMSG_GOSSIP_MESSAGE_PayloadHandler(ILog logger)
			: base(logger)
		{

		}

		/// <inheritdoc />
		public override Task HandleMessage(IPeerMessageContext<GamePacketPayload> context, SMSG_GOSSIP_MESSAGE_Payload payload)
		{
			if (Logger.IsDebugEnabled)
			{
				Logger.Debug($"SMSG_GOSSIP_MESSAGE MenuSize: {payload.GossipOptions.Count()} QuestCount: {payload.QuestOptions.Count()}");

				foreach(var menu in payload.GossipOptions)
					Logger.Debug($"Menu: {menu.EntryId} Text: {menu.MenuText}");

				foreach(var quest in payload.QuestOptions)
					Logger.Debug($"Quest: {quest.QuestId} Text: {quest.QuestTitle}");
			}

			return Task.CompletedTask;
		}
	}
}