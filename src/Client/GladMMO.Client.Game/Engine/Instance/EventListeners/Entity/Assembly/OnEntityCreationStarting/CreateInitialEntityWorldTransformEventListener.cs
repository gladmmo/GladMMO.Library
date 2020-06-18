using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class CreateInitialEntityWorldTransformEventListener : BaseSingleEventListenerInitializable<IEntityCreationStartingEventSubscribable, EntityCreationStartingEventArgs>
	{
		private IEntityGuidMappable<WorldTransform> TransformMap { get; }

		private IReadonlyEntityGuidMappable<MovementBlockData> MovementDataMappable { get; }

		private IWorldTransformFactory WorldTransformFactory { get; }

		private ILog Logger { get; }

		public CreateInitialEntityWorldTransformEventListener(IEntityCreationStartingEventSubscribable subscriptionService,
			[NotNull] IEntityGuidMappable<WorldTransform> transformMap,
			[NotNull] IReadonlyEntityGuidMappable<MovementBlockData> movementDataMappable,
			[NotNull] ILog logger,
			[NotNull] IWorldTransformFactory worldTransformFactory) : base(subscriptionService)
		{
			TransformMap = transformMap ?? throw new ArgumentNullException(nameof(transformMap));
			MovementDataMappable = movementDataMappable ?? throw new ArgumentNullException(nameof(movementDataMappable));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			WorldTransformFactory = worldTransformFactory ?? throw new ArgumentNullException(nameof(worldTransformFactory));
		}

		protected override void OnEventFired(object source, EntityCreationStartingEventArgs args)
		{
			//Only world objects have WorldTransforms.
			if (!args.EntityGuid.IsWorldObject())
				return;

			MovementBlockData movementData = MovementDataMappable.RetrieveEntity(args.EntityGuid);
			WorldTransform worldTransform = WorldTransformFactory.Create(movementData);

			//Can happen if we cannot handle the movement block
			if (worldTransform != null)
			{
				TransformMap.AddObject(args.EntityGuid, worldTransform);
			}
			else
			{
				if(Logger.IsWarnEnabled)
					Logger.Warn($"Unhandled WorldTransform Creation: {args.EntityGuid} Flags: {movementData.UpdateFlags}");
			}
		}
	}
}
