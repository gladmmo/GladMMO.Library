using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladMMO
{
	public interface IContentDownloadAuthroizationValidator
	{
		/// <summary>
		/// Indicates if a provided user with the id <see cref="userId"/>
		/// has authorization to access the content related to a world
		/// for downloading.
		/// </summary>
		/// <param name="userId">The id of the user.</param>
		/// <param name="worldId">The id of the world to check access for.</param>
		/// <returns>True if the user is authorized to download the content.</returns>
		Task<bool> CanUserAccessWorldContet(int userId, long worldId);
	}
}
