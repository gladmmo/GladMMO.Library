using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using ProtoBuf;

namespace GladMMO
{
	[ProtoContract]
	[GamePayload(GamePayloadOperationCode.FieldValueUpdate)]
	public sealed class FieldValueUpdateEvent : GameServerPacketPayload
	{
		/// <summary>
		/// The field value updates.
		/// </summary>
		[ProtoMember(1)]
		private EntityAssociatedData<FieldValueUpdate>[] _FieldValueUpdates { get; set; }
		
		/// <summary>
		/// The field value updates.
		/// </summary>
		[ProtoIgnore]
		public IReadOnlyCollection<EntityAssociatedData<FieldValueUpdate>> FieldValueUpdates => _FieldValueUpdates;

		/// <inheritdoc />
		public FieldValueUpdateEvent([NotNull] EntityAssociatedData<FieldValueUpdate>[] fieldValueUpdates)
		{
			if(fieldValueUpdates == null) throw new ArgumentNullException(nameof(fieldValueUpdates));
			if(fieldValueUpdates.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(fieldValueUpdates));

			_FieldValueUpdates = fieldValueUpdates;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		protected FieldValueUpdateEvent()
		{
			
		}
	}
}
