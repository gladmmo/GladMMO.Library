using System;
using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	/// <summary>
	/// Contract for a type that implements a subscription service for events that publish <see cref="EntityAvatarChangedEventArgs"/>
	/// through the <see cref="EventHandler{TEventArgs}"/> <see cref="OnEntityAvatarChanged"/>
	/// </summary>
	public interface IEntityAvatarChangedEventSubscribable
	{
		event EventHandler<EntityAvatarChangedEventArgs> OnEntityAvatarChanged;
	}

	/// <summary>
	/// Event arguments for the <see cref="IEntityAvatarChangedEventSubscribable"/> interface.
	/// </summary>
	public sealed class EntityAvatarChangedEventArgs : EventArgs, IEntityGuidContainer
	{
		/// <summary>
		/// The guid of the entity the avatar is changed for.
		/// </summary>
		public ObjectGuid EntityGuid { get; }

		/// <summary>
		/// The world model of the new avatar.
		/// </summary>
		public GameObject AvatarWorldRepresentation { get; }

		public EntityAvatarChangedEventArgs([NotNull] ObjectGuid entityGuid, [NotNull] GameObject avatarWorldRepresentation)
		{
			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
			AvatarWorldRepresentation = avatarWorldRepresentation ?? throw new ArgumentNullException(nameof(avatarWorldRepresentation));
		}
	}
}