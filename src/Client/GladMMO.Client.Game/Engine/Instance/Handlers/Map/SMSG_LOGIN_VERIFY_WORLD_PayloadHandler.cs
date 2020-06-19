using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using FreecraftCore;
using GladNet;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class SMSG_LOGIN_VERIFY_WORLD_PayloadHandler : BaseGameClientGameMessageHandler<SMSG_LOGIN_VERIFY_WORLD_PAYLOAD>
	{
		//Not readonly, we need to modify it MAYBE.
		private IZoneDataRepository ZoneDataRepository { get; }

		private IMapTransferService MapTransferService { get; }

		private IReadonlyKnownEntitySet KnownEntities { get; }

		public SMSG_LOGIN_VERIFY_WORLD_PayloadHandler(ILog logger,
			[NotNull] IZoneDataRepository zoneDataRepository,
			[NotNull] IMapTransferService mapTransferService,
			[NotNull] IReadonlyKnownEntitySet knownEntities) 
			: base(logger)
		{
			ZoneDataRepository = zoneDataRepository ?? throw new ArgumentNullException(nameof(zoneDataRepository));
			MapTransferService = mapTransferService ?? throw new ArgumentNullException(nameof(mapTransferService));
			KnownEntities = knownEntities ?? throw new ArgumentNullException(nameof(knownEntities));
		}

		//Note: Unfortunately we have some troubles here. TC may send UPDATE packet before we even recieve this
		//so we must LEAVE the current functionality that causes us to disconnect completely.
		public override async Task HandleMessage(IPeerMessageContext<GamePacketPayload> context, SMSG_LOGIN_VERIFY_WORLD_PAYLOAD payload)
		{
			//Nothing has to happen if we're in the right map.
			if (ZoneDataRepository.ZoneId == payload.MapId)
				return;

			if (Logger.IsWarnEnabled)
				Logger.Warn($"Loaded wrong Map according to {nameof(SMSG_LOGIN_VERIFY_WORLD_PAYLOAD)}. This is normal if an instance has expired. KnownEntity Check: {KnownEntities.Count}");

			//TODO: This is a hack, can we ever improve it?
			//Disconnect, we're in the WRONG MAP. Current issues with staying connected here because TC will send a packet to start spawning
			//objects that will end up NOT in the other scene that we load, which will be the correct map.
			await context.ConnectionService.DisconnectAsync(0);

			//TC will send down this packet when we log into the world, it's possible we ASSUMED the wrong map
			//and loaded into the wrong map. The MapId in THIS packet is the actual correct one.
			//And we should load it if our expectations are wrong.
			await MapTransferService.TransferToMapAsync(payload.MapId);
			ZoneDataRepository.UpdateZoneId(payload.MapId);

			if(Logger.IsWarnEnabled)
				Logger.Warn($"KnownEntity Check2: {KnownEntities.Count}");
		}
	}
}
