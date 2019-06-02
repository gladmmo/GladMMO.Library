using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	//This is mostly introduced to reduce write lock contention
	//without skipping it would be possible to enter write locks very often on
	//the global collections policy meaning low concurrency for any packet
	//handler that needs to touch the collections (with read).
	/// <summary>
	/// Contract for tickables that may skip running.
	/// </summary>
	public interface ITickableSkippable
	{
		/// <summary>
		/// Indicates if the tickable can skipped.
		/// (not run this frame/time and passed over).
		/// </summary>
		bool IsTickableSkippable { get; }
	}
}
