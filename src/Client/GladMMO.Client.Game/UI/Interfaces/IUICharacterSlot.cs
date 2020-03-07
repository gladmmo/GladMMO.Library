using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public interface IUICharacterSlot : IUIElement, IUIToggle, IUIText
	{
		IUIText LevelText { get; }

		IUIText LocationText { get; }
	}
}
