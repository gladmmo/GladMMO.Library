using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace GladMMO.SDK
{
	public abstract class BasePrefabedCustomContentUploadEditorWindow<TModelDataDefinitionType> : BaseCustomContentUploadEditorWindow
		where TModelDataDefinitionType : UnityEngine.Component, IUploadedContentDataDefinition
	{
		[Tooltip("The Prefab containing the model.")]
		[SerializeField]
		private UnityEngine.GameObject ModelPrefab;

		protected BasePrefabedCustomContentUploadEditorWindow(UserContentType contentType) 
			: base(contentType)
		{

		}

		protected override void AuthenticatedOnGUI()
		{
			ModelPrefab = EditorGUILayout.ObjectField("Model Prefab", ModelPrefab, typeof(GameObject), false) as GameObject;

			if(ModelPrefab != null)
			{
				if(PrefabUtility.GetPrefabAssetType(ModelPrefab) == PrefabAssetType.NotAPrefab)
				{
					ModelPrefab = null;
					Debug.LogError($"Provided model MUST be a prefab.");
					return;
				}
			}
			else
				return; //TODO: Try to discover the avatar's prefab in the scene.

			//We need the avatar data definition now.
			TModelDataDefinitionType definitionData = ModelPrefab.GetComponent<TModelDataDefinitionType>();

			if(definitionData == null)
				Debug.LogError($"Provided creature must contain Component: {typeof(TModelDataDefinitionType).Name} on root {nameof(GameObject)}");

			base.OnRenderUploadGUI(definitionData, ModelPrefab, token =>
			{
				//DO NOT REFERNECE THE ABOVE DEFINITION. IT NO LONGER EXISTS
				//THIS IS DUE TO SCENE RELOAD
				definitionData = ModelPrefab.GetComponent<TModelDataDefinitionType>();

				definitionData.ContentGuid = token.ContentGuid;
				definitionData.ContentId = token.ContentId;

				PrefabUtility.SavePrefabAsset(ModelPrefab);
			});
		}

		protected override void UnAuthenticatedOnGUI()
		{
			//TODO: Redirect to login.
		}
	}
}
