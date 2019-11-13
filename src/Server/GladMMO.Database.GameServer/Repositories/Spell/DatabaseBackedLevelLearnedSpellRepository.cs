using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed class DatabaseBackedLevelLearnedSpellRepository : BaseGenericBackedDatabaseRepository<ContentDatabaseContext, int, SpellLevelLearned>, ILevelLearnedSpellRepository
	{
		public DatabaseBackedLevelLearnedSpellRepository(ContentDatabaseContext databaseContext) 
			: base(databaseContext)
		{

		}

		public Task<SpellLevelLearned[]> RetrieveAllAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return Context.LevelLearnedSpells
				.ToArrayAsync(cancellationToken);
		}
	}
}
