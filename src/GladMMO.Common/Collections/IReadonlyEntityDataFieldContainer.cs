using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Contract for containers that hold
	/// field values.
	/// </summary>
	public interface IReadonlyEntityDataFieldContainer
	{
		TValueType GetFieldValue<TValueType>(int index)
			where TValueType : struct;
	}

	/// <summary>
	/// Contract for containers that hold
	/// field values.
	/// </summary>
	public interface IReadonlyEntityDataFieldContainer<in TFieldType> : IReadonlyEntityDataFieldContainer
	{
		TValueType GetFieldValue<TValueType>(TFieldType index)
			where TValueType : struct;
	}
}
