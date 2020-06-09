using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using GladMMO;
using GladNet;

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

		private IFactoryCreatable<NetworkEntityNowVisibleEventArgs, ObjectUpdateCreateObject1Block> VisibileEventFactory { get; }

		/// <inheritdoc />
		public ObjectUpdateCreateObject2BlockHandler(ILog logger,
			[NotNull] INetworkEntityVisibilityEventPublisher visibilityEventPublisher,
			[NotNull] IFactoryCreatable<NetworkEntityNowVisibleEventArgs, ObjectUpdateCreateObject1Block> visibileEventFactory)
			: base(ObjectUpdateType.UPDATETYPE_CREATE_OBJECT, logger)
		{
			VisibilityEventPublisher = visibilityEventPublisher ?? throw new ArgumentNullException(nameof(visibilityEventPublisher));
			VisibileEventFactory = visibileEventFactory ?? throw new ArgumentNullException(nameof(visibileEventFactory));
		}

		/// <inheritdoc />
		public override void HandleUpdateBlock([NotNull] ObjectUpdateCreateObject2Block updateBlock)
		{
			if(updateBlock == null) throw new ArgumentNullException(nameof(updateBlock));

			Logger.Info($"Attempting to Respawn: {updateBlock.CreationData.CreationObjectType}");

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

			NetworkEntityNowVisibleEventArgs visibilityEvent = VisibileEventFactory.Create(updateBlock);

			//Now we broadcast that an entity is now visible.
			VisibilityEventPublisher.Publish(visibilityEvent);
		}
	}
}