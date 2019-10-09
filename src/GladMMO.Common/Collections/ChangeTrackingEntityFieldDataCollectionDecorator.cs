using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Non-generic version.
	/// </summary>
	public sealed class ChangeTrackingEntityFieldDataCollectionDecorator : IEntityDataFieldContainer, IChangeTrackableEntityDataCollection
	{
		/// <summary>
		/// The collection that tracks the dirty changes in values.
		/// </summary>
		public WireReadyBitArray ChangeTrackingArray { get; }

		/// <inheritdoc />
		public object SyncObj { get; } = new object();

		/// <inheritdoc />
		public WireReadyBitArray DataSetIndicationArray => EntityDataCollection.DataSetIndicationArray;

		/// <inheritdoc />
		public bool HasPendingChanges { get; private set; } = false;

		/// <inheritdoc />
		public void ClearTrackedChanges()
		{
			lock (SyncObj)
			{
				//TODO: This is slow and inefficient. We should maintain and memcpy an array of false bits. We may have to do this hundreds/thousands of a minute.
				ChangeTrackingArray.SetAll(false);
				HasPendingChanges = false;
			}
		}

		/// <summary>
		/// The decorated entity data container.
		/// </summary>
		private IEntityDataFieldContainer EntityDataCollection { get; }

		/// <inheritdoc />
		public ChangeTrackingEntityFieldDataCollectionDecorator(IEntityDataFieldContainer entityDataCollection)
		{
			EntityDataCollection = entityDataCollection ?? throw new ArgumentNullException(nameof(entityDataCollection));
			ChangeTrackingArray = new WireReadyBitArray(entityDataCollection.DataSetIndicationArray.Length); //just the size of the initial data indiciation bitarray
		}

		/// <summary>
		/// Overloaded constructor which will actually take an initial <see cref="ChangeTrackingArray"/>.
		/// Useful for initial creation if you want initial values to be viewed as changes.
		/// </summary>
		/// <param name="entityDataCollection"></param>
		/// <param name="initialChangeTrackBitArray"></param>
		public ChangeTrackingEntityFieldDataCollectionDecorator(IEntityDataFieldContainer entityDataCollection, [NotNull] WireReadyBitArray initialChangeTrackBitArray)
		{
			if(initialChangeTrackBitArray == null) throw new ArgumentNullException(nameof(initialChangeTrackBitArray));
			if(initialChangeTrackBitArray.Length != entityDataCollection.DataSetIndicationArray.Length)
				throw new InvalidOperationException($"Cannot set fields in {nameof(ChangeTrackingEntityFieldDataCollectionDecorator)} since provided collections {nameof(entityDataCollection)} and {nameof(initialChangeTrackBitArray)} do not have matching lengths.");

			EntityDataCollection = entityDataCollection ?? throw new ArgumentNullException(nameof(entityDataCollection));
			ChangeTrackingArray = initialChangeTrackBitArray; //just the size of the initial data indiciation bitarray

			//TODO: Technically we can't ASSUME it has any changes, but probably more efficient to than to check.
			HasPendingChanges = true;
		}

		/// <inheritdoc />
		public TValueType GetFieldValue<TValueType>(int index)
			where TValueType : struct
		{
			return EntityDataCollection.GetFieldValue<TValueType>(index);
		}

		/// <inheritdoc />
		public void SetFieldValue<TValueType>(int index, TValueType value)
			where TValueType : struct
		{
			int potentialNewValue = Unsafe.As<TValueType, int>(ref value);

			//We lock here because it's possible that we're in the middle of setting
			//and someone clears HasPendingCHanges since they went through the collection
			//This could cause a race condition between networking coming in and changing entity data
			//and the change tracking/dispatcher not dispatching changes due to being missed or canceled out
			//in this race condition.
			lock(SyncObj)
			{
				//If the values aren't equal we need to set the tracking/dirty stuff
				//Then we also should set the data
				if(!potentialNewValue.Equals(EntityDataCollection.GetFieldValue<int>(index)))
				{
					ChangeTrackingArray.Set(index, true);
					EntityDataCollection.SetFieldValue(index, value);

					//We only have pending changes if the value is not equal
					HasPendingChanges = true;
				}
				else
				{
					//TODO: This kinda exposing an implementation detail because if we started with 0 and setting 0 the above if will fail.
					//The reasoning is if we explictly set 0 then we set the bit because it might not be set
					DataSetIndicationArray.Set(index, true);
				}
			}
		}
	}
}
