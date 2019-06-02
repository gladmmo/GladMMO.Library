using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GladMMO
{
	public interface IEntityInterestChangeEventSubscribable
	{
		event EventHandler<EntityInterestChangeEventArgs> OnEntityInterestChanged;
	}

	public interface IEntityInterestChangeEventSpoofable
	{
		void SpoofExitInterest(EntityInterestChangeEventArgs args);

		void SpoofEnterInterest(EntityInterestChangeEventArgs args);
	}

	//Based on the old EntityInterestChangeContext
	public sealed class EntityInterestChangeEventArgs : EventArgs
	{
		/// <summary>
		/// The enterable entity represents the entity that an entity is joining
		/// the interest of. <see cref="EnteringEntity"/> is JOINING <see cref="EnterableEntity"/>'s
		/// interest collection.
		/// </summary>
		public NetworkEntityGuid EnterableEntity { get; }

		/// <summary>
		/// The entity entering <see cref="EnterableEntity"/>'s interest collection.
		/// </summary>
		public NetworkEntityGuid EnteringEntity { get; }

		/// <summary>
		/// Enumeration of the types of changes.
		/// </summary>
		public enum ChangeType
		{
			Enter = 1,
			Exit = 2,
		}

		public ChangeType ChangingType { get; }

		/// <inheritdoc />
		public EntityInterestChangeEventArgs([NotNull] NetworkEntityGuid enterableEntity, [NotNull] NetworkEntityGuid enteringEntity, ChangeType changingType)
		{
			if(!Enum.IsDefined(typeof(ChangeType), changingType)) throw new InvalidEnumArgumentException(nameof(changingType), (int)changingType, typeof(ChangeType));

			EnterableEntity = enterableEntity ?? throw new ArgumentNullException(nameof(enterableEntity));
			EnteringEntity = enteringEntity ?? throw new ArgumentNullException(nameof(enteringEntity));
			ChangingType = changingType;
		}
	}
}
