using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using ProtoBuf;

namespace GladMMO
{
	[ProtoContract]
	public sealed class EntityCreationData : IEntityGuidContainer
	{
		/// <summary>
		/// The GUID of the entity.
		/// </summary>
		[ProtoMember(1, IsRequired = true)]
		public ObjectGuid EntityGuid { get; }

		/// <summary>
		/// The initial movement data to use to
		/// create the entity.
		/// This is sent because we need to know position and movement details to
		/// create the entity and move it until movement updates are recieved.
		/// </summary>
		[ProtoMember(2, IsRequired = true)]
		public IMovementData InitialMovementData { get; }

		/// <summary>
		/// The initial data fields for the entity.
		/// </summary>
		[ProtoMember(3, IsRequired = true)]
		public FieldValueUpdate InitialFieldValues { get; }

		/// <inheritdoc />
		public EntityCreationData([NotNull] ObjectGuid entityGuid, [NotNull] IMovementData initialMovementData, [NotNull] FieldValueUpdate initialFieldValues)
		{
			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
			InitialMovementData = initialMovementData ?? throw new ArgumentNullException(nameof(initialMovementData));
			InitialFieldValues = initialFieldValues ?? throw new ArgumentNullException(nameof(initialFieldValues));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		protected EntityCreationData()
		{

		}
	}
}
