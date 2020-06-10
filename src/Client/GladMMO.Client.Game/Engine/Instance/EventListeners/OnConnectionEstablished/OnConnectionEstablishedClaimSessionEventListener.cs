using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using GladNet;
using Nito.AsyncEx;
using Nito.AsyncEx.Synchronous;
using Reinterpret.Net;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class OnConnectionEstablishedClaimSessionEventListener : BaseSingleEventListenerInitializable<INetworkConnectionEstablishedEventSubscribable>
	{
		private IPeerPayloadSendService<GamePacketPayload> SendService { get; }

		public OnConnectionEstablishedClaimSessionEventListener(INetworkConnectionEstablishedEventSubscribable subscriptionService,
			[NotNull] IPeerPayloadSendService<GamePacketPayload> sendService)
			: base(subscriptionService)
		{
			SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));
		}

		protected override void OnEventFired(object source, EventArgs args)
		{
			ProjectVersionStage.AssertAlpha();

			//HelloKitty: So, auth challenge data is useless unless you wanna reconnect/redirect
			//so we should just sent the auth session request
			//TODO: We have a hack here to use realmID as the account id. We really need to move eventually to reading the auth token on TC.
			SendService.SendMessage(new SessionAuthProofRequest(ClientBuild.Wotlk_3_2_2a, PreBetaUsernameStorage.UserName, ((int) 0).Reinterpret(), new RealmIdentification(1), new byte[20], new AddonChecksumInfo[0])).WaitAndUnwrapException();

			//Idea here is that TC won't let a character login to the game unless they've gotten the character list first.
			//So to deal with this, on connection to the game we'll request the character list
			//and then in the character list handler or something we'll send a request to login as a character.
			SendService.SendMessage(new CharacterListRequest()).WaitAndUnwrapException();
		}
	}
}
