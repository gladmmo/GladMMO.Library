using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	/// <summary>
	/// Basically the directory component for addressing non-root <see cref="GameObject"/>
	/// associated with the avatar/entity/player.
	/// </summary>
	public sealed class EntityGameObjectDirectory : MonoBehaviour
	{
		[SerializeField]
		private GameObject RootGameObject;

		[SerializeField]
		private GameObject IKRoot;

		[SerializeField]
		private GameObject CameraRoot;

		[SerializeField]
		private GameObject RightHand;

		[SerializeField]
		private GameObject LeftHand;

		[SerializeField]
		private GameObject NameRoot;

		[SerializeField]
		private GameObject HeadRoot;

		public enum Type
		{
			Root = 0,
			IKRoot = 1,
			CameraRoot = 2,
			RightHand = 3,
			LeftHand = 4,
			NameRoot = 5,
			HeadRoot = 6,
		}

		public GameObject GetGameObject(EntityGameObjectDirectory.Type gameObjectType)
		{
			if(!Enum.IsDefined(typeof(Type), gameObjectType)) throw new InvalidEnumArgumentException(nameof(gameObjectType), (int)gameObjectType, typeof(Type));

			switch (gameObjectType)
			{
				case Type.Root:
					return RootGameObject;
				case Type.IKRoot:
					return IKRoot;
				case Type.CameraRoot:
					return CameraRoot;
				case Type.RightHand:
					return RightHand;
				case Type.LeftHand:
					return LeftHand;
				case Type.NameRoot:
					return NameRoot;
				case Type.HeadRoot:
					return HeadRoot;
				default:
					throw new ArgumentOutOfRangeException(nameof(gameObjectType), gameObjectType, null);
			}
		}
	}
}
