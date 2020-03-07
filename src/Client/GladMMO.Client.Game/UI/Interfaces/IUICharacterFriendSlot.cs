using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public interface IUICharacterFriendSlot : IUIElement, IUIButton, IUIText
	{
		IUIText LevelText { get; }

		IUIText LocationText { get; }
	}
}