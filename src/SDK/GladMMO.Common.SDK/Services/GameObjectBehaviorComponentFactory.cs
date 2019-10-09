using System;
using System.Collections.Generic;
using System.Text;
using GladMMO.SDK;
using UnityEngine;

namespace GladMMO
{
	public sealed class GameObjectBehaviorComponentFactory : IFactoryCreatable<INetworkGameObjectBehaviour, BehaviourAttachmentContext>
	{
		public INetworkGameObjectBehaviour Create([NotNull] BehaviourAttachmentContext context)
		{
			if (context == null) throw new ArgumentNullException(nameof(context));

			switch (context.DesiredType)
			{
				case GameObjectType.Visual:
					throw new InvalidOperationException($"Cannot attach behaviours for Visual object.");
				case GameObjectType.WorldTeleporter:
					return context.TargetGameObject.AddComponent<WorldTeleporterDefinitionData>();
				case GameObjectType.AvatarPedestal:
					return context.TargetGameObject.AddComponent<AvatarPedestalDefinitionData>();
				default:
					throw new InvalidOperationException($"Cannot attach behaviours for {context.DesiredType} object.");
			}
		}
	}
}
