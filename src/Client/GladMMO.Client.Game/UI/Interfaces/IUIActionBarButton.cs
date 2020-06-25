using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public interface IUIActionBarButton : IUIElement
	{
		IUIButton ActionBarButton { get; }

		IUIImage ActionBarImageIcon { get; }

		/// <summary>
		/// Event published when the mouse over state changes.
		/// Event arg will be TRUE if mouse over.
		/// FALSE if exited.
		/// </summary>
		event EventHandler<bool> OnActionBarMouseOverChanged;
	}
}
