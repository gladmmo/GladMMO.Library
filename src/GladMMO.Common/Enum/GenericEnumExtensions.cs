using System;
using System.Collections.Generic;
using System.Text;
using Generic.Math;

namespace GladMMO
{
	public static class GenericEnumExtensions
	{
		public static bool HasAnyFlags<T>(this T enumValue, T flags)
			where T : Enum
		{
			return (GenericMath.Convert<T, int>(enumValue) & GenericMath.Convert<T, int>(flags)) != 0;
		}
	}
}
