using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IUIView
	{
		/// <summary>
		/// Indicates if the view is enabled.
		/// </summary>
		bool IsEnabled { get; }
	}
}
