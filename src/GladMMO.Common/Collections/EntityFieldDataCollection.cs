using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Generic.Math;
using Reinterpret.Net;

namespace GladMMO
{
	public sealed class EntityFieldDataCollection<TFieldType> : IEntityDataFieldContainer, IEntityDataFieldContainer<TFieldType>
		where TFieldType : struct //TODO: When C# 8.0 or 7.3 is better supported switch to it for Enum constraint
	{
		//TODO: This is a hack until Enum constraint.
		static EntityFieldDataCollection()
		{
			if(!typeof(TFieldType).IsEnum)
				throw new InvalidOperationException($"Type: {typeof(TFieldType).Name} {nameof(TFieldType)} MUST be an enum type for Type: {typeof(EntityFieldDataCollection<>)}");
		}

		//Data fields are modeled as 4 byte fields.
		//.NET runtime does a really good job of optimizing Int32 operations
		//and Int32 arrays. Most fields are small enough to be represented by integer fields
		//and the largest 64bit fields can just take up 2 slots.
		private byte[] InternalDataFields { get; }

		/// <inheritdoc />
		public object SyncObj { get; } = new object();

		/// <inheritdoc />
		public WireReadyBitArray DataSetIndicationArray { get; }

		public EntityFieldDataCollection()
		{
			//TODO: Make this allocation more efficient. Maybe even use pooling.
			InternalDataFields = new byte[ComputeDataFieldCollectionLength()];
			DataSetIndicationArray = new WireReadyBitArray(ComputeBitLength());
		}

		/// <summary>
		/// Overload that supports initializing custom (by the exactly sized)
		/// <see cref="initialDataSetIndicationArray"/> and entity data <see cref="entityData"/>.
		/// </summary>
		/// <param name="initialDataSetIndicationArray">The initial data set array.</param>
		/// <param name="entityData"></param>
		public EntityFieldDataCollection(WireReadyBitArray initialDataSetIndicationArray, byte[] entityData)
		{
			if(initialDataSetIndicationArray == null) throw new ArgumentNullException(nameof(initialDataSetIndicationArray));

			//TODO: Make this allocation more efficient. Maybe even use pooling.
			InternalDataFields = entityData ?? throw new ArgumentNullException(nameof(entityData));

			//Internal data representation is bytes. Soooo, we must reduce it to 4 byte chunks which is what the bitlength for the wireready
			//bitarray represents.
			if((InternalDataFields.Length / 4) != initialDataSetIndicationArray.Length)
				throw new ArgumentException($"Failed to initialize entity field data collection due to incorrect Length: {initialDataSetIndicationArray.Length} vs Field Length: {(InternalDataFields.Length / 4)}");

			DataSetIndicationArray = initialDataSetIndicationArray;
		}

		//Same number of bits as fields.
		private static int ComputeBitLength()
		{
			return Enum.GetValues(typeof(TFieldType)).Length;
		}

		private static int ComputeDataFieldCollectionLength()
		{
			return ComputeBitLength() * 4;
		}

		/// <inheritdoc />
		public EntityFieldDataCollection(byte[] internalDataFields)
		{
			if(internalDataFields == null) throw new ArgumentNullException(nameof(internalDataFields));
			if(internalDataFields.Length != ComputeDataFieldCollectionLength())
				throw new InvalidOperationException($"Cannot initialize: {GetType().Name} with data fields Length: {internalDataFields}. Required exact Length: {ComputeDataFieldCollectionLength()}");

			InternalDataFields = internalDataFields;
		}

		//TFieldType

		public TValueType GetFieldValue<TValueType>(TFieldType index)
			where TValueType : struct
		{
			return GetFieldValue<TValueType>(GenericMath.Convert<TFieldType, int>(index));
		}

		//TODO: Would ref return be better here? Maybe only for 64bits?

		public TValueType GetFieldValue<TValueType>(int index)
			where TValueType : struct
		{
			IfIndexExceedsLengthThrow(index);

			//Just assume we can do it, the caller is responsible for the diaster.
			return Unsafe.As<byte, TValueType>(ref InternalDataFields[index * sizeof(int)]);
		}

		private void IfIndexExceedsLengthThrow(int index)
		{
			if(index >= InternalDataFields.Length)
				throw new ArgumentOutOfRangeException(nameof(index), $"Provided Index: {index} was out of range. Max index for Type: {typeof(TFieldType).Name} is Index: {InternalDataFields.Length - 1}");
		}

		/// <inheritdoc />
		public void SetFieldValue<TValueType>(int index, TValueType value) 
			where TValueType : struct
		{
			IfIndexExceedsLengthThrow(index);

			lock(SyncObj)
			{
				//Whenever someone sets, even if the value is not changing, we should set it being set (not changed).
				DataSetIndicationArray.Set(index, true);
				value.Reinterpret(InternalDataFields, index * sizeof(int));
			}
		}

		/// <inheritdoc />
		public void SetFieldValue<TValueType>(TFieldType index, TValueType value) 
			where TValueType : struct
		{
			SetFieldValue<TValueType>(GenericMath.Convert<TFieldType, int>(index), value);
		}
	}
}
