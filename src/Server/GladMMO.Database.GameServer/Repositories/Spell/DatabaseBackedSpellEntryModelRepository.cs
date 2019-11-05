using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed class DatabaseBackedSpellEntryModelRepository : BaseGenericBackedDatabaseRepository<ContentDatabaseContext, int, SpellEntryModel>, ISpellEntryRepository
	{
		public DatabaseBackedSpellEntryModelRepository(ContentDatabaseContext databaseContext) 
			: base(databaseContext)
		{

		}

		public Task<SpellEntryModel[]> RetrieveAllDefaultAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return Context
				.SpellEntries
				.Where(e => e.isDefault)
				.ToArrayAsync(cancellationToken);
		}
	}
}
