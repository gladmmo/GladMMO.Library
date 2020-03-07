using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	/// <summary>
	/// Event subscriber that links the <see cref="GameObject"/> and <see cref="ObjectGuid"/> of an Entity
	/// together in a two-way relationship.
	/// </summary>
	public class SharedInitializeEntityWorldMappablesEventListener : BaseSingleEventListenerInitializable<IEntityWorldRepresentationCreatedEventSubscribable, EntityWorldRepresentationCreatedEventArgs>
	{
		private IEntityGuidMappable<GameObject> GuidToGameObjectMappable { get; }

		private IGameObjectToEntityMappable GameObjectToEntityMap { get; }

		public SharedInitializeEntityWorldMappablesEventListener(IEntityWorldRepresentationCreatedEventSubscribable subscriptionService,
			[NotNull] IEntityGuidMappable<GameObject> guidToGameObjectMappable,
			[NotNull] IGameObjectToEntityMappable gameObjectToEntityMap) 
			: base(subscriptionService)
		{
			GuidToGameObjectMappable = guidToGameObjectMappable ?? throw new ArgumentNullException(nameof(guidToGameObjectMappable));
			GameObjectToEntityMap = gameObjectToEntityMap ?? throw new ArgumentNullException(nameof(gameObjectToEntityMap));
		}

		protected override void OnEventFired(object source, EntityWorldRepresentationCreatedEventArgs args)
		{
			//Basically, all this one does is it just initializes the containers.
			GameObjectToEntityMap.ObjectToEntityMap.Add(args.EntityWorldRepresentation, args.EntityGuid);
			GuidToGameObjectMappable.AddObject(args.EntityGuid, args.EntityWorldRepresentation);
		}
	}
}
