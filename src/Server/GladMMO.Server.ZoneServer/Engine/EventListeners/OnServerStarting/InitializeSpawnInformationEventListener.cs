using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using Glader.Essentials;
using Nito.AsyncEx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GladMMO
{
	//TODO: Eventually we need to do this after we download the server map data.
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class InitializeSpawnInformationEventListener : BaseSingleEventListenerInitializable<IServerStartingEventSubscribable>
	{
		private PlayerSpawnPointQueue SpawnStrategyQueue { get; }

		private ILog Logger { get; }

		private IPlayerSpawnPointDataServiceClient PlayerSpawnContentDataClient { get; }

		private WorldConfiguration WorldConfiguration { get; }

		public InitializeSpawnInformationEventListener(IServerStartingEventSubscribable subscriptionService,
			[NotNull] PlayerSpawnPointQueue spawnStrategyQueue,
			[NotNull] ILog logger,
			[NotNull] IPlayerSpawnPointDataServiceClient playerSpawnContentDataClient,
			[NotNull] WorldConfiguration worldConfiguration) 
			: base(subscriptionService)
		{
			SpawnStrategyQueue = spawnStrategyQueue ?? throw new ArgumentNullException(nameof(spawnStrategyQueue));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			PlayerSpawnContentDataClient = playerSpawnContentDataClient ?? throw new ArgumentNullException(nameof(playerSpawnContentDataClient));
			WorldConfiguration = worldConfiguration ?? throw new ArgumentNullException(nameof(worldConfiguration));
		}

		protected override void OnEventFired(object source, EventArgs args)
		{
			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				try
				{
					ResponseModel<ObjectEntryCollectionModel<PlayerSpawnPointInstanceModel>, ContentEntryCollectionResponseCode> responseModel = await PlayerSpawnContentDataClient.GetSpawnPointEntriesByWorld(WorldConfiguration.WorldId);

					//TODO: Handle failure.
					foreach(var spawnPoint in responseModel.Result.Entries)
						if(!spawnPoint.isReserved)
						{
							if(Logger.IsInfoEnabled)
								Logger.Info($"Found Player Spawn: {spawnPoint.SpawnPointId}");

							SpawnStrategyQueue.Enqueue(spawnPoint);
						}


					//It's possible the creator didn't specify a spawnpoint, so we just use a default
					if(SpawnStrategyQueue.Count == 0)
					{
						if(Logger.IsWarnEnabled)
							Logger.Debug($"No spawnPoints found.");

						SpawnStrategyQueue.Enqueue(new PlayerSpawnPointInstanceModel(1, Vector3.zero, 0, false));
					}
				}
				catch (Exception e)
				{
					if(Logger.IsErrorEnabled)
						Logger.Error($"Failed to fully finish query for PlayerSpawnPoint data. Reason: {e.Message}\nStack: {e.StackTrace}");
				}
			});
		}
	}
}
