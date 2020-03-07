using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed class DatabaseBackedGroupEntryRepository : IGroupEntryRepository
	{
		private IGenericRepositoryCrudable<int, CharacterGroupEntryModel> GenericBackedRepository { get; }

		private CharacterDatabaseContext Context { get; }

		/// <inheritdoc />
		public DatabaseBackedGroupEntryRepository([JetBrains.Annotations.NotNull] CharacterDatabaseContext context)
		{
			Context = context ?? throw new ArgumentNullException(nameof(context));
			GenericBackedRepository = new GeneralGenericCrudRepositoryProvider<int, CharacterGroupEntryModel>(context.Groups, context);
		}

		/// <inheritdoc />
		public Task<bool> ContainsAsync(int key)
		{
			return GenericBackedRepository.ContainsAsync(key);
		}

		/// <inheritdoc />
		public Task<bool> TryCreateAsync(CharacterGroupEntryModel model)
		{
			return GenericBackedRepository.TryCreateAsync(model);
		}

		/// <inheritdoc />
		public Task<CharacterGroupEntryModel> RetrieveAsync(int key, bool includeNavigationProperties = false)
		{
			return GenericBackedRepository.RetrieveAsync(key, includeNavigationProperties);
		}

		/// <inheritdoc />
		public Task<bool> TryDeleteAsync(int key)
		{
			return GenericBackedRepository.TryDeleteAsync(key);
		}

		/// <inheritdoc />
		public Task UpdateAsync(int key, CharacterGroupEntryModel model)
		{
			return GenericBackedRepository.UpdateAsync(key, model);
		}

		/// <inheritdoc />
		public Task<bool> ContainsGroupWithLeader(int characterId)
		{
			return Context.Groups
				.AnyAsync(g => g.LeaderCharacterId == characterId);
		}

		/// <inheritdoc />
		public async Task<CharacterGroupEntryModel> RetrieveByLeader(int characterId)
		{
			if(!await ContainsGroupWithLeader(characterId).ConfigureAwaitFalse())
				throw new InvalidOperationException($"Tried to retrieve {nameof(CharacterGroupEntryModel)} of Leader: {characterId} but none exists.");

			return await Context.Groups
				.FirstAsync(g => g.LeaderCharacterId == characterId)
				.ConfigureAwaitFalse();
		}
	}
}
