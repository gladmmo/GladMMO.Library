using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using GladNet;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.PreZoneBurstingScreen)]
	public sealed class PreBurstSessionClaimOnClientConnectionEventListener : BaseSingleEventListenerInitializable<INetworkConnectionEstablishedEventSubscribable>
	{
		private IPeerPayloadSendService<GameClientPacketPayload> SendService { get; }

		private IReadonlyAuthTokenRepository AuthTokenRepository { get; }

		private ICharacterDataRepository CharacterDataRepository { get; }

		/// <inheritdoc />
		public PreBurstSessionClaimOnClientConnectionEventListener(INetworkConnectionEstablishedEventSubscribable subscriptionService, [NotNull] IPeerPayloadSendService<GameClientPacketPayload> sendService, [NotNull] IReadonlyAuthTokenRepository authTokenRepository, [NotNull] ICharacterDataRepository characterDataRepository) 
			: base(subscriptionService)
		{
			SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));
			AuthTokenRepository = authTokenRepository ?? throw new ArgumentNullException(nameof(authTokenRepository));
			CharacterDataRepository = characterDataRepository ?? throw new ArgumentNullException(nameof(characterDataRepository));
		}

		/// <inheritdoc />
		protected override async void OnEventFired(object source, EventArgs args)
		{
			//TODO: Check result
			//We don't need to be on the main thread to send a session claim request.
			/*await SendService.SendMessage(new ClientSessionClaimRequestPayload(AuthTokenRepository.RetrieveWithType(), CharacterDataRepository.CharacterId))
				.ConfigureAwait(false);*/
		}
	}
}
