using System; using FreecraftCore;
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

		/// <summary>
		/// TODO: Doc
		/// </summary>
		/// <param name="container"></param>
		/// <param name="flag"></param>
		public static void AddUnitFieldFlag(this IEntityDataFieldContainer container, UnitFlags flag)
		{
			if(container == null) throw new ArgumentNullException(nameof(container));

			container.SetFieldValue(EUnitFields.UNIT_FIELD_FLAGS, (int)((UnitFlags)container.GetFieldValue<int>(EUnitFields.UNIT_FIELD_FLAGS) | flag));
		}

		/// <summary>
		/// TODO: Doc
		/// </summary>
		/// <param name="container"></param>
		/// <param name="flag"></param>
		public static bool HasBaseObjectFieldFlag(this IEntityDataFieldContainer container, UnitFlags flag)
		{
			if(container == null) throw new ArgumentNullException(nameof(container));

			return ((UnitFlags) container.GetFieldValue<int>(EUnitFields.UNIT_FIELD_FLAGS) & flag) == flag;
		}

		public static bool HasBaseGameObjectFieldFlag(this IEntityDataFieldContainer container, GameObjectFlags flag)
		{
			if(container == null) throw new ArgumentNullException(nameof(container));

			return ((GameObjectFlags)container.GetFieldValue<int>(EGameObjectFields.GAMEOBJECT_FLAGS) & flag) == flag;
		}

		/// <summary>
		/// TODO: Doc
		/// </summary>
		/// <param name="container"></param>
		/// <param name="flag"></param>
		public static void RemoveBaseObjectFieldFlag(this IEntityDataFieldContainer container, UnitFlags flag)
		{
			if(container == null) throw new ArgumentNullException(nameof(container));

			container.SetFieldValue(EUnitFields.UNIT_FIELD_FLAGS, (int)((UnitFlags)container.GetFieldValue<int>(EUnitFields.UNIT_FIELD_FLAGS) & ~flag));
		}

		/// <summary>
		/// Helper extension for setting entity data in <see cref="IEntityDataFieldContainer"/>
		/// based on the int value of a specified Enum value <see cref="index"/>.
		/// </summary>
		/// <typeparam name="TEnumType"></typeparam>
		/// <param name="container"></param>
		/// <param name="index"></param>
		/// <param name="guid"></param>
		public static void SetFieldValue<TEnumType>(this IEntityDataFieldContainer container, TEnumType index, ObjectGuid guid)
			where TEnumType : Enum
		{
			if(container == null) throw new ArgumentNullException(nameof(container));

			container.SetFieldValue(GenericMath.Convert<TEnumType, int>(index), guid.RawGuidValue);
		}

		/// <summary>
		/// Helper extension for setting entity data in <see cref="IEntityDataFieldContainer"/>
		/// based on the int value of a specified Enum value <see cref="index"/>.
		/// </summary>
		/// <param name="container"></param>
		/// <param name="index"></param>
		/// <param name="guid"></param>
		public static void SetFieldValue(this IEntityDataFieldContainer container, int index, ObjectGuid guid)
		{
			if(container == null) throw new ArgumentNullException(nameof(container));

			container.SetFieldValue(index, guid.RawGuidValue);
		}

		//TODO: Doc
		public static TValueType GetFieldValue<TValueType>(this IReadonlyEntityDataFieldContainer container, EGameObjectFields index)
			where TValueType : struct
		{
			if(container == null) throw new ArgumentNullException(nameof(container));

			return container.GetFieldValue<TValueType>((int)index);
		}

		public static TValueType GetFieldValue<TValueType>(this IReadonlyEntityDataFieldContainer container, EUnitFields index)
			where TValueType : struct
		{
			if(container == null) throw new ArgumentNullException(nameof(container));

			return container.GetFieldValue<TValueType>((int)index);
		}

		//TODO: Doc
		public static TValueType GetFieldValue<TValueType>(this IReadonlyEntityDataFieldContainer container, EPlayerFields index)
			where TValueType : struct
		{
			if(container == null) throw new ArgumentNullException(nameof(container));

			return container.GetFieldValue<TValueType>((int)index);
		}

		//TODO: Doc
		public static TValueType GetFieldValue<TValueType>(this IReadonlyEntityDataFieldContainer container, EObjectFields index)
			where TValueType : struct
		{
			if(container == null) throw new ArgumentNullException(nameof(container));

			return container.GetFieldValue<TValueType>((int)index);
		}

		public static TValueType GetEnumFieldValue<TValueType>(this IReadonlyEntityDataFieldContainer container, EGameObjectFields index)
			where TValueType : Enum
		{
			if(container == null) throw new ArgumentNullException(nameof(container));

			return GenericMath.Convert<int, TValueType>(container.GetFieldValue<int>((int)index));
		}

		public static ObjectGuid GetEntityGuidValue(this IReadonlyEntityDataFieldContainer container, EGameObjectFields index)
		{
			if(container == null) throw new ArgumentNullException(nameof(container));

			return new ObjectGuid(container.GetFieldValue<ulong>((int) index));
		}

		public static ObjectGuid GetEntityGuidValue(this IReadonlyEntityDataFieldContainer container, EObjectFields index)
		{
			if(container == null) throw new ArgumentNullException(nameof(container));

			return new ObjectGuid(container.GetFieldValue<ulong>((int)index));
		}

		public static ObjectGuid GetEntityGuidValue(this IReadonlyEntityDataFieldContainer container, EUnitFields index)
		{
			if(container == null) throw new ArgumentNullException(nameof(container));

			return new ObjectGuid(container.GetFieldValue<ulong>((int)index));
		}
	}
}
