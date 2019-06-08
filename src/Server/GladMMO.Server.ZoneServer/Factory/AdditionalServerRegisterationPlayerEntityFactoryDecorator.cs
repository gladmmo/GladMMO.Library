using System;
using System.Collections.Generic;
using System.Text;
using GladNet;
using JetBrains.Annotations;
using Nito.AsyncEx;
using UnityEngine;

namespace GladMMO
{
	/// <summary>
	/// Decorator for <see cref="IFactoryCreatable{TCreateType,TContextType}"/>'s that create
	/// player entities. Player entites require some additional creation logic compared to normal entities.
	/// </summary>
	public sealed class AdditionalServerRegisterationPlayerEntityFactoryDecorator : IFactoryCreatable<GameObject, PlayerEntityCreationContext>
	{
		private IFactoryCreatable<GameObject, PlayerEntityCreationContext> DecoratedFactory { get; }

		private IEntityGuidMappable<IPeerPayloadSendService<GameServerPacketPayload>> GuidToSessionMappable { get; }

		private IEntityGuidMappable<InterestCollection> GuidToInterestCollectionMappable { get; }

		private IDictionary<int, NetworkEntityGuid> ConnectionIdToControllingEntityMap { get; }

		private IEntityGuidMappable<AsyncReaderWriterLock> AsyncLockMappable { get; }

		private IEntityGuidMappable<CharacterController> CharacterControllerMappable { get; }

		private IEntityGuidMappable<IMovementGenerator<GameObject>> MovementGeneratorMappable { get; }

		/// <inheritdoc />
		public AdditionalServerRegisterationPlayerEntityFactoryDecorator(
			IFactoryCreatable<GameObject, PlayerEntityCreationContext> decoratedFactory, 
			IEntityGuidMappable<IPeerPayloadSendService<GameServerPacketPayload>> guidToSessionMappable, 
			IEntityGuidMappable<InterestCollection> guidToInterestCollectionMappable, 
			IDictionary<int, NetworkEntityGuid> connectionIdToControllingEntityMap, 
			IEntityGuidMappable<AsyncReaderWriterLock> asyncLockMappable, 
			IEntityGuidMappable<CharacterController> characterControllerMappable,
			[NotNull] IEntityGuidMappable<IMovementGenerator<GameObject>> movementGeneratorMappable)
		{
			DecoratedFactory = decoratedFactory;
			GuidToSessionMappable = guidToSessionMappable;
			GuidToInterestCollectionMappable = guidToInterestCollectionMappable;
			ConnectionIdToControllingEntityMap = connectionIdToControllingEntityMap;
			AsyncLockMappable = asyncLockMappable;
			CharacterControllerMappable = characterControllerMappable;
			MovementGeneratorMappable = movementGeneratorMappable ?? throw new ArgumentNullException(nameof(movementGeneratorMappable));
		}

		/// <inheritdoc />
		public GameObject Create(PlayerEntityCreationContext context)
		{
			GameObject gameObject = DecoratedFactory.Create(context);

			GuidToSessionMappable.AddObject(context.EntityGuid, context.SessionContext.ZoneSession);
			ConnectionIdToControllingEntityMap.Add(context.SessionContext.ConnectionId, context.EntityGuid);

			InterestCollection playerInterestCollection = new InterestCollection();

			//directly add ourselves so we don't become interest in ourselves after spawning
			playerInterestCollection.Add(context.EntityGuid);

			//We just create our own manaul interest collection here.
			GuidToInterestCollectionMappable.AddObject(context.EntityGuid, playerInterestCollection);
			AsyncLockMappable.AddObject(context.EntityGuid, new AsyncReaderWriterLock());
			CharacterControllerMappable.AddObject(context.EntityGuid, gameObject.GetComponent<CharacterController>());

			//TODO: We cannot do this in the future if we want to have flight paths, I guess.
			MovementGeneratorMappable.AddObject(context.EntityGuid, new IdleMovementGenerator()); //idle, since we are spawning.

			//We don't need to touch the gameobject, we can just return it.
			return gameObject;
		}
	}
}
