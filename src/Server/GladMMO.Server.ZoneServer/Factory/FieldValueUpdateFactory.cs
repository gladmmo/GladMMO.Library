using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class FieldValueUpdateFactory : IFactoryCreatable<FieldValueUpdate, EntityFieldUpdateCreationContext>
	{
		/// <inheritdoc />
		public FieldValueUpdate Create(EntityFieldUpdateCreationContext context)
		{
			//TODO: This will be a source of contetion. This method will likely be called for N entities meaning lots of allocations and slowdown. We need a better solution
			List<int> changedValues = new List<int>(5); //TODO: What is the best estimate?

			foreach(int changedValueIndex in context.FieldsToUpdateBitArray.EnumerateSetBitsByIndex())
			{
				changedValues.Add(context.DataCollection.GetFieldValue<int>(changedValueIndex));
			}

			//TODO: This design is currently going to fail at MASSIVE scale. We need a copyless, allocationless, threadsafe, lockless and serializable solution. A tall order but a MUST in the future.
			return new FieldValueUpdate(new WireReadyBitArray(context.FieldsToUpdateBitArray.InternalIntegerArray.ToArrayTryAvoidCopy()), changedValues.ToArray());
		}
	}
}
