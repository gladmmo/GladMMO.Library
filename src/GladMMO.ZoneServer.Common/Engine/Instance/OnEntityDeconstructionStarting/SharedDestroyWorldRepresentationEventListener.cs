using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	/// <summary>
	/// Base shared event listener for EntityDeconstructionStarting that will destroy the
	/// world representation/<see cref="GameObject"/> representation of the Entity.
	/// It will also broadcast the <see cref="IEntityWorldRepresentationDeconstructionFinishedEventSubscribable"/> and
	/// <see cref="IEntityWorldRepresentationDeconstructionStartingEventSubscribable"/> events.
	/// </summary>
	[AdditionalRegisterationAs(typeof(IEntityWorldRepresentationDeconstructionStartingEventSubscribable))]
	[AdditionalRegisterationAs(typeof(IEntityWorldRepresentationDeconstructionFinishedEventSubscribable))]
	public abstract class SharedDestroyWorldRepresentationEventListener : BaseSingleEventListenerInitializable<IEntityDeconstructionStartingEventSubscribable, EntityDeconstructionStartingEventArgs>, IEntityWorldRepresentationDeconstructionFinishedEventSubscribable, IEntityWorldRepresentationDeconstructionStartingEventSubscribable
	{
		private IReadonlyEntityGuidMappable<GameObject> GuidToGameObjectMappable { get; }

		private IGameObjectToEntityMappable GameObjectToEntityMap { get; }

		public event EventHandler<EntityWorldRepresentationDeconstructionFinishedEventArgs> OnEntityWorldRepresentationDeconstructionFinished;

		public event EventHandler<EntityWorldRepresentationDeconstructionStartingEventArgs> OnEntityWorldRepresentationDeconstructionStarting;

		protected SharedDestroyWorldRepresentationEventListener(IEntityDeconstructionStartingEventSubscribable subscriptionService,
			[NotNull] IReadonlyEntityGuidMappable<GameObject> guidToGameObjectMappable,
			[NotNull] IGameObjectToEntityMappable gameObjectToEntityMap) 
			: base(subscriptionService)
		{
			GuidToGameObjectMappable = guidToGameObjectMappable ?? throw new ArgumentNullException(nameof(guidToGameObjectMappable));
			GameObjectToEntityMap = gameObjectToEntityMap ?? throw new ArgumentNullException(nameof(gameObjectToEntityMap));
		}

		protected override void OnEventFired(object source, EntityDeconstructionStartingEventArgs args)
		{
			//Before we even touch the GameObject, let's broadcast this.
			OnEntityWorldRepresentationDeconstructionStarting?.Invoke(this, new EntityWorldRepresentationDeconstructionStartingEventArgs(args.EntityGuid));
			CleanupGameObject(args);
			OnEntityWorldRepresentationDeconstructionFinished?.Invoke(this, new EntityWorldRepresentationDeconstructionFinishedEventArgs(args.EntityGuid));

			//We don't need to touch the IEntityGuidMappable. Those get cleaned up seperately.
		}

		private void CleanupGameObject([NotNull] EntityDeconstructionStartingEventArgs args)
		{
			if (args == null) throw new ArgumentNullException(nameof(args));

			//This removes the world entity from it's special collection AND removes it from the relevant map
			GameObject rootEntityGameObject = GuidToGameObjectMappable.RetrieveEntity(args.EntityGuid);
			GameObjectToEntityMap.ObjectToEntityMap.Remove(rootEntityGameObject);
			GameObject.Destroy(rootEntityGameObject);
		}
	}
}
