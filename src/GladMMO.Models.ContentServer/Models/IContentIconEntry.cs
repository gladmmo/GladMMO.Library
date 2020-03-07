using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IContentIconEntry
	{
		/// <summary>
		/// The unique identifier for the icon entry.
		/// </summary>
		int IconId { get; }

		/// <summary>
		/// The relative path name WITH extension.
		/// </summary>
		[NotNull]
		string IconPathName { get; }
	}
}
