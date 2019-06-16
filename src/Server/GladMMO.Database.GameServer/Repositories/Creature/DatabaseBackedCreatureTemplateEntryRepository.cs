using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed class DatabaseBackedCreatureTemplateEntryRepository : ICreatureTemplateRepository
	{
		private ContentDatabaseContext Context { get; }

		private IGenericRepositoryCrudable<int, CreatureTemplateEntryModel> GenericRepository { get; }

		public DatabaseBackedCreatureTemplateEntryRepository([NotNull] ContentDatabaseContext context)
		{
			if (context == null) throw new ArgumentNullException(nameof(context));
			Context = context;

			GenericRepository = new GeneralGenericCrudRepositoryProvider<int, CreatureTemplateEntryModel>(context.Set<CreatureTemplateEntryModel>(), context);
		}

		public Task<bool> ContainsAsync(int key)
		{
			return GenericRepository.ContainsAsync(key);
		}

		public Task<bool> TryCreateAsync(CreatureTemplateEntryModel model)
		{
			return GenericRepository.TryCreateAsync(model);
		}

		public Task<CreatureTemplateEntryModel> RetrieveAsync(int key)
		{
			return GenericRepository.RetrieveAsync(key);
		}

		public Task<bool> TryDeleteAsync(int key)
		{
			return GenericRepository.TryDeleteAsync(key);
		}

		public Task UpdateAsync(int key, CreatureTemplateEntryModel model)
		{
			return GenericRepository.UpdateAsync(key, model);
		}

		public async Task<string> RetrieveNameAsync(int key)
		{
			CreatureTemplateEntryModel findAsync = await Context.CreatureTemplates
				.FindAsync(key);

			return findAsync.CreatureName;
		}
	}
}
