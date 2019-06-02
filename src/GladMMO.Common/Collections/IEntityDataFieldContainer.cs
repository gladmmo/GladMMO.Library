using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IEntityDataFieldContainer : IReadonlyEntityDataFieldContainer
	{
		object SyncObj { get; }

		/// <summary>
		/// Bit array that indicates what data fields have been set.
		/// This is not related to the dirty/tracked changes bit array
		/// and only indicates which fields have been set.
		/// </summary>
		WireReadyBitArray DataSetIndicationArray { get; }

		void SetFieldValue<TValueType>(int index, TValueType value)
			where TValueType : struct;
	}

	public interface IEntityDataFieldContainer<in TFieldType> : IReadonlyEntityDataFieldContainer<TFieldType>, IEntityDataFieldContainer
	{
		void SetFieldValue<TValueType>(TFieldType index, TValueType value)
			where TValueType : struct;
	}
}
