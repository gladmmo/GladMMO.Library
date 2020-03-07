using System; using FreecraftCore;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Generic.Math;
using Newtonsoft.Json;
using Reinterpret.Net;

namespace GladMMO
{
	[JsonObject]
	public sealed class EntityFieldDataCollection : IEntityDataFieldContainer
	{
		//Data fields are modeled as 4 byte fields.
		//.NET runtime does a really good job of optimizing Int32 operations
		//and Int32 arrays. Most fields are small enough to be represented by integer fields
		//and the largest 64bit fields can just take up 2 slots.
		[JsonProperty]
		private byte[] InternalDataFields { get; set; }

		/// <inheritdoc />
		[JsonIgnore]
		public object SyncObj { get; } = new object();

		/// <inheritdoc />
		[JsonProperty]
		public WireReadyBitArray DataSetIndicationArray { get; private set; }

		public EntityFieldDataCollection(int fieldCount)
		{
			//TODO: Make this allocation more efficient. Maybe even use pooling.
			InternalDataFields = new byte[fieldCount * sizeof(int)];
			DataSetIndicationArray = new WireReadyBitArray(fieldCount);
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

		/// <inheritdoc />
		public EntityFieldDataCollection(byte[] internalDataFields)
		{
			if(internalDataFields == null) throw new ArgumentNullException(nameof(internalDataFields));

			InternalDataFields = internalDataFields;
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
				throw new ArgumentOutOfRangeException(nameof(index), $"Provided Index: {index} was out of range. Max Index: {InternalDataFields.Length - 1}");
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
				
				//Important to set the next index too so long replication works.
				if (typeof(TValueType) == typeof(ulong) || typeof(TValueType) == typeof(long))
					DataSetIndicationArray.Set(index + 1, true);

				value.Reinterpret(InternalDataFields, index * sizeof(int));
			}
		}

		[JsonConstructor]
		private EntityFieldDataCollection()
		{
			
		}
	}
}
