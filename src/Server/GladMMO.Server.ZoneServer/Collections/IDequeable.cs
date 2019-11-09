using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IDequeable<out T>
	{
		/// <summary>
		/// Indicates if the dequeable is empty.
		/// </summary>
		bool isEmpty { get; }

		void Clear();
	}
}
