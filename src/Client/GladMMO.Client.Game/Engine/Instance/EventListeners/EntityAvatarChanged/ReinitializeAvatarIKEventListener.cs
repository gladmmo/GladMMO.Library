using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using FreecraftCore;
using GladMMO.FinalIK;
using GladMMO.SDK;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class ReinitializeAvatarIKEventListener : EntityAvatarChangedEventListener
	{
		private IReadonlyEntityGuidMappable<EntityGameObjectDirectory> GameObjectDirectoryMappable { get; }

		private ILog Logger { get; }

		public ReinitializeAvatarIKEventListener(IEntityAvatarChangedEventSubscribable subscriptionService,
			[NotNull] IReadonlyEntityGuidMappable<EntityGameObjectDirectory> gameObjectDirectoryMappable,
			[NotNull] ILog logger) 
			: base(subscriptionService)
		{
			GameObjectDirectoryMappable = gameObjectDirectoryMappable ?? throw new ArgumentNullException(nameof(gameObjectDirectoryMappable));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void OnEventFired(object source, EntityAvatarChangedEventArgs args)
		{
			if (args.EntityGuid.TypeId != EntityTypeId.TYPEID_PLAYER)
				return;

			//Now we've assigned the handle, we need to actually handle the spawning/loading of the avatar.
			GameObject ikRootGameObject = GameObjectDirectoryMappable.RetrieveEntity(args.EntityGuid).GetGameObject(EntityGameObjectDirectory.Type.IKRoot);
			GameObject newlySpawnedAvatar = args.AvatarWorldRepresentation;

			//Try to get AvatarBoneSDKData from root spawned model
			AvatarBoneSDKData boneSdkData = newlySpawnedAvatar.GetComponent<AvatarBoneSDKData>();

			//TODO: Head height.
			//We can set relative camera height for VR users or first person users.
			//Don't do it for desktop.
			if(boneSdkData != null)
			{
				GameObject nameRoot = GameObjectDirectoryMappable.RetrieveEntity(args.EntityGuid).GetGameObject(EntityGameObjectDirectory.Type.NameRoot);
				nameRoot.transform.localPosition = new Vector3(nameRoot.transform.localPosition.x, boneSdkData.FloatingNameHeight, nameRoot.transform.localPosition.z);

				//Don't use 0 or super small head heights. They're probably wrong, especially negative ones.
				if(boneSdkData.HeadHeight > 0.1f)
				{
					GameObject headRoot = GameObjectDirectoryMappable.RetrieveEntity(args.EntityGuid).GetGameObject(EntityGameObjectDirectory.Type.HeadRoot);
					headRoot.transform.localPosition = new Vector3(headRoot.transform.localPosition.x, boneSdkData.HeadHeight, headRoot.transform.localPosition.z);
				}
			}

			//This will actually re-initialize the IK for the new avatar, since the old one is now gone.
			ikRootGameObject.GetComponent<IIKReinitializable>().ReInitialize();
		}
	}
}
