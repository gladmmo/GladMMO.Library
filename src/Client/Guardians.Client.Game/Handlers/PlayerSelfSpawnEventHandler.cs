﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;
using UnityEngine;

namespace Guardians
{
	/// <summary>
	/// The handler for the player spawn event
	/// the server sends once it enter's the client into the world.
	/// </summary>
	public sealed class PlayerSelfSpawnEventHandler : BaseZoneClientGameMessageHandler<PlayerSelfSpawnEventPayload>
	{
		private IFactoryCreatable<GameObject, DefaultEntityCreationContext> PlayerFactory { get; }

		private IReadOnlyCollection<IGameInitializable> Initializables { get; }

		/// <inheritdoc />
		public PlayerSelfSpawnEventHandler(
			ILog logger, 
			IFactoryCreatable<GameObject, DefaultEntityCreationContext> playerFactory,
			IReadOnlyCollection<IGameInitializable> initializables)
			: base(logger)
		{
			PlayerFactory = playerFactory ?? throw new ArgumentNullException(nameof(playerFactory));
			Initializables = initializables ?? throw new ArgumentNullException(nameof(initializables));
		}

		/// <inheritdoc />
		public override async Task HandleMessage(IPeerMessageContext<GameClientPacketPayload> context, PlayerSelfSpawnEventPayload payload)
		{
			//TODO: Actually handle this. Right now it's just demo code, it actually could fail.
			if(Logger.IsInfoEnabled)
				Logger.Info($"Recieved server commanded PlayerSpawn. Player GUID: {payload.CreationData.EntityGuid} Position: {payload.CreationData.InitialMovementData.InitialPosition}");

			await new UnityYieldAwaitable();

			//Don't do any checks for now, we just spawn
			PlayerFactory.Create(new DefaultEntityCreationContext(payload.CreationData.EntityGuid, payload.CreationData.InitialMovementData, EntityPrefab.LocalPlayer));


			//Call all OnGameInitializables
			foreach(var init in Initializables)
				await init.OnGameInitialized()
					.ConfigureAwait(false);

			//TODO: We need to make this the first packet, or couple of packets. We don't want to do this inbetween potentially slow operatons.
			await context.PayloadSendService.SendMessageImmediately(new ServerTimeSyncronizationRequestPayload(DateTime.UtcNow.Ticks))
				.ConfigureAwait(false);
		}
	}
}
