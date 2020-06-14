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
	/// <summary>
	/// A copy of <see cref="ObjectUpdateCreateObject1BlockHandler"/> but called
	/// for copied or respawning objects.
	/// </summary>
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class ObjectUpdateCreateObject2BlockHandler : BaseObjectUpdateBlockHandler<ObjectUpdateCreateObject2Block>
	{
		private INetworkEntityVisibilityEventPublisher VisibilityEventPublisher { get; }

		private IFactoryCreatable<NetworkEntityNowVisibleEventArgs, ObjectCreationData> VisibleEventFactory { get; }

		private IEntityGuidMappable<AsyncLock> LockMappable { get; }

		/// <inheritdoc />
		public ObjectUpdateCreateObject2BlockHandler(ILog logger,
			[NotNull] INetworkEntityVisibilityEventPublisher visibilityEventPublisher,
			[NotNull] IFactoryCreatable<NetworkEntityNowVisibleEventArgs, ObjectCreationData> visibleEventFactory,
			[NotNull] IEntityGuidMappable<AsyncLock> lockMappable)
			: base(ObjectUpdateType.UPDATETYPE_CREATE_OBJECT2, logger)
		{
			VisibilityEventPublisher = visibilityEventPublisher ?? throw new ArgumentNullException(nameof(visibilityEventPublisher));
			VisibleEventFactory = visibleEventFactory ?? throw new ArgumentNullException(nameof(visibleEventFactory));
			LockMappable = lockMappable ?? throw new ArgumentNullException(nameof(lockMappable));
		}

		/// <inheritdoc />
		public override void HandleUpdateBlock([NotNull] ObjectUpdateCreateObject2Block updateBlock)
		{
			if(updateBlock == null) throw new ArgumentNullException(nameof(updateBlock));

			Logger.Info($"Attempting to Respawn: {updateBlock.CreationData.CreationObjectType}");

			//TODO: There is a race condition here, this can happen if an entity despawn is occuring at the same time this creation
			//packet is being processed. Meaning that we could result in unknown behavior
			if (!LockMappable.ContainsKey(updateBlock.CreationData.CreationGuid))
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
					if(Logger.IsWarnEnabled)
						Logger.Warn($"Encountered Respawn for Player.");
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