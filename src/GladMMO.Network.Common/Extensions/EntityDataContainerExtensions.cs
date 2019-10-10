using System;
using System.Collections.Generic;
using System.Text;
using Generic.Math;

namespace GladMMO
{
	public static class EntityDataContainerExtensions
	{
		/// <summary>
		/// Helper extension for setting entity data in <see cref="IEntityDataFieldContainer"/>
		/// based on the int value of a specified Enum value <see cref="index"/>.
		/// </summary>
		/// <typeparam name="TEnumType"></typeparam>
		/// <typeparam name="TValueType"></typeparam>
		/// <param name="container"></param>
		/// <param name="index"></param>
		/// <param name="value"></param>
		public static void SetFieldValue<TEnumType, TValueType>(this IEntityDataFieldContainer container, TEnumType index, TValueType value)
			where TValueType : struct
			where TEnumType : Enum
		{
			if (container == null) throw new ArgumentNullException(nameof(container));

			container.SetFieldValue(GenericMath.Convert<TEnumType, int>(index), value);
		}

		//TODO: Doc
		public static TValueType GetFieldValue<TValueType>(this IEntityDataFieldContainer container, GameObjectField index)
			where TValueType : struct
		{
			if(container == null) throw new ArgumentNullException(nameof(container));

			return container.GetFieldValue<TValueType>((int)index);
		}

		public static TValueType GetEnumFieldValue<TValueType>(this IEntityDataFieldContainer container, GameObjectField index)
			where TValueType : Enum
		{
			if(container == null) throw new ArgumentNullException(nameof(container));

			return GenericMath.Convert<int, TValueType>(container.GetFieldValue<int>((int)index));
		}
	}
}
