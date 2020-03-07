using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class CreateInitialEntityWorldTransformEventListener : BaseSingleEventListenerInitializable<IEntityCreationStartingEventSubscribable, EntityCreationStartingEventArgs>
	{
		private IEntityGuidMappable<WorldTransform> TransformMap { get; }

		private IReadonlyEntityGuidMappable<MovementBlockData> MovementDataMappable { get; }

		public CreateInitialEntityWorldTransformEventListener(IEntityCreationStartingEventSubscribable subscriptionService,
			[NotNull] IEntityGuidMappable<WorldTransform> transformMap,
			[NotNull] IReadonlyEntityGuidMappable<MovementBlockData> movementDataMappable) : base(subscriptionService)
		{
			TransformMap = transformMap ?? throw new ArgumentNullException(nameof(transformMap));
			MovementDataMappable = movementDataMappable ?? throw new ArgumentNullException(nameof(movementDataMappable));
		}

		protected override void OnEventFired(object source, EntityCreationStartingEventArgs args)
		{
			MovementBlockData movementData = MovementDataMappable.RetrieveEntity(args.EntityGuid);

			//We just need to make the world transform match
			//the initial movement data
			TransformMap.AddObject(args.EntityGuid, new WorldTransform(movementData.InitialPosition.x, movementData.InitialPosition.y, movementData.InitialPosition.z, movementData.Rotation));
		}
	}
}
