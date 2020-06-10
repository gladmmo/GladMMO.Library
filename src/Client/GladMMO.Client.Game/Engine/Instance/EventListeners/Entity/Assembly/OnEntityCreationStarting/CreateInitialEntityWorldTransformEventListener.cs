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
			switch (args.EntityGuid.TypeId)
			{
				case EntityTypeId.TYPEID_OBJECT:
				case EntityTypeId.TYPEID_ITEM:
				case EntityTypeId.TYPEID_CONTAINER:
					return; //This object types don't have movement.
				case EntityTypeId.TYPEID_UNIT:
				case EntityTypeId.TYPEID_PLAYER:
				case EntityTypeId.TYPEID_GAMEOBJECT:
				case EntityTypeId.TYPEID_DYNAMICOBJECT:
				case EntityTypeId.TYPEID_CORPSE:
				default:
					break;
			}

			MovementBlockData movementData = MovementDataMappable.RetrieveEntity(args.EntityGuid);

			//We just need to make the world transform match
			//the initial movement data
			TransformMap.AddObject(args.EntityGuid, new WorldTransform(movementData.MoveInfo.Position.X, movementData.MoveInfo.Position.Y, movementData.MoveInfo.Position.Z, movementData.MoveInfo.Orientation));
		}
	}
}
