using System; using FreecraftCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public sealed class BehaviourAttachmentContext
	{
		public GameObject TargetGameObject { get; }

		public GameObjectType DesiredType { get; }

		public BehaviourAttachmentContext([NotNull] GameObject targetGameObject, GameObjectType desiredType)
		{
			if (!Enum.IsDefined(typeof(GameObjectType), desiredType)) throw new InvalidEnumArgumentException(nameof(desiredType), (int) desiredType, typeof(GameObjectType));

			TargetGameObject = targetGameObject ?? throw new ArgumentNullException(nameof(targetGameObject));
			DesiredType = desiredType;
		}
	}
}
