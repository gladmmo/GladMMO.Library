using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	public sealed class DatabaseBackedGuildCharacterMembershipRepository : IGuildCharacterMembershipRepository
	{
		private IGenericRepositoryCrudable<int, CharacterGuildMemberRelationshipModel> GenericGuildRelationshipRepository { get; }

		public DatabaseBackedGuildCharacterMembershipRepository(CharacterDatabaseContext context)
		{
			GenericGuildRelationshipRepository = new GeneralGenericCrudRepositoryProvider<int, CharacterGuildMemberRelationshipModel>(context.GuildMembers, context);
		}

		/// <inheritdoc />
		public Task<bool> ContainsAsync(int key)
		{
			return GenericGuildRelationshipRepository.ContainsAsync(key);
		}

		/// <inheritdoc />
		public Task<bool> TryCreateAsync(CharacterGuildMemberRelationshipModel model)
		{
			return GenericGuildRelationshipRepository.TryCreateAsync(model);
		}

		/// <inheritdoc />
		public Task<CharacterGuildMemberRelationshipModel> RetrieveAsync(int key, bool includeNavigationProperties = false)
		{
			return GenericGuildRelationshipRepository.RetrieveAsync(key, includeNavigationProperties);
		}

		/// <inheritdoc />
		public Task<bool> TryDeleteAsync(int key)
		{
			return GenericGuildRelationshipRepository.TryDeleteAsync(key);
		}

		/// <inheritdoc />
		public Task UpdateAsync(int key, CharacterGuildMemberRelationshipModel model)
		{
			return GenericGuildRelationshipRepository.UpdateAsync(key, model);
		}
	}
}
