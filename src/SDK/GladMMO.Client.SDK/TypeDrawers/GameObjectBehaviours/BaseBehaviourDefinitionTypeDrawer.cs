using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Glader.Essentials;
using UnityEditor;
using UnityEngine;

namespace GladMMO.SDK
{
	public abstract class BaseBehaviourDefinitionTypeDrawer<TBehaviourType, TBehaviourTransportType> : NetworkedDefinitionEditor
		where TBehaviourType : UnityEngine.Component, IRemoteModelUpdateable<TBehaviourTransportType>
		where TBehaviourTransportType : class
	{
		private ITypeConverterProvider<TBehaviourType, TBehaviourTransportType> BehaviourToTransportConverter { get; }

		private IGameObjectBehaviourDataClientFactory<TBehaviourTransportType> BehaviourDataClientFactory { get; }

		protected BaseBehaviourDefinitionTypeDrawer([NotNull] ITypeConverterProvider<TBehaviourType, TBehaviourTransportType> behaviourToTransportConverter,
			[NotNull] IGameObjectBehaviourDataClientFactory<TBehaviourTransportType> behaviourDataClientFactory)
		{
			BehaviourToTransportConverter = behaviourToTransportConverter ?? throw new ArgumentNullException(nameof(behaviourToTransportConverter));
			BehaviourDataClientFactory = behaviourDataClientFactory ?? throw new ArgumentNullException(nameof(behaviourDataClientFactory));
		}

		public sealed override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			using (new EditorGUI.DisabledGroupScope(isProgressBarActive))
			{
				//Depending on authentication state
				//we dispatch the rendering for GUI controls
				if (AuthenticationModelSingleton.Instance.isAuthenticated)
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

			GatherConfigurableData(GetTarget());

			if(GUILayout.Button($"Refresh Behaviour Data"))
			{
				IGameObjectBehaviourDataServiceClient<TBehaviourTransportType> client = BehaviourDataClientFactory.Create(EmptyFactoryContext.Instance);

				RefresheData(client);
			}

			if(GUILayout.Button("Save Updates"))
			{
				IGameObjectBehaviourDataServiceClient<TBehaviourTransportType> client = BehaviourDataClientFactory.Create(EmptyFactoryContext.Instance);

				UpdateData(client);
			}
		}

		private void UpdateData([NotNull] IGameObjectBehaviourDataServiceClient<TBehaviourTransportType> client)
		{
			if(client == null) throw new ArgumentNullException(nameof(client));

			DisplayProgressBar($"Saving {nameof(TBehaviourType)} Data", "Saving", 0.25f);

			UnityAsyncHelper.UnityMainThreadContext.Post(async o =>
			{
				try
				{
					int objectInstanceId = GetTarget().gameObject.GetComponent<GameObjectStaticSpawnPointDefinition>().GameObjectInstanceId;
					await client.UpdateBehaviourInstance(objectInstanceId, BehaviourToTransportConverter.Convert(GetTarget()));

					await RefreshLocalDataAsync(client);
				}
				catch(Exception e)
				{
					Debug.LogError($"Failed to update remote data. Reason: {e.Message}");
					throw;
				}
				finally
				{
					ClearProgressBar();
				}

			}, null);
		}

		private void RefresheData([NotNull] IGameObjectBehaviourDataServiceClient<TBehaviourTransportType> client)
		{
			if(client == null) throw new ArgumentNullException(nameof(client));

			DisplayProgressBar($"Querying {typeof(TBehaviourType).Name} Data", "Querying", 0.5f);

			UnityAsyncHelper.UnityMainThreadContext.Post(async o => { await RefreshLocalDataAsync(client); }, null);
		}

		private async Task RefreshLocalDataAsync(IGameObjectBehaviourDataServiceClient<TBehaviourTransportType> client)
		{
			try
			{
				var responseModel = await client.GetBehaviourInstance(GetTarget().gameObject.GetComponent<GameObjectStaticSpawnPointDefinition>().GameObjectInstanceId);

				//TODO: Handle failure
				GetTarget().UpdateModel(responseModel.Result);
			}
			catch(Exception e)
			{
				Debug.LogError($"Failed to refresh local data. Reason: {e.Message}");
				throw;
			}
			finally
			{
				ClearProgressBar();
			}
		}

		/// <summary>
		/// Gather configurable data from the editor
		/// and update the provided model <see cref="target"/>.
		/// </summary>
		/// <param name="target">The model to update.</param>
		protected abstract void GatherConfigurableData(TBehaviourType target);

		protected TBehaviourType GetTarget()
		{
			return (TBehaviourType)target;
		}
	}

	/*[CustomEditor(typeof(AvatarPedestalDefinitionData))]
	public sealed class AvatarPedestalDefinitionTypeDrawer : NetworkedDefinitionEditor
	{
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

			GetTarget().TargetAvatarModelId = EditorGUILayout.IntField("Avatar Model Id:", GetTarget().TargetAvatarModelId);

			if(GUILayout.Button($"Refresh Pedestal Data"))
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

		private AvatarPedestalDefinitionData GetTarget()
		{
			return (AvatarPedestalDefinitionData)target;
		}
	}*/
}
