using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	/// <summary>
	/// This event will fire after the <see cref="GameObject"/> representation
	/// of an entity has been created. The Entity data is not PROMISED to be fully
	/// initialized at this point. It may not have all <see cref="IEntityGuidMappable{TValue}"/>
	/// containers initialized. It will have the <see cref="GameObject"/> created though.
	/// </summary>
	public interface IEntityWorldRepresentationCreatedEventSubscribable
	{
		event EventHandler<EntityWorldRepresentationCreatedEventArgs> OnEntityWorldRepresentationCreated;
	}

	public sealed class EntityWorldRepresentationCreatedEventArgs : EventArgs
	{
		/// <summary>
		/// The entity guid of the creating entity.
		/// </summary>
		public ObjectGuid EntityGuid { get; }

		/// <summary>
		/// The world/gameobject representation of the entity.
		/// </summary>
		public GameObject EntityWorldRepresentation { get; }

		/// <inheritdoc />
		public EntityWorldRepresentationCreatedEventArgs([NotNull] ObjectGuid entityGuid, GameObject entityWorldRepresentation)
		{
			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
			EntityWorldRepresentation = entityWorldRepresentation; //don't check null, due to threading problems.
		}
	}
}
