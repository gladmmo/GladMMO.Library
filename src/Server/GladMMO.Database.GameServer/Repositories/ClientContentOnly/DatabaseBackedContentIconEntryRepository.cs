using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed class DatabaseBackedContentIconEntryRepository : BaseGenericBackedDatabaseRepository<ContentDatabaseContext, int, ContentIconEntryModel>, IContentIconEntryModelRepository
	{
		public DatabaseBackedContentIconEntryRepository([JetBrains.Annotations.NotNull] ContentDatabaseContext context)
			: base(context)
		{

		}

		public Task<ContentIconEntryModel[]> RetrieveAllAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return Context.ContentIcons
				.ToArrayAsync(cancellationToken);
		}
	}
}