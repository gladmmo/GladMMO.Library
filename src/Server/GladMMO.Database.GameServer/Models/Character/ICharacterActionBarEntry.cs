using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface ICharacterActionBarEntry
	{
		ActionBarIndex BarIndex { get; }

		[CanBeNull]
		int? LinkedSpellId { get; }

		FreecraftCore.ActionButtonType ActionType { get; }

		int ActionId { get; }
	}
}
