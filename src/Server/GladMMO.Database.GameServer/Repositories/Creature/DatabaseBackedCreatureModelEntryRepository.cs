using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class DatabaseBackedCreatureModelEntryRepository : BaseCustomContentRepository<CreatureModelEntryModel>
	{
		public DatabaseBackedCreatureModelEntryRepository(ContentDatabaseContext databaseContext) 
			: base(databaseContext)
		{

		}
	}
}
