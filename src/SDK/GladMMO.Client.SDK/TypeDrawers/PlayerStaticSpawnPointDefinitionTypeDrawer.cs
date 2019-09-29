using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Glader.Essentials;
using Refit;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace GladMMO.SDK
{
	//TODO: Refactor to combine Creature and GameObject instance spawning.
	[CustomEditor(typeof(PlayerStaticSpawnPointDefinition))]
	public sealed class PlayerStaticSpawnPointDefinitionTypeDrawer : NetworkedDefinitionEditor
	{
		private string CachedInfoText = null;

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			using (new EditorGUI.DisabledGroupScope(isProgressBarActive))
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

			//There is no instance associated with this yet.
			if (GetTarget().PlayerSpawnPointId == -1)
			{
				if (GUILayout.Button($"Create SpawnPoint Instance"))
				{
					IPlayerSpawnPointDataServiceClient client = new PlayerSpawnPointContentServiceClientFactory().Create(EmptyFactoryContext.Instance);

					WorldDefinitionData worldData = FindObjectOfType<WorldDefinitionData>();

					if (worldData == null)
					{
						Debug.LogError($"Cannot create creature instance until the world is uploaded and the {nameof(WorldDefinitionData)} exists within the scene.");
						return;
					}

					CreateInstance(client, worldData);
				}

				return;
			}
			else
			{
				GUILayout.Label(CachedInfoText, GUI.skin.textArea);

				//The reason we do this manually is so that it can be hidden before there is an instance id.
				GetTarget().isInstanceReserved = EditorGUILayout.Toggle($"IsReserved", GetTarget().isInstanceReserved);
			}

			if (GUILayout.Button($"Refresh PlayerSpawnPoint Data"))
			{
				IPlayerSpawnPointDataServiceClient client = new PlayerSpawnPointContentServiceClientFactory().Create(EmptyFactoryContext.Instance);

				RefreshData(client);
			}

			if(GUILayout.Button("Save Updates"))
			{
				UpdateData();
			}
		}

		private void UpdateData()
		{
			DisplayProgressBar("Updating PlayerSpawnPoint", "Uploading Data (1/1)", 0.25f);

			UnityAsyncHelper.UnityMainThreadContext.Post(async o =>
			{
				try
				{
					IPlayerSpawnPointDataServiceClient client = new PlayerSpawnPointContentServiceClientFactory().Create(EmptyFactoryContext.Instance);

					//Just sent the updated model.
					await client.UpdateSpawnPointInstance(GetTarget().PlayerSpawnPointId, new PlayerSpawnPointInstanceModel(GetTarget().PlayerSpawnPointId, GetTarget().transform.position, GetTarget().transform.eulerAngles.y, GetTarget().isInstanceReserved));

					//Since the data about the creature displayed is probably now stale, we should update it after saving.
					await RefreshData(client);
				}
				catch (Exception e)
				{
					Debug.LogError($"Failed to update PlayerSpawnPoint. Reason: {e.Message}");
					throw;
				}
				finally
				{
					ClearProgressBar();
				}
			}, null);
		}

		private void CreateInstance(IPlayerSpawnPointDataServiceClient client, WorldDefinitionData worldData)
		{
			DisplayProgressBar("Creating PlayerSpawnPoint", "Requesting instance (1/2)", 0.0f);

			UnityAsyncHelper.UnityMainThreadContext.Post(async o =>
			{
				try
				{
					//If they press this, we need to actually create a creature instance for this world id.
					var result = await client.CreateSpawnPointInstance(worldData.ContentId);

					if (result.isSuccessful)
					{
						DisplayProgressBar("Creating PlayerSpawnPoint", "Saving Instance (2/2)", 0.5f);

						GetTarget().PlayerSpawnPointId = result.Result.SpawnPointId;
						EditorUtility.SetDirty(GetTarget());
						EditorSceneManager.MarkSceneDirty(GetTarget().gameObject.scene);

						await RefreshData(client);
					}
					else
						Debug.LogError($"Failed to create PlayerSpawnPoint Instance. Reason: {result.ResultCode}");
				}
				catch (Exception e)
				{
					Debug.LogError($"Failed to create PlayerSpawnPoint Instance. Reason: {e.Message}");
					throw;
				}
				finally
				{
					ClearProgressBar();
				}
			}, null);
		}

		private async Task RefreshData([NotNull] IPlayerSpawnPointDataServiceClient client)
		{
			if(client == null) throw new ArgumentNullException(nameof(client));

			try
			{
				DisplayProgressBar("Refreshing PlayerSpawnPoint", "Spawn Instance (1/1).", 0.5f);

				PlayerSpawnPointInstanceModel instanceData = await RefreshInstanceData(client);
				GetTarget().PlayerSpawnPointId = instanceData.SpawnPointId;
				GetTarget().isInstanceReserved = instanceData.isReserved;

				CachedInfoText = $"PlayerSpawn Instance: {GetTarget().PlayerSpawnPointId}\nSpawnPosition: {instanceData.InitialPosition}\nYRotation: {instanceData.YAxisRotation}\nIsReserved: {instanceData.isReserved}";
			}
			catch (Exception e)
			{
				Debug.LogError($"Failed to refresh PlayerSpawnPoint. Reason: {e.Message}");
				throw;
			}
			finally
			{
				ClearProgressBar();
			}
		}

		private async Task<PlayerSpawnPointInstanceModel> RefreshInstanceData([NotNull] IPlayerSpawnPointDataServiceClient client)
		{
			if (client == null) throw new ArgumentNullException(nameof(client));

			ResponseModel<PlayerSpawnPointInstanceModel, SceneContentQueryResponseCode> queryResponseModel = await client.GetSpawnPointInstance(GetTarget().PlayerSpawnPointId);

			//TODO: No idea what should be done here.
			if (!queryResponseModel.isSuccessful)
				return null;

			return queryResponseModel.Result;
		}

		private PlayerStaticSpawnPointDefinition GetTarget()
		{
			return (PlayerStaticSpawnPointDefinition)target;
		}
	}
}
