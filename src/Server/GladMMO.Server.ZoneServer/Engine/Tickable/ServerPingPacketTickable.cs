using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using GladNet;

namespace GladMMO
{
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class ServerPingPacketTickable : IGameTickable
	{
		private int tickCount = 0;

		private IReadonlyEntityGuidMappable<IPeerPayloadSendService<GameServerPacketPayload>> SendServiceMappable { get; }

		private IReadonlyKnownEntitySet KnownEntities { get; }

		public ServerPingPacketTickable([NotNull] IReadonlyEntityGuidMappable<IPeerPayloadSendService<GameServerPacketPayload>> sendServiceMappable,
			[NotNull] IReadonlyKnownEntitySet knownEntities)
		{
			SendServiceMappable = sendServiceMappable ?? throw new ArgumentNullException(nameof(sendServiceMappable));
			KnownEntities = knownEntities ?? throw new ArgumentNullException(nameof(knownEntities));
		}

		public void Tick()
		{
			//Right now, each tick is 100ms. But this can change.
			tickCount++;

			//every 10 seconds.
			if (tickCount > 100)
			{
				ServerPingPacketPayload pingPacketPayload = new ServerPingPacketPayload();
				foreach (var sendService in SendServiceMappable.EnumerateWithGuid(KnownEntities, EntityType.Player))
					sendService.ComponentValue.SendMessage(pingPacketPayload);

				tickCount = 0;
			}
		}
	}
}
