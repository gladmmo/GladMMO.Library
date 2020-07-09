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
	[AdditionalRegisterationAs(typeof(IGossipCompleteEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class SMSG_GOSSIP_COMPLETE_PayloadHandler : BaseGameClientGameMessageHandler<SMSG_GOSSIP_COMPLETE_Payload>, IGossipCompleteEventSubscribable
	{
		public event EventHandler<GossipCompleteEventArgs> OnGossipComplete;

		/// <inheritdoc />
		public SMSG_GOSSIP_COMPLETE_PayloadHandler(ILog logger)
			: base(logger)
		{
		}

		/// <inheritdoc />
		public override Task HandleMessage(IPeerMessageContext<GamePacketPayload> context, SMSG_GOSSIP_COMPLETE_Payload payload)
		{
			//Just publish the event on the main thread.
			UnityAsyncHelper.UnityMainThreadContext.Post(o => OnGossipComplete?.Invoke(this, new GossipCompleteEventArgs()), null);
			return Task.CompletedTask;
		}
	}
}