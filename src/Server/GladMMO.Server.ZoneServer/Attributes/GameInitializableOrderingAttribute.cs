using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class GameInitializableOrderingAttribute : Attribute
	{
		public static int DefaultOrderValue { get; } = 0;

		/// <summary>
		/// The ordering of the initializable..
		/// </summary>
		public int Order { get; }

		/// <inheritdoc />
		public GameInitializableOrderingAttribute(int order)
		{
			if(order < 0) throw new ArgumentOutOfRangeException(nameof(order));

			Order = order;
		}
	}
}
