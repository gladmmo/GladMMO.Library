using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladMMO
{
	/// <summary>
	/// Contract for builder that can produce paths to
	/// the region config files.
	/// </summary>
	public interface IRegionalServiceFilePathBuilder
	{
		/// <summary>
		/// Builds a relative path for the specified <see cref="region"/>.
		/// </summary>
		/// <param name="region">The region.</param>
		/// <returns>Relative path to the file.</returns>
		string BuildPath(ClientRegionLocale region);
	}
}
