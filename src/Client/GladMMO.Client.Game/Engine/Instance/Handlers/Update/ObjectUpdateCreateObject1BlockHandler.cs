using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using GladMMO;
using GladNet;
using Nito.AsyncEx;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class ObjectUpdateCreateObject1BlockHandler : BaseObjectUpdateBlockHandler<ObjectUpdateCreateObject1Block>
	{
		private INetworkEntityVisibilityEventPublisher VisibilityEventPublisher { get; }

		private IFactoryCreatable<NetworkEntityNowVisibleEventArgs, ObjectCreationData> VisibleEventFactory { get; }

		private IEntityGuidMappable<AsyncLock> LockMappable { get; }

		/// <inheritdoc />
		public ObjectUpdateCreateObject1BlockHandler(ILog logger,
			[NotNull] INetworkEntityVisibilityEventPublisher visibilityEventPublisher,
			[NotNull] IFactoryCreatable<NetworkEntityNowVisibleEventArgs, ObjectCreationData> visibleEventFactory,
			[NotNull] IEntityGuidMappable<AsyncLock> lockMappable)
			: base(ObjectUpdateType.UPDATETYPE_CREATE_OBJECT, logger)
		{
			VisibilityEventPublisher = visibilityEventPublisher ?? throw new ArgumentNullException(nameof(visibilityEventPublisher));
			VisibleEventFactory = visibleEventFactory ?? throw new ArgumentNullException(nameof(visibleEventFactory));
			LockMappable = lockMappable ?? throw new ArgumentNullException(nameof(lockMappable));
		}

		/// <inheritdoc />
		public override void HandleUpdateBlock([NotNull] ObjectUpdateCreateObject1Block updateBlock)
		{
			if(updateBlock == null) throw new ArgumentNullException(nameof(updateBlock));

			Logger.Info($"Attempting to Spawn: {updateBlock.CreationData.CreationObjectType}");

			//TODO: There is a race condition here, this can happen if an entity despawn is occuring at the same time this creation
			//packet is being processed. Meaning that we could result in unknown behavior
			if(!LockMappable.ContainsKey(updateBlock.CreationData.CreationGuid))
				LockMappable.AddObject(updateBlock.CreationData.CreationGuid, new AsyncLock());

			switch(updateBlock.CreationData.CreationObjectType)
			{
				case ObjectType.Object:
					break;
				case ObjectType.Item:
					break;
				case ObjectType.Container:
					break;
				case ObjectType.Unit:
					break;
				case ObjectType.Player:
					//HelloKitty: This is the special case of the local player spawning into the world.
					if(updateBlock.CreationData.MovementData.UpdateFlags.HasFlag(ObjectUpdateFlags.UPDATEFLAG_SELF))
					{
						if(Logger.IsInfoEnabled)
							Logger.Info($"Recieved local player spawn data. Id: {updateBlock.CreationData.CreationGuid.CurrentObjectGuid}");
					}
					else if(Logger.IsInfoEnabled)
						Logger.Info($"Recieved Remote Player Spawn Data. Id: {updateBlock.CreationData.CreationGuid.CurrentObjectGuid}");
					break;
				case ObjectType.GameObject:
					break;
				case ObjectType.DynamicObject:
					break;
				case ObjectType.Corpse:
					break;
				case ObjectType.AreaTrigger:
					break;
				case ObjectType.SceneObject:
					break;
				case ObjectType.Conversation:
					break;
				case ObjectType.Map:
					break;
				default:
					throw new ArgumentOutOfRangeException($"Unable to handle the creation of ObjectType: {updateBlock.UpdateType}");
			}

			NetworkEntityNowVisibleEventArgs visibilityEvent = VisibleEventFactory.Create(updateBlock.CreationData);

			//Now we broadcast that an entity is now visible.
			VisibilityEventPublisher.Publish(visibilityEvent);
		}
	}
}