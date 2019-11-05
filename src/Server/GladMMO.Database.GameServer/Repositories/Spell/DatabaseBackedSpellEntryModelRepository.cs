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

		public Task<SpellEntryModel[]> RetrieveAllDefaultAsync(bool shouldLoadEffects = false, CancellationToken cancellationToken = default(CancellationToken))
		{
			if(shouldLoadEffects)
				return Context
					.SpellEntries
					.Include(spell => spell.SpellEffectOne)
					.Where(e => e.isDefault)
					.ToArrayAsync(cancellationToken);
			else
				return Context
					.SpellEntries
					.Where(e => e.isDefault)
					.ToArrayAsync(cancellationToken);
		}
	}
}
