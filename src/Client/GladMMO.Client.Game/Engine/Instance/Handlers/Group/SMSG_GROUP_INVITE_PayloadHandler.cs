﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using FreecraftCore;
using GladNet;

namespace GladMMO
{
	//TODO: Properly implement this.
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class SMSG_GROUP_INVITE_PayloadHandler : BaseGameClientGameMessageHandler<SMSG_GROUP_INVITE_Payload>
	{
		public SMSG_GROUP_INVITE_PayloadHandler(ILog logger)
			: base(logger)
		{

		}

		public override Task HandleMessage(IPeerMessageContext<GamePacketPayload> context, SMSG_GROUP_INVITE_Payload payload)
		{
			//This is for demo purposes, just auto-accepting.
			context.PayloadSendService.SendMessage(new CMSG_GROUP_ACCEPT_Payload());
			return Task.CompletedTask;
		}
	}
}