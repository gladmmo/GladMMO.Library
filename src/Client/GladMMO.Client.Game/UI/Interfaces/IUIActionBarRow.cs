using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public interface IUIActionBarRow : IReadOnlyDictionary<ActionBarIndex, IUIActionBarButton>
	{
		/// <summary>
		/// The starting action bar index for the row.
		/// </summary>
		ActionBarIndex StartIndex { get; }

		/// <summary>
		/// The ending action bar index for the row.
		/// </summary>
		ActionBarIndex EndIndex { get; }
	}
}
