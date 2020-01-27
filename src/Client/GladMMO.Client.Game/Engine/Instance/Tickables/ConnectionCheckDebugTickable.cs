using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Glader.Essentials;
using GladNet;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public class ConnectionCheckDebugTickable : BaseSingleEventListenerInitializable<INetworkClientDisconnectedEventSubscribable>, IGameTickable
	{
		private IManagedNetworkClient<GameClientPacketPayload, GameServerPacketPayload> Client { get; }

		private INetworkClientManager ClientManager { get; }

		private ILog Logger { get; }

		public ConnectionCheckDebugTickable(INetworkClientDisconnectedEventSubscribable subService, 
			[NotNull] IManagedNetworkClient<GameClientPacketPayload, GameServerPacketPayload> client, 
			[NotNull] ILog logger,
			[NotNull] INetworkClientManager clientManager)
			: base(subService)
		{
			Client = client ?? throw new ArgumentNullException(nameof(client));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			ClientManager = clientManager ?? throw new ArgumentNullException(nameof(clientManager));
		}

		public void Tick()
		{
			//This is so fucking dumb but it's the only way to currently signal to the network thread that the
			//remote service disconnected us and that we should stop, which will then fire
			//the disconnection event below.
			if (!Client.isConnected && ClientManager.isNetworkHandling)
				ClientManager.StopHandlingNetworkClient();
		}

		protected override void OnEventFired(object source, EventArgs args)
		{
			Logger.Warn($"Disconnection Event Indicated that Client isConnected: {Client.isConnected}");
		}
	}
}
