using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class DatabaseBackedGameObjectModelEntryRepository : BaseCustomContentRepository<GameObjectModelEntryModel>
	{
		public DatabaseBackedGameObjectModelEntryRepository(ContentDatabaseContext databaseContext) 
			: base(databaseContext)
		{

		}
	}
}
