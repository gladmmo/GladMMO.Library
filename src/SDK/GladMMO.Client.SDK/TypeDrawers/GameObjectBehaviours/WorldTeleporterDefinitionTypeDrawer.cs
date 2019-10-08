using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Glader.Essentials;
using UnityEditor;
using UnityEngine;

namespace GladMMO.SDK
{
	[CustomEditor(typeof(WorldTeleporterDefinitionData))]
	public sealed class WorldTeleporterDefinitionTypeDrawer : NetworkedDefinitionEditor
	{
		private List<string> CachedRemoteSpawnList = new List<string>(0);

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			using(new EditorGUI.DisabledGroupScope(isProgressBarActive))
			{
				//Depending on authentication state
				//we dispatch the rendering for GUI controls
				if(AuthenticationModelSingleton.Instance.isAuthenticated)
					AuthenticatedOnGUI();
				else
					UnAuthenticatedOnGUI();
			}
		}

		private void UnAuthenticatedOnGUI()
		{
			EditorGUILayout.LabelField($"You need to be authenticated.");
		}

		private void AuthenticatedOnGUI()
		{
			UnityAsyncHelper.InitializeSyncContext();

			GetTarget().LocalSpawnPointId = (PlayerStaticSpawnPointDefinition)EditorGUILayout.ObjectField("Local Spawn:", GetTarget().LocalSpawnPointId, typeof(PlayerStaticSpawnPointDefinition), true);

			//This isn't really used by the data model BUT it is useful for filtering down a list.
			GetTarget().TargetTeleportWorldId = EditorGUILayout.IntField("Target WorldId:", GetTarget().TargetTeleportWorldId);

			if (CachedRemoteSpawnList.Any())
			{
				int index = EditorGUILayout.Popup("Remote SpawnPoint", CachedRemoteSpawnList.FindIndex(s => Int32.Parse(s) == GetTarget().RemoteSpawnPointId), CachedRemoteSpawnList.ToArray());

				if(CachedRemoteSpawnList.Count > index)
				{
					GetTarget().RemoteSpawnPointId = Int32.Parse(CachedRemoteSpawnList[index]);
				}
			}
			else
			{
				int index = EditorGUILayout.Popup("Remote SpawnPoint", 0, Array.Empty<string>());
			}


			if (GUILayout.Button($"Refresh Spawnpoint List"))
			{
				if (GetTarget().TargetTeleportWorldId > 0)
				{
					IPlayerSpawnPointDataServiceClient client = new PlayerSpawnPointContentServiceClientFactory().Create(EmptyFactoryContext.Instance);

					UnityAsyncHelper.UnityMainThreadContext.Post(async o =>
					{
						try
						{
							DisplayProgressBar($"Querying Spawnpoints for World: {GetTarget().TargetTeleportWorldId}", "Querying", 0.5f);

							ResponseModel<ObjectEntryCollectionModel<PlayerSpawnPointInstanceModel>, ContentEntryCollectionResponseCode> spawnPoints = await client.GetSpawnPointEntriesByWorld(GetTarget().TargetTeleportWorldId);

							if (!spawnPoints.isSuccessful)
							{
								Debug.LogError($"Failed to query SpawnPoints: {spawnPoints.ResultCode}");
								return;
							}

							//TODO: Handle error.
							CachedRemoteSpawnList = spawnPoints.Result.Entries
								.Select(p => p.SpawnPointId.ToString())
								.ToList();

							//This will prevent some random errors.
							GetTarget().RemoteSpawnPointId = int.Parse(CachedRemoteSpawnList.First());
						}
						catch (Exception e)
						{
							Debug.LogError($"Error: {e.Message}");
							throw;
						}
						finally
						{
							ClearProgressBar();
						}

					}, null);
				}
			}

			if(GUILayout.Button($"Refresh Teleporter Data"))
			{
				IWorldTeleporterDataServiceClient client = new WorldTeleporterContentServiceClientFactory().Create(EmptyFactoryContext.Instance);

				RefresheData(client);
			}

			if (GUILayout.Button("Save Updates"))
			{
				IWorldTeleporterDataServiceClient client = new WorldTeleporterContentServiceClientFactory().Create(EmptyFactoryContext.Instance);

				UpdateData(client);
			}
		}

		private void UpdateData([NotNull] IWorldTeleporterDataServiceClient client)
		{
			if (client == null) throw new ArgumentNullException(nameof(client));

			DisplayProgressBar($"Saving WorldTeleporter Data", "Saving", 0.25f);

			UnityAsyncHelper.UnityMainThreadContext.Post(async o =>
			{
				try
				{
					int objectInstanceId = GetTarget().gameObject.GetComponent<GameObjectStaticSpawnPointDefinition>().GameObjectInstanceId;
					await client.UpdateGameObjectInstance(objectInstanceId, new WorldTeleporterInstanceModel(objectInstanceId, GetTarget().LocalSpawnPointId.PlayerSpawnPointId, GetTarget().RemoteSpawnPointId));

					await RefreshLocalDataAsync(client);
				}
				catch (Exception e)
				{
					//TODO: Log
					throw;
				}
				finally
				{
					ClearProgressBar();
				}

			}, null);
		}

		private void RefresheData([NotNull] IWorldTeleporterDataServiceClient client)
		{
			if (client == null) throw new ArgumentNullException(nameof(client));

			DisplayProgressBar($"Querying WorldTeleporter Data", "Querying", 0.5f);

			UnityAsyncHelper.UnityMainThreadContext.Post(async o => { await RefreshLocalDataAsync(client); }, null);
		}

		private async Task RefreshLocalDataAsync(IWorldTeleporterDataServiceClient client)
		{
			try
			{
				ResponseModel<WorldTeleporterInstanceModel, SceneContentQueryResponseCode> responseModel = await client.GetWorldTeleporterInstance(GetTarget().gameObject.GetComponent<GameObjectStaticSpawnPointDefinition>().GameObjectInstanceId);

				//TODO: Handle failure
				GetTarget().LocalSpawnPointId = GameObject
					.FindObjectsOfType<PlayerStaticSpawnPointDefinition>()
					.First(sp => sp.PlayerSpawnPointId == responseModel.Result.LocalSpawnPointId);

				GetTarget().RemoteSpawnPointId = responseModel.Result.RemoteSpawnPointId;
			}
			catch (Exception e)
			{
				//TODO: Log
				throw;
			}
			finally
			{
				ClearProgressBar();
			}
		}

		private WorldTeleporterDefinitionData GetTarget()
		{
			return (WorldTeleporterDefinitionData)target;
		}
	}
}
