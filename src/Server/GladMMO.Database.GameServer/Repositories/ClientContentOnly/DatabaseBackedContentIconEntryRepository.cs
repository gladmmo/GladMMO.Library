using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed class DatabaseBackedContentIconEntryRepository : BaseGenericBackedDatabaseRepository<CharacterDatabaseContext, int, ContentIconEntryModel>, IContentIconEntryModelRepository
	{
		public DatabaseBackedContentIconEntryRepository([JetBrains.Annotations.NotNull] CharacterDatabaseContext context)
			: base(context)
		{

		}
	}
}