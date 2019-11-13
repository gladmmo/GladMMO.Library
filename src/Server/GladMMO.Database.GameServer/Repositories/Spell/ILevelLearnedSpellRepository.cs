using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GladMMO
{
	public interface ILevelLearnedSpellRepository : IGenericRepositoryCrudable<int, SpellLevelLearned>
	{
		Task<SpellLevelLearned[]> RetrieveAllAsync(CancellationToken cancellationToken = default(CancellationToken));

		Task<SpellLevelLearned[]> RetrieveAllAsync(EntityPlayerClassType classType, CancellationToken cancellationToken = default(CancellationToken));

		Task<SpellLevelLearned[]> RetrieveAllAsync(EntityPlayerClassType classType, int level, CancellationToken cancellationToken = default(CancellationToken));
	}
}
