using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using GladNet;
using JetBrains.Annotations;
using UnityEngine;

namespace GladMMO
{
	[AdditionalRegisterationAs(typeof(IPlayerSessionClaimedEventSubscribable))]
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class ClientSessionClaimRequestHandler : BaseServerRequestHandler<ClientSessionClaimRequestPayload>, IPlayerSessionClaimedEventSubscribable
	{
		private IZoneServerToGameServerClient GameServerClient { get; }

		/// <inheritdoc />
		public event EventHandler<PlayerSessionClaimedEventArgs> OnSuccessfulSessionClaimed;

		/// <inheritdoc />
		public ClientSessionClaimRequestHandler(
			[NotNull] IZoneServerToGameServerClient gameServerClient,
			[NotNull] ILog logger)
			: base(logger)
		{
			GameServerClient = gameServerClient ?? throw new ArgumentNullException(nameof(gameServerClient));
		}

		/// <inheritdoc />
		public override async Task HandleMessage(IPeerSessionMessageContext<GameServerPacketPayload> context, ClientSessionClaimRequestPayload payload)
		{
			//TODO: We need better validation/authorization for clients trying to claim a session. Right now it's open to malicious attack
			ZoneServerTryClaimSessionResponse zoneServerTryClaimSessionResponse = null;
			try
			{
				ProjectVersionStage.AssertAlpha();
				zoneServerTryClaimSessionResponse = await GameServerClient.TryClaimSession(new ZoneServerTryClaimSessionRequest(await GameServerClient.GetAccountIdFromToken(payload.JWT), payload.CharacterId))
					.ConfigureAwait(false);
			}
			catch(Exception e) //we could get an unauthorized response
			{
				Logger.Error($"Failed to Query for AccountId: {e.Message}. AuthToken provided was: {payload.JWT}");
				throw;
			}

			if(!zoneServerTryClaimSessionResponse.isSuccessful)
			{
				//TODO: Better error code
				await context.PayloadSendService.SendMessage(new ClientSessionClaimResponsePayload(ClientSessionClaimResponseCode.SessionUnavailable))
					.ConfigureAwait(false);

				return;
			}
			
			NetworkEntityGuidBuilder builder = new NetworkEntityGuidBuilder();

			builder
				.WithId(payload.CharacterId)
				.WithType(EntityType.Player);

			//TODO: We assume they are authenticated, we don't check at the moment but we WILL and SHOULD. Just load their location.
			ZoneServerCharacterLocationResponse locationResponse = await GameServerClient.GetCharacterLocation(payload.CharacterId)
				.ConfigureAwait(false);

			Vector3 position = locationResponse.isSuccessful ? locationResponse.Position : Vector3.zero;

			if(Logger.IsDebugEnabled)
				Logger.Debug($"Recieved player location: {position}");

			//Just broadcast successful claim, let listeners figure out what to do with this information.
			OnSuccessfulSessionClaimed?.Invoke(this, new PlayerSessionClaimedEventArgs(builder.Build(), position, new PlayerEntitySessionContext(context.PayloadSendService, context.Details.ConnectionId)));

			await context.PayloadSendService.SendMessage(new ClientSessionClaimResponsePayload(ClientSessionClaimResponseCode.Success))
				.ConfigureAwait(false);

			//TODO: We shouldn't hardcode this, we should send the correct scene specified by the gameserver this zone/instance connects to to service.
			await context.PayloadSendService.SendMessage(new LoadNewSceneEventPayload(PlayableGameScene.LobbyType1))
				.ConfigureAwait(false);
		}
	}
}
