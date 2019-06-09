using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class LocalPlayerSpawnedInitializeCameraMappableEventListener : OnLocalPlayerSpawnedEventListener
	{
		private IEntityGuidMappable<Camera> CameraMappable { get; }

		public LocalPlayerSpawnedInitializeCameraMappableEventListener(ILocalPlayerSpawnedEventSubscribable subscriptionService, 
			[NotNull] IEntityGuidMappable<Camera> cameraMappable) 
			: base(subscriptionService)
		{
			CameraMappable = cameraMappable ?? throw new ArgumentNullException(nameof(cameraMappable));
		}

		protected override void OnLocalPlayerSpawned(LocalPlayerSpawnedEventArgs args)
		{
			//Just add the main camera.
			CameraMappable.AddObject(args.EntityGuid, Camera.main);
		}
	}
}
