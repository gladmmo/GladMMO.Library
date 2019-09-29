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
	[CustomEditor(typeof(CreatureStaticSpawnPointDefinition))]
	public sealed class CreatureStaticSpawnPointDefinitionTypeDrawer : NetworkedDefinitionEditor
	{
		private string CachedCreatureInfoText = null;

		private string CachedCreatureTemplateInfoText = null;

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
			if (GetTarget().CreatureInstanceId == -1)
			{
				if (GUILayout.Button($"Create Creature Instance"))
				{
					ICreatureDataServiceClient client = new CreatureContentServiceClientFactory().Create(EmptyFactoryContext.Instance);

					WorldDefinitionData worldData = FindObjectOfType<WorldDefinitionData>();

					if (worldData == null)
					{
						Debug.LogError($"Cannot create creature instance until the world is uploaded and the {nameof(WorldDefinitionData)} exists within the scene.");
						return;
					}

					CreateCreatureInstance(client, worldData);
				}

				return;
			}
			else
			{
				EditorGUILayout.LabelField($"Instance Id: {GetTarget().CreatureInstanceId}");

				GUILayout.Label(CachedCreatureInfoText, GUI.skin.textArea);

				//The reason we do this manually is so that it can be hidden before there is an instance id.
				GetTarget().CreatureTemplateId = EditorGUILayout.IntField($"Template Id", GetTarget().CreatureTemplateId);

				//Now, if the creature template id is not -1 we should try to load the template
				if (GetTarget().CreatureTemplateId > 0)
				{
					GUILayout.Label(CachedCreatureTemplateInfoText, GUI.skin.textArea);
				}
				else
					GUILayout.Label($"Unknown Creature Template: {GetTarget().CreatureTemplateId}", GUI.skin.textArea);

			}

			if (GUILayout.Button($"Refresh Creature Data"))
			{
				ICreatureDataServiceClient client = new CreatureContentServiceClientFactory().Create(EmptyFactoryContext.Instance);

				RefreshCreatureData(client);
			}

			if(GUILayout.Button("Save Updates"))
			{
				UpdateCreatureData();
			}
		}

		private void UpdateCreatureData()
		{
			DisplayProgressBar("Updating Creature", "Uploading Data (1/1)", 0.25f);

			UnityAsyncHelper.UnityMainThreadContext.Post(async o =>
			{
				try
				{
					ICreatureDataServiceClient client = new CreatureContentServiceClientFactory().Create(EmptyFactoryContext.Instance);

					//Just sent the updated model.
					await client.UpdateCreatureInstance(GetTarget().CreatureInstanceId, new CreatureInstanceModel(BuildNetworkEntityGuid(), GetTarget().CreatureTemplateId, GetTarget().transform.position, GetTarget().transform.eulerAngles.y));

					//Since the data about the creature displayed is probably now stale, we should update it after saving.
					await RefreshCreatureData(client);
				}
				catch (Exception e)
				{
					Debug.LogError($"Failed to update creature. Reason: {e.Message}");
					throw;
				}
				finally
				{
					ClearProgressBar();
				}
			}, null);
		}

		private void CreateCreatureInstance(ICreatureDataServiceClient client, WorldDefinitionData worldData)
		{
			DisplayProgressBar("Creating Creature", "Requesting instance (1/2)", 0.0f);

			UnityAsyncHelper.UnityMainThreadContext.Post(async o =>
			{
				try
				{
					//If they press this, we need to actually create a creature instance for this world id.
					var result = await client.CreateCreatureInstance(worldData.ContentId);

					if (result.isSuccessful)
					{
						DisplayProgressBar("Creating Creature", "Saving Instance (2/2)", 0.5f);

						GetTarget().CreatureInstanceId = result.Result.Guid.EntryId;
						EditorUtility.SetDirty(GetTarget());
						EditorSceneManager.MarkSceneDirty(GetTarget().gameObject.scene);

						await RefreshCreatureData(client);
					}
					else
						Debug.LogError($"Failed to create Creature Instance. Reason: {result.ResultCode}");
				}
				catch (Exception e)
				{
					Debug.LogError($"Failed to create Creature Instance. Reason: {e.Message}");
					throw;
				}
				finally
				{
					ClearProgressBar();
				}
			}, null);
		}

		private async Task RefreshCreatureData([NotNull] ICreatureDataServiceClient client)
		{
			if(client == null) throw new ArgumentNullException(nameof(client));

			try
			{
				DisplayProgressBar("Refreshing Creature", "Creature Instance (1/2).", 0.0f);

				CreatureInstanceModel instanceData = await RefreshCreatureInstanceData(client);

				//If the creature instance exists and we have a valid template assigned.
				if (instanceData != null && instanceData.TemplateId > 0)
				{
					DisplayProgressBar("Refreshing Creature", "Creature Template (2/2).", 0.0f);
					await RefreshCreatureTemplateData(client);
				}
			}
			catch (Exception e)
			{
				Debug.LogError($"Failed to refresh Creature. Reason: {e.Message}");
				throw;
			}
			finally
			{
				ClearProgressBar();
			}
		}

		private async Task RefreshCreatureTemplateData(ICreatureDataServiceClient client)
		{
			//TODO: This is just for testing, we should properly handle this
			ResponseModel<CreatureTemplateModel, SceneContentQueryResponseCode> templateModelResponse = await client.GetCreatureTemplate(GetTarget().CreatureTemplateId);
			var result = templateModelResponse.Result;

			CachedCreatureTemplateInfoText = $"Creature Template: {GetTarget().CreatureTemplateId}\nName: {result.CreatureName}\nModel Id: {result.ModelId}\nLevel Range: {result.MinimumLevel}-{result.MaximumLevel}";
		}

		private async Task<CreatureInstanceModel> RefreshCreatureInstanceData([NotNull] ICreatureDataServiceClient client)
		{
			if (client == null) throw new ArgumentNullException(nameof(client));

			ResponseModel<CreatureInstanceModel, SceneContentQueryResponseCode> queryResponseModel = await client.GetCreatureInstance(GetTarget().CreatureInstanceId);

			//TODO: No idea what should be done here.
			if (!queryResponseModel.isSuccessful)
				return null;

			CachedCreatureInfoText = $"Creature Instance: {GetTarget().CreatureInstanceId}\nGuid: {queryResponseModel.Result.Guid}\nSpawnPosition: {queryResponseModel.Result.InitialPosition}\nYRotation: {queryResponseModel.Result.YAxisRotation}";
			GetTarget().CreatureTemplateId = queryResponseModel.Result.TemplateId;

			return queryResponseModel.Result;
		}

		private NetworkEntityGuid BuildNetworkEntityGuid()
		{
			return new NetworkEntityGuidBuilder()
				.WithId(0) //0 means it's not an instance.
				.WithType(EntityType.Creature)
				.WithEntryId(GetTarget().CreatureInstanceId)
				.Build();
		}

		private CreatureStaticSpawnPointDefinition GetTarget()
		{
			return (CreatureStaticSpawnPointDefinition)target;
		}
	}
}
