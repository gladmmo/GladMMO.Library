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

		private ISpawnPointStrategy SpawnPointProvider { get; }

		private ICharacterService CharacterService { get; }

		private IEntityGuidMappable<CharacterDataInstance> InitialCharacterDataMappable { get; }

		/// <inheritdoc />
		public ClientSessionClaimRequestHandler(
			[NotNull] IZoneServerToGameServerClient gameServerClient,
			[NotNull] ILog logger,
			[NotNull] ISpawnPointStrategy spawnPointProvider,
			[NotNull] IEntityGuidMappable<CharacterDataInstance> initialCharacterDataMappable,
			[NotNull] ICharacterService characterService)
			: base(logger)
		{
			GameServerClient = gameServerClient ?? throw new ArgumentNullException(nameof(gameServerClient));
			SpawnPointProvider = spawnPointProvider ?? throw new ArgumentNullException(nameof(spawnPointProvider));
			InitialCharacterDataMappable = initialCharacterDataMappable ?? throw new ArgumentNullException(nameof(initialCharacterDataMappable));
			CharacterService = characterService ?? throw new ArgumentNullException(nameof(characterService));
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
					.ConfigureAwaitFalse();
			}
			catch(Exception e) //we could get an unauthorized response
			{
				Logger.Error($"Failed to Query for AccountId: {e.Message}. AuthToken provided was: {payload.JWT}");
				throw;
			}

			if(!zoneServerTryClaimSessionResponse.isSuccessful)
			{
				if(Logger.IsWarnEnabled)
					Logger.Warn($"Client attempted to claim session for Character: {payload.CharacterId} but was denied.");

				//TODO: Better error code
				await context.PayloadSendService.SendMessage(new ClientSessionClaimResponsePayload(ClientSessionClaimResponseCode.SessionUnavailable))
					.ConfigureAwaitFalse();

				return;
			}

			NetworkEntityGuid entityGuid = new NetworkEntityGuidBuilder()
				.WithId(payload.CharacterId)
				.WithType(EntityType.Player)
				.Build();

			//TODO: We assume they are authenticated, we don't check at the moment but we WILL and SHOULD. Just load their location.
			ZoneServerCharacterLocationResponse locationResponse = await GameServerClient.GetCharacterLocation(payload.CharacterId)
				.ConfigureAwaitFalse();

			Vector3 position = locationResponse.isSuccessful ? locationResponse.Position : SpawnPointProvider.GetSpawnPoint().WorldPosition;

			SpawnPointData pointData = new SpawnPointData(position, Quaternion.identity);

			if(Logger.IsDebugEnabled)
				Logger.Debug($"Recieved player location: {pointData.WorldPosition} from {(locationResponse.isSuccessful ? "Database" : "Spawnpoint")}");

			//TODO: We need a cleaner/better way to load initial player data.
			ResponseModel<CharacterDataInstance, CharacterDataQueryReponseCode> characterData = await CharacterService.GetCharacterData(payload.CharacterId);

			//TODO: Check success.
			InitialCharacterDataMappable.AddObject(entityGuid, characterData.Result);

			//Just broadcast successful claim, let listeners figure out what to do with this information.
			OnSuccessfulSessionClaimed?.Invoke(this, new PlayerSessionClaimedEventArgs(entityGuid, pointData.WorldPosition, new PlayerEntitySessionContext(context.PayloadSendService, context.Details.ConnectionId, context.ConnectionService)));

			await context.PayloadSendService.SendMessage(new ClientSessionClaimResponsePayload(ClientSessionClaimResponseCode.Success))
				.ConfigureAwaitFalse();
		}
	}
}
