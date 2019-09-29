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
	[CustomEditor(typeof(GameObjectStaticSpawnPointDefinition))]
	public sealed class GameObjectStaticSpawnPointDefinitionTypeDrawer : StaticSpawnPointDefinitionEditor
	{
		private string CachedGameObjectInfoText = null;

		private string CachedGameObjectTemplateInfoText = null;

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

			//There is no creature instance associated with this yet.
			if (GetTarget().GameObjectInstanceId == -1)
			{
				if (GUILayout.Button($"Create GameObject Instance"))
				{
					IGameObjectDataServiceClient client = new GameObjectContentServiceClientFactory().Create(EmptyFactoryContext.Instance);

					WorldDefinitionData worldData = FindObjectOfType<WorldDefinitionData>();

					if (worldData == null)
					{
						Debug.LogError($"Cannot create GameObject instance until the world is uploaded and the {nameof(WorldDefinitionData)} exists within the scene.");
						return;
					}

					CreateGameObjectInstance(client, worldData);
				}

				return;
			}
			else
			{
				EditorGUILayout.LabelField($"Instance Id: {GetTarget().GameObjectInstanceId}");

				GUILayout.Label(CachedGameObjectInfoText, GUI.skin.textArea);

				//The reason we do this manually is so that it can be hidden before there is an instance id.
				GetTarget().GameObjectTemplateId = EditorGUILayout.IntField($"Template Id", GetTarget().GameObjectTemplateId);

				//Now, if the creature template id is not -1 we should try to load the template
				if (GetTarget().GameObjectTemplateId > 0)
				{
					GUILayout.Label(CachedGameObjectTemplateInfoText, GUI.skin.textArea);
				}
				else
					GUILayout.Label($"Unknown GameObject Template: {GetTarget().GameObjectTemplateId}", GUI.skin.textArea);

			}

			if (GUILayout.Button($"Refresh GameObject Data"))
			{
				IGameObjectDataServiceClient client = new GameObjectContentServiceClientFactory().Create(EmptyFactoryContext.Instance);

				RefreshGameObjectData(client);
			}

			if(GUILayout.Button("Save Updates"))
			{
				UpdateGameObjectData();
			}
		}

		private void UpdateGameObjectData()
		{
			DisplayProgressBar("Updating GameObject", "Uploading Data (1/1)", 0.25f);

			UnityAsyncHelper.UnityMainThreadContext.Post(async o =>
			{
				try
				{
					IGameObjectDataServiceClient client = new GameObjectContentServiceClientFactory().Create(EmptyFactoryContext.Instance);

					//Just sent the updated model.
					await client.UpdateGameObjectInstance(GetTarget().GameObjectInstanceId, new GameObjectInstanceModel(BuildNetworkEntityGuid(), GetTarget().GameObjectTemplateId, GetTarget().transform.position, GetTarget().transform.eulerAngles.y));

					//Since the data about the creature displayed is probably now stale, we should update it after saving.
					await RefreshGameObjectData(client);
				}
				catch (Exception e)
				{
					Debug.LogError($"Failed to update GameObject. Reason: {e.Message}");
					throw;
				}
				finally
				{
					ClearProgressBar();
				}
			}, null);
		}

		private void CreateGameObjectInstance(IGameObjectDataServiceClient client, WorldDefinitionData worldData)
		{
			DisplayProgressBar("Creating GameObject", "Requesting instance (1/2)", 0.0f);

			UnityAsyncHelper.UnityMainThreadContext.Post(async o =>
			{
				try
				{
					//If they press this, we need to actually create a gameobject instance for this world id.
					var result = await client.CreateGameObjectInstance(worldData.ContentId);

					if (result.isSuccessful)
					{
						DisplayProgressBar("Creating GameObject", "Saving Instance (2/2)", 0.5f);

						GetTarget().GameObjectInstanceId = result.Result.Guid.EntryId;
						EditorUtility.SetDirty(GetTarget());
						EditorSceneManager.MarkSceneDirty(GetTarget().gameObject.scene);

						await RefreshGameObjectData(client);
					}
					else
						Debug.LogError($"Failed to create GameObject Instance. Reason: {result.ResultCode}");
				}
				catch (Exception e)
				{
					Debug.LogError($"Failed to create GameObject Instance. Reason: {e.Message}");
					throw;
				}
				finally
				{
					ClearProgressBar();
				}
			}, null);
		}

		private async Task RefreshGameObjectData([NotNull] IGameObjectDataServiceClient client)
		{
			if(client == null) throw new ArgumentNullException(nameof(client));

			try
			{
				DisplayProgressBar("Refreshing GameObject", "GameObject Instance (1/2).", 0.0f);

				GameObjectInstanceModel instanceData = await RefreshGameObjectInstanceData(client);

				//If the creature instance exists and we have a valid template assigned.
				if (instanceData != null && instanceData.TemplateId > 0)
				{
					DisplayProgressBar("Refreshing GameObject", "GameObject Template (2/2).", 0.0f);
					await RefreshGameObjectTemplateData(client);
				}
			}
			catch (Exception e)
			{
				Debug.LogError($"Failed to refresh GameObject. Reason: {e.Message}");
				throw;
			}
			finally
			{
				ClearProgressBar();
			}
		}

		private async Task RefreshGameObjectTemplateData(IGameObjectDataServiceClient client)
		{
			//TODO: This is just for testing, we should properly handle this
			ResponseModel<GameObjectTemplateModel, SceneContentQueryResponseCode> templateModelResponse = await client.GetGameObjectTemplate(GetTarget().GameObjectTemplateId);
			var result = templateModelResponse.Result;

			CachedGameObjectTemplateInfoText = $"GameObject Template: {GetTarget().GameObjectTemplateId}\nName: {result.GameObjectName}\nModel Id: {result.ModelId}\n Type: {result.ObjectType.ToString()}";
		}

		private async Task<GameObjectInstanceModel> RefreshGameObjectInstanceData([NotNull] IGameObjectDataServiceClient client)
		{
			if (client == null) throw new ArgumentNullException(nameof(client));

			ResponseModel<GameObjectInstanceModel, SceneContentQueryResponseCode> queryResponseModel = await client.GetGameObjectInstance(GetTarget().GameObjectInstanceId);

			//TODO: No idea what should be done here.
			if (!queryResponseModel.isSuccessful)
				return null;

			CachedGameObjectInfoText = $"GameObject Instance: {GetTarget().GameObjectInstanceId}\nGuid: {queryResponseModel.Result.Guid}\nSpawnPosition: {queryResponseModel.Result.InitialPosition}\nYRotation: {queryResponseModel.Result.YAxisRotation}";
			GetTarget().GameObjectTemplateId = queryResponseModel.Result.TemplateId;

			return queryResponseModel.Result;
		}

		private NetworkEntityGuid BuildNetworkEntityGuid()
		{
			return new NetworkEntityGuidBuilder()
				.WithId(0) //0 means it's not an instance.
				.WithType(EntityType.GameObject)
				.WithEntryId(GetTarget().GameObjectInstanceId)
				.Build();
		}

		private GameObjectStaticSpawnPointDefinition GetTarget()
		{
			return (GameObjectStaticSpawnPointDefinition)target;
		}
	}
}
