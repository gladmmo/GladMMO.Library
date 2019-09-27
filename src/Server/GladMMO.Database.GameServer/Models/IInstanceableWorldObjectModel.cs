using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IInstanceableWorldObjectModel
	{
		/// <summary>
		/// The unique identifier for the instance.
		/// </summary>
		int ObjectInstanceId { get; }
	}
}
