using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed class DatabaseBackedAvatarPedestalEntryRepository : GenericDatabaseBackedGameObjectBehaviorEntryRepository<GameObjectAvatarPedestalEntryModel>, IAvatarPedestalGameObjectEntryRepository
	{
		public DatabaseBackedAvatarPedestalEntryRepository([JetBrains.Annotations.NotNull] ContentDatabaseContext context) 
			: base(context)
		{

		}
	}
}
