using System; using FreecraftCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace GladMMO.Database
{
	[Owned]
	public sealed class Vector3<T>
		where T : struct
	{
		[Required]
		public T X { get; private set; }

		[Required]
		public T Y { get; private set; }

		[Required]
		public T Z { get; private set; }

		/// <inheritdoc />
		public Vector3(T x, T y, T z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		protected Vector3()
		{
			
		}
	}
}
