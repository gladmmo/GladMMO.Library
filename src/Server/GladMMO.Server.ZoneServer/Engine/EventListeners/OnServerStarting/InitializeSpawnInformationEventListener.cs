using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using Glader.Essentials;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GladMMO
{
	//TODO: Eventually we need to do this after we download the server map data.
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class InitializeSpawnInformationEventListener : BaseSingleEventListenerInitializable<IServerStartingEventSubscribable>
	{
		private PlayerSpawnStrategyQueue SpawnStrategyQueue { get; }

		private ILog Logger { get; }

		public InitializeSpawnInformationEventListener(IServerStartingEventSubscribable subscriptionService,
			[NotNull] PlayerSpawnStrategyQueue spawnStrategyQueue,
			[NotNull] ILog logger) 
			: base(subscriptionService)
		{
			SpawnStrategyQueue = spawnStrategyQueue ?? throw new ArgumentNullException(nameof(spawnStrategyQueue));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void OnEventFired(object source, EventArgs args)
		{
			//This locates all spawnpoint strats in the scene
			foreach (var spawn in GameObject.FindObjectsOfType<MonoBehaviour>()
				.Where(b => b?.gameObject?.scene != null && !String.IsNullOrEmpty(b?.gameObject?.scene.name))
				.OfType<ISpawnPointStrategy>()
				.Where(b => b.EntityType == EntitySpawnType.Player))
			{
				SpawnStrategyQueue.Enqueue(spawn);

				if(Logger.IsDebugEnabled)
					Logger.Debug($"SpawnPoint found.");
			}

			//It's possible the creator didn't specify a spawnpoint, so we just use a default
			if (SpawnStrategyQueue.Count == 0)
			{
				if(Logger.IsWarnEnabled)
					Logger.Debug($"No spawnPoints found.");

				SpawnStrategyQueue.Enqueue(new DefaultSpawnPointStrategy());
			}
		}
	}
}
