using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GladMMO
{
	public interface ISpellEntryRepository : IGenericRepositoryCrudable<int, SpellEntryModel>
	{
		Task<SpellEntryModel[]> RetrieveAllDefaultAsync(bool shouldLoadEffects = false, CancellationToken cancellationToken = default(CancellationToken));
	}
}
