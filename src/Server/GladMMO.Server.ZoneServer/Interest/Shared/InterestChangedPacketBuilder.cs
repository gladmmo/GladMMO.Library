using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;

namespace GladMMO
{
	public sealed class InterestChangedPacketBuilder
	{
		/// <summary>
		/// The movement data mappabled from an entity guid.
		/// </summary>
		private IReadonlyEntityGuidMappable<IMovementData> MovementMapper { get; }

		private IReadonlyEntityGuidMappable<IChangeTrackableEntityDataCollection> EntityDataMapper { get; }

		private IFactoryCreatable<FieldValueUpdate, EntityFieldUpdateCreationContext> FieldUpdateFactory { get; }

		/// <inheritdoc />
		public InterestChangedPacketBuilder(
			[NotNull] IReadonlyEntityGuidMappable<IMovementData> movementMapper, 
			[NotNull] IReadonlyEntityGuidMappable<IChangeTrackableEntityDataCollection> entityDataMapper,
			[NotNull] IFactoryCreatable<FieldValueUpdate, EntityFieldUpdateCreationContext> fieldUpdateFactory)
		{
			MovementMapper = movementMapper ?? throw new ArgumentNullException(nameof(movementMapper));
			EntityDataMapper = entityDataMapper ?? throw new ArgumentNullException(nameof(entityDataMapper));
			FieldUpdateFactory = fieldUpdateFactory ?? throw new ArgumentNullException(nameof(fieldUpdateFactory));
		}

		public NetworkObjectVisibilityChangeEventPayload Build([NotNull] IReadOnlyCollection<NetworkEntityGuid> joining, [NotNull] IReadOnlyCollection<NetworkEntityGuid> leaving)
		{
			if(joining == null) throw new ArgumentNullException(nameof(joining));
			if(leaving == null) throw new ArgumentNullException(nameof(leaving));

			if(joining.Count == 0 && leaving.Count == 0)
				throw new InvalidOperationException($"Cannot build interest change packet when changes are empty.");

			//Arrange packet data
			EntityCreationData[] creationDatas = joining
				.Select(j => new EntityCreationData(j, MovementMapper[j], BuildInitialDataFieldValues(j)))
				.ToArray();

			NetworkObjectVisibilityChangeEventPayload changePayload = new NetworkObjectVisibilityChangeEventPayload(creationDatas, leaving.ToArray());

			return changePayload;
		}

		//TODO: We should do some initial field data caching so we don't rebuild it multiple times per game tick
		private FieldValueUpdate BuildInitialDataFieldValues(NetworkEntityGuid entityGuid)
		{
			return FieldUpdateFactory.Create(new EntityFieldUpdateCreationContext(EntityDataMapper[entityGuid], EntityDataMapper[entityGuid].DataSetIndicationArray));
		}
	}
}
