using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed class DatabaseBackedWorldTeleporterEntryRepository : GenericDatabaseBackedGameObjectBehaviorEntryRepository<GameObjectWorldTeleporterEntryModel>, IWorldTeleporterGameObjectEntryRepository
	{
		public DatabaseBackedWorldTeleporterEntryRepository([JetBrains.Annotations.NotNull] ContentDatabaseContext context) 
			: base(context)
		{

		}
	}
}
