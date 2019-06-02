using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using ProtoBuf;

namespace GladMMO
{
	/// <summary>
	/// Packet sent when object visibility changes.
	/// Such as a player has come into view or a player 
	/// </summary>
	[ProtoContract]
	[GamePayload(GamePayloadOperationCode.EntityVisibilityChange)]
	public sealed class NetworkObjectVisibilityChangeEventPayload : GameServerPacketPayload
	{
		//TODO: We should also include any initialization stuff, such as update values or initial movement data.
		[ProtoMember(1)]
		private readonly EntityCreationData[] _EntitiesToCreate;

		[ProtoMember(2)]
		private readonly NetworkEntityGuid[] _OutOfRangeEntities;

		/// <summary>
		/// List of now newly visible entites to create.
		/// </summary>
		[ProtoIgnore]
		public IReadOnlyCollection<EntityCreationData> EntitiesToCreate => _EntitiesToCreate ?? Array.Empty<EntityCreationData>();

		/// <summary>
		/// List of now out-of-range entities.
		/// </summary>
		[ProtoIgnore]
		public IReadOnlyCollection<NetworkEntityGuid> OutOfRangeEntities => _OutOfRangeEntities ?? Array.Empty<NetworkEntityGuid>();

		/// <inheritdoc />
		public NetworkObjectVisibilityChangeEventPayload([NotNull] EntityCreationData[] entitiesToCreate, [NotNull] NetworkEntityGuid[] outOfRangeEntities)
		{
			_EntitiesToCreate = entitiesToCreate ?? throw new ArgumentNullException(nameof(entitiesToCreate));
			_OutOfRangeEntities = outOfRangeEntities ?? throw new ArgumentNullException(nameof(outOfRangeEntities));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		protected NetworkObjectVisibilityChangeEventPayload()
		{
			
		}
	}
}
