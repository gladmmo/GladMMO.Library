using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IEntityDataChangeCallbackRegisterable
	{
		/// <summary>
		/// Registers a callback for when a value changes for <see cref="entity"/>.
		/// The value being tracked is <see cref="dataField"/>.
		/// The callback to invoke is <see cref="callback"/>.
		/// </summary>
		/// <typeparam name="TCallbackValueCastType">The type of the value. (Ex. Expecting the data at index <see cref="dataField"/> to be cast to Float before dispatching).</typeparam>
		/// <param name="entity"></param>
		/// <param name="dataField"></param>
		/// <param name="callback"></param>
		IEntityDataEventUnregisterable RegisterCallback<TCallbackValueCastType>(NetworkEntityGuid entity, int dataField, Action<NetworkEntityGuid, EntityDataChangedArgs<TCallbackValueCastType>> callback)
			where TCallbackValueCastType : struct;

		//TODO: Add unregisteration for callbacks
	}
}
