using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using GladNet;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class OnCameraInputChangedUpdateWorldTransformRotationEventListener : BaseSingleEventListenerInitializable<ICameraInputChangedEventSubscribable, CameraInputChangedEventArgs>
	{
		private ILocalPlayerDetails PlayerDetails { get; }

		private IEntityGuidMappable<WorldTransform> TransformMappable { get; }

		public OnCameraInputChangedUpdateWorldTransformRotationEventListener(ICameraInputChangedEventSubscribable subscriptionService,
			[NotNull] IEntityGuidMappable<WorldTransform> transformMappable,
			[NotNull] ILocalPlayerDetails playerDetails)
			: base(subscriptionService)
		{
			TransformMappable = transformMappable ?? throw new ArgumentNullException(nameof(transformMappable));
			PlayerDetails = playerDetails ?? throw new ArgumentNullException(nameof(playerDetails));
		}

		protected override void OnEventFired(object source, CameraInputChangedEventArgs args)
		{
			WorldTransform worldTransform = TransformMappable.RetrieveEntity(PlayerDetails.LocalPlayerGuid);
			TransformMappable.ReplaceObject(PlayerDetails.LocalPlayerGuid, new WorldTransform(worldTransform.PositionX, worldTransform.PositionY, worldTransform.PositionZ, args.Rotation));
		}
	}
}
