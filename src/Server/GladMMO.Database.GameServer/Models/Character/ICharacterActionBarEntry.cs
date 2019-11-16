using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface ICharacterActionBarEntry
	{
		ActionBarIndex BarIndex { get; }

		[CanBeNull]
		int? LinkedSpellId { get; }

		ActionBarIndexType ActionType { get; }
	}
}
