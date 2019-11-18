using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IReadonlyActionBarCollection : IReadOnlyCollection<CharacterActionBarInstanceModel>
	{
		/// <summary>
		/// Indicates if an action bar index are set.
		/// AKA non-empty.
		/// </summary>
		/// <param name="index">The actionbar index.</param>
		/// <returns>True if the actionbar index is set.</returns>
		bool IsSet(ActionBarIndex index);
	}

	public interface IActionBarCollection : IReadonlyActionBarCollection
	{
		void Add(CharacterActionBarInstanceModel actionBarModel);
	}
}
