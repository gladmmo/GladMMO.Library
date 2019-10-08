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
	public sealed class WorldTeleporterDefinitionTypeDrawer : BaseBehaviourDefinitionTypeDrawer<WorldTeleporterDefinitionData, WorldTeleporterInstanceModel>
	{
		private List<string> CachedRemoteSpawnList = new List<string>(0);

		public WorldTeleporterDefinitionTypeDrawer() 
			: base(new WorldTeleporterBehaviourToTransportModelTypeConverter(), new WorldTeleporterContentServiceClientFactory())
		{

		}

		protected override void GatherConfigurableData(WorldTeleporterDefinitionData target)
		{
			target.LocalSpawnPointId = (PlayerStaticSpawnPointDefinition)EditorGUILayout.ObjectField("Local Spawn:", GetTarget().LocalSpawnPointId, typeof(PlayerStaticSpawnPointDefinition), true);

			//This isn't really used by the data model BUT it is useful for filtering down a list.
			target.TargetTeleportWorldId = EditorGUILayout.IntField("Target WorldId:", GetTarget().TargetTeleportWorldId);

			if(CachedRemoteSpawnList.Any())
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

			if(GUILayout.Button($"Refresh Spawnpoint List"))
			{
				if(GetTarget().TargetTeleportWorldId > 0)
				{
					IPlayerSpawnPointDataServiceClient client = new PlayerSpawnPointContentServiceClientFactory().Create(EmptyFactoryContext.Instance);

					UnityAsyncHelper.UnityMainThreadContext.Post(async o =>
					{
						try
						{
							DisplayProgressBar($"Querying Spawnpoints for World: {GetTarget().TargetTeleportWorldId}", "Querying", 0.5f);

							ResponseModel<ObjectEntryCollectionModel<PlayerSpawnPointInstanceModel>, ContentEntryCollectionResponseCode> spawnPoints = await client.GetSpawnPointEntriesByWorld(GetTarget().TargetTeleportWorldId);

							if(!spawnPoints.isSuccessful)
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
						catch(Exception e)
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
		}
	}
}
