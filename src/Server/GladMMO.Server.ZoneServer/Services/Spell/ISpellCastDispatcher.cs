using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Contract for types that can dispatch a spell cast.
	/// </summary>
	public interface ISpellCastDispatcher
	{
		/// <summary>
		/// Dispatches the provided prepared <see cref="pendingSpellCast"/> data.
		/// </summary>
		/// <param name="pendingSpellCast">The pending spell to cast.</param>
		void DispatchSpellCast(IPendingSpellCastData pendingSpellCast);
	}
}
