using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed class DatabaseBackedCharacterFriendsRepository : ICharacterFriendsRepository
	{
		private GeneralGenericCrudRepositoryProvider<int, CharacterFriendRelationshipModel> GenericRepository { get; }

		private CharacterDatabaseContext Context { get; }

		/// <inheritdoc />
		public DatabaseBackedCharacterFriendsRepository([JetBrains.Annotations.NotNull] CharacterDatabaseContext context)
		{
			Context = context ?? throw new ArgumentNullException(nameof(context));

			GenericRepository = new GeneralGenericCrudRepositoryProvider<int, CharacterFriendRelationshipModel>(context.CharacterFriendRequests, context);
		}

		/// <inheritdoc />
		public Task<bool> ContainsAsync(int key)
		{
			return GenericRepository.ContainsAsync(key);
		}

		/// <inheritdoc />
		public Task<bool> TryCreateAsync(CharacterFriendRelationshipModel model)
		{
			return GenericRepository.TryCreateAsync(model);
		}

		/// <inheritdoc />
		public Task<CharacterFriendRelationshipModel> RetrieveAsync(int key, bool includeNavigationProperties = false)
		{
			return GenericRepository.RetrieveAsync(key, includeNavigationProperties);
		}

		/// <inheritdoc />
		public Task<bool> TryDeleteAsync(int key)
		{
			return GenericRepository.TryDeleteAsync(key);
		}

		/// <inheritdoc />
		public Task UpdateAsync(int key, CharacterFriendRelationshipModel model)
		{
			return GenericRepository.UpdateAsync(key, model);
		}

		/// <inheritdoc />
		public Task<bool> HasFriendRelationship(int characterIdOne, int characterIdTwo)
		{
			long directionalUniquenessValue = CharacterFriendRelationshipModel.ComputeDirectionalUniquenessIndex(characterIdOne, characterIdTwo);

			//A friend relation exists if there is a uniqueness entry.
			return Context.CharacterFriendRequests
				.AnyAsync(cfr => cfr.DirectionalUniqueness == directionalUniquenessValue);
		}

		/// <inheritdoc />
		public async Task<CharacterFriendRelationshipModel[]> GetCharactersPendingFriendRequests(int characterId)
		{
			//This gets all incoming and outgoing friend requests
			//which can help users cancel since they'll see their own pending friend requests
			return await Context.CharacterFriendRequests
				.Where(cfr => (cfr.RequestingCharacterId == characterId || cfr.TargetRequestCharacterId == characterId) && cfr.RelationState == FriendshipRelationshipState.Pending)
				.ToArrayAsync();
		}

		/// <inheritdoc />
		public async Task<int[]> GetCharactersFriendsList(int characterId)
		{
			//We get the models first, when we can generate the friends list ids.
			CharacterFriendRelationshipModel[] models = await Context.CharacterFriendRequests
				.Where(cfr => (cfr.RequestingCharacterId == characterId || cfr.TargetRequestCharacterId == characterId) && cfr.RelationState == FriendshipRelationshipState.Accepted)
				.ToArrayAsync();

			return models
				.Select(cfr =>
				{
					//We need to get the character ids that aren't the characterid of the requested
					//player.
					if(cfr.TargetRequestCharacterId == characterId)
						return cfr.RequestingCharacterId;

					return cfr.TargetRequestCharacterId;
				})
				.ToArray();
		}
	}
}
