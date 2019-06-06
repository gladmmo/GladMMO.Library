using System;
using System.Collections.Generic;
using System.Text;
using GladNet;
using JetBrains.Annotations;
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

		/// <inheritdoc />
		public AdditionalServerRegisterationPlayerEntityFactoryDecorator(
			IFactoryCreatable<GameObject, PlayerEntityCreationContext> decoratedFactory, 
			IEntityGuidMappable<IPeerPayloadSendService<GameServerPacketPayload>> guidToSessionMappable, 
			IEntityGuidMappable<InterestCollection> guidToInterestCollectionMappable, 
			IDictionary<int, NetworkEntityGuid> connectionIdToControllingEntityMap)
		{
			DecoratedFactory = decoratedFactory;
			GuidToSessionMappable = guidToSessionMappable;
			GuidToInterestCollectionMappable = guidToInterestCollectionMappable;
			ConnectionIdToControllingEntityMap = connectionIdToControllingEntityMap;
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

			//We don't need to touch the gameobject, we can just return it.
			return gameObject;
		}
	}
}
