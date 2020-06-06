using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Common.Logging;
using Glader.Essentials;
using GladNet;
using Nito.AsyncEx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class SendLogoutPacketEventListener : ButtonClickedEventListener<ISceneBackButtonClickedSubscribable>
	{
		private IConnectionService ConnectionService { get; }

		private IPeerPayloadSendService<GamePacketPayload> SendService { get; }

		private ILocalClientMessageSenderService LocalSendService { get; }

		public SendLogoutPacketEventListener(ISceneBackButtonClickedSubscribable subscriptionService,
			[NotNull] IPeerPayloadSendService<GamePacketPayload> sendService,
			[NotNull] IConnectionService connectionService,
			[NotNull] ILocalClientMessageSenderService localSendService) 
			: base(subscriptionService)
		{
			SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));
			ConnectionService = connectionService ?? throw new ArgumentNullException(nameof(connectionService));
			LocalSendService = localSendService ?? throw new ArgumentNullException(nameof(localSendService));
		}

		protected override void OnEventFired(object source, ButtonClickedEventArgs args)
		{
			//Idea here, is if we're connected lets send a LOGOUT request
			//and then the server will respond and we can actually broadcast logout there
			//otherwise we can SPOOF a logout packet.
			if (ConnectionService.isConnected)
				SendService.SendMessage(new CMSG_LOGOUT_REQUEST_Payload());
			else
				LocalSendService.HandleMessage(new SMSG_LOGOUT_RESPONSE_Payload(true));
		}
	}
}
