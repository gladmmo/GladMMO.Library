using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Glader.Essentials;

namespace GladMMO
{
	/*[AdditionalRegisterationAs(typeof(INetworkEntityVisibleEventSubscribable))]
	public sealed class ObjectUpdateCreateObject1BlockHandler : BaseObjectUpdateBlockHandler<ObjectUpdateCreateObject1Block>, INetworkEntityVisibleEventSubscribable
	{
		/// <inheritdoc />
		public event EventHandler<NetworkEntityNowVisibleEventArgs> OnNetworkEntityNowVisible;

		private IFactoryCreatable<NetworkEntityNowVisibleEventArgs, ObjectUpdateCreateObject1Block> VisibileEventFactory { get; }

		/// <inheritdoc />
		public ObjectUpdateCreateObject1BlockHandler(
			ILog logger, 
			[NotNull] IFactoryCreatable<NetworkEntityNowVisibleEventArgs, ObjectUpdateCreateObject1Block> visibileEventFactory) 
			: base(ObjectUpdateType.UPDATETYPE_CREATE_OBJECT, logger)
		{
			VisibileEventFactory = visibileEventFactory ?? throw new ArgumentNullException(nameof(visibileEventFactory));
		}

		/// <inheritdoc />
		public override void HandleUpdateBlock([NotNull] ObjectUpdateCreateObject1Block updateBlock)
		{
			if(updateBlock == null) throw new ArgumentNullException(nameof(updateBlock));

			Logger.Info($"Attempting to Spawn: {updateBlock.CreationData.CreationObjectType}");

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
							Logger.Info($"Recieved local player spawn data. Id:{updateBlock.CreationData.CreationGuid.CurrentObjectGuid}");
					}

					NetworkEntityNowVisibleEventArgs visibilityEvent = VisibileEventFactory.Create(updateBlock);

					//Now we broadcast that an entity is now visible.
					OnNetworkEntityNowVisible?.Invoke(this, visibilityEvent);
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
		}
	}*/
}
