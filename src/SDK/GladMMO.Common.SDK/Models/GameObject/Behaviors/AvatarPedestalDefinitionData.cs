using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO.SDK
{
	public sealed class AvatarPedestalDefinitionData : GladMMOSDKMonoBehaviour, INetworkGameObjectBehaviour
	{
		public GameObjectType BehaviorType => GameObjectType.AvatarPedestal;

		/// <summary>
		/// The Avatar's Model ID.
		/// </summary>
		[HideInInspector]
		[SerializeField]
		private int _TargetAvatarModelId;

		public int TargetAvatarModelId
		{
			get => _TargetAvatarModelId;
			set => _TargetAvatarModelId = value;
		}
	}
}
