using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using FreecraftCore;
using Glader.Essentials;
using GladNet;
using Nito.AsyncEx;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class SetupClientDisconnectOnApplicationQuitInitializable : IGameInitializable, IDisposable
	{
		private INetworkClientManager ClientManager { get; }

		private IConnectionService ConnectionService { get; }

		private ILog Logger { get; }

		public SetupClientDisconnectOnApplicationQuitInitializable([NotNull] ILog logger,
			[NotNull] INetworkClientManager clientManager,
			[NotNull] IConnectionService connectionService)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			ClientManager = clientManager ?? throw new ArgumentNullException(nameof(clientManager));
			ConnectionService = connectionService ?? throw new ArgumentNullException(nameof(connectionService));
		}

		public Task OnGameInitialized()
		{
			Application.quitting += OnApplicationQuit;
			return Task.CompletedTask;
		}

		private void OnApplicationQuit()
		{
			if(Logger.IsInfoEnabled)
				Logger.Info("ApplicationQuit network disconnection called.");

			ConnectionService.Disconnect();
			ClientManager.StopHandlingNetworkClient(true).ConfigureAwait(false).GetAwaiter().GetResult();
		}

		public void Dispose()
		{
			Application.quitting -= OnApplicationQuit;
		}
	}
}
