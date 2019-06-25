using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;
using UnityEditor;
using UnityEngine;

namespace GladMMO
{
	[CustomEditor(typeof(CreatureStaticSpawnPointDefinition))]
	public sealed class CreatureStaticSpawnPointDefinitionTypeDrawer : StaticSpawnPointDefinitionEditor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			bool isSaveable = true;

			//If the instance id is -1 then this creature instance is NOT networked, the information will not exists in the database at all.
			if (GetTarget().CreatureInstanceId == -1)
			{
				//So, we should a button asking the user if they want to create a creature instance.
				if (GUILayout.Button($"Create Creature Instance"))
				{
					//TODO: This is just for testing, we should properly handle this
					ICreatureDataServiceClient client = RestService.For<ICreatureDataServiceClient>("http://192.168.0.12:5005");

					//If they press this, we need to actually create a creature instance for this world id.
					var result = client.CreateCreatureInstance(30).ConfigureAwait(false).GetAwaiter().GetResult(); //TODO: This is just for testing. We should get the world id from the scene somehow.

					if (result.isSuccessful)
					{
						GetTarget().CreatureInstanceId = result.Result.Guid.EntityId;
						EditorUtility.SetDirty(GetTarget());
					}
				}

				//No matter what, do not continue with editing. Since the creature DOES NOT EXIST on the backend.
				return;
			}
			else
			{
				//TODO: This is just for testing, we should properly handle this
				ICreatureDataServiceClient client = RestService.For<ICreatureDataServiceClient>("http://192.168.0.12:5005");

				var task = client.GetCreatureInstance(GetTarget().CreatureInstanceId);
				var queryResponseModel = task.ConfigureAwait(false).GetAwaiter().GetResult();

				//TODO: No idea what should be done here.
				if (!queryResponseModel.isSuccessful)
					return;

				//Just show the instance id
				GUILayout.Label($"Creature Instance: {GetTarget().CreatureInstanceId}\nGuid: {queryResponseModel.Result.Guid}\nSpawnPosition: {queryResponseModel.Result.InitialPosition}\nYRotation: {queryResponseModel.Result.YAxisRotation}", GUI.skin.textArea);
			}

			//The reason we do this manually is so that it can be hidden before there is an instance id.
			GetTarget().CreatureTemplateId = EditorGUILayout.IntField($"Template Id", GetTarget().CreatureTemplateId);

			//Now, if the creature template id is not -1 we should try to load the template
			if (GetTarget().CreatureTemplateId != -1)
			{
				//TODO: This is just for testing, we should properly handle this
				ICreatureDataServiceClient client = RestService.For<ICreatureDataServiceClient>("http://192.168.0.12:5005");
				var task = client.GetCreatureTemplate(GetTarget().CreatureTemplateId);
				var queryResponseModel = task.ConfigureAwait(false).GetAwaiter().GetResult();

				if (!queryResponseModel.isSuccessful)
				{
					isSaveable = false;
					GUILayout.Label($"Unknown Creature Template: {GetTarget().CreatureTemplateId}", GUI.skin.textArea);
				}
				else
				{
					var result = queryResponseModel.Result;
					GUILayout.Label($"Creature Template: {GetTarget().CreatureTemplateId}\nName: {result.CreatureName}\nModel Id: {result.ModelId}\nLevel Range: {result.MinimumLevel}-{result.MaximumLevel}", GUI.skin.textArea);
				}
			}

			if (isSaveable)
			{
				if (GUILayout.Button("Save Updates"))
				{
					ICreatureDataServiceClient client = RestService.For<ICreatureDataServiceClient>("http://192.168.0.12:5005");

					//Just sent the updated model.
					client.UpdateCreatureInstance(GetTarget().CreatureInstanceId, new CreatureInstanceModel(BuildNetworkEntityGuid(), GetTarget().CreatureTemplateId, GetTarget().transform.position, GetTarget().transform.eulerAngles.y))
						.ConfigureAwait(false).GetAwaiter().GetResult();
				}
			}
		}

		private NetworkEntityGuid BuildNetworkEntityGuid()
		{
			return new NetworkEntityGuidBuilder()
				.WithId(GetTarget().CreatureInstanceId)
				.WithType(EntityType.Npc)
				.WithTemplate(GetTarget().CreatureTemplateId)
				.Build();
		}

		private CreatureStaticSpawnPointDefinition GetTarget()
		{
			return (CreatureStaticSpawnPointDefinition)target;
		}
	}
}
