using System;
using System.Collections.Generic;
using System.Text;
using FreecraftCore;
using Glader.Essentials;
using GladNet;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class NetworkPingTickable : IGameTickable
	{
		private IManagedNetworkClient<GamePacketPayload, GamePacketPayload> Client { get; }

		private float DeltaTime = 500;

		private const float SEND_RATE = 30;

		public NetworkPingTickable([NotNull] IManagedNetworkClient<GamePacketPayload, GamePacketPayload> client)
		{
			Client = client ?? throw new ArgumentNullException(nameof(client));
		}

		public void Tick()
		{
			if (Client.isConnected)
			{
				if (DeltaTime > SEND_RATE)
				{
					DeltaTime = 0;
					Client.SendMessage(new FreecraftCore.PingRequest(50, 50));
				}
				else
					DeltaTime += Time.deltaTime;
			}
		}
	}
}
