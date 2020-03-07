using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface ILoadedContentResourceHandle
	{
		/// <summary>
		/// Reference-counted in-use count
		/// that representing how many objects are dependent on this
		/// resource being loaded.
		/// </summary>
		int CurrentUseCount { get; }

		/// <summary>
		/// Releases the handle claimed.
		/// </summary>
		void Release();
	}
}
