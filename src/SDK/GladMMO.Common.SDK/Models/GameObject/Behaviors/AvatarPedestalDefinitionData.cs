using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO.SDK
{
	public sealed class AvatarPedestalDefinitionData : GladMMOSDKMonoBehaviour, INetworkGameObjectBehaviour, IRemoteModelUpdateable<AvatarPedestalInstanceModel>
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

		public void UpdateModel([NotNull] AvatarPedestalInstanceModel model)
		{
			if (model == null) throw new ArgumentNullException(nameof(model));

			_TargetAvatarModelId = model.AvatarModelId;
		}
	}
}
