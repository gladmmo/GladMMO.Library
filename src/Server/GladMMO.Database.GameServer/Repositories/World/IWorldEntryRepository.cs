using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	/// <summary>
	/// Contract for world entry repository types.
	/// </summary>
	public interface IWorldEntryRepository : ICustomContentRepository<WorldEntryModel>
	{
		Task SetWorldValidated(long worldId);
	}
}
