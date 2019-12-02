using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public interface IUIActionBarButton : IUIElement
	{
		IUIButton ActionBarButton { get; }

		IUIImage ActionBarImageIcon { get; }
	}
}
