using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed class DatabaseBackedGuildEntryRepository : BaseGenericBackedDatabaseRepository<CharacterDatabaseContext, int, GuildEntryModel>, IGuildEntryRepository
	{
		public DatabaseBackedGuildEntryRepository([JetBrains.Annotations.NotNull] CharacterDatabaseContext context)
			: base(context)
		{

		}
	}
}