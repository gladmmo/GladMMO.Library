using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed class DatabaseBackedCharacterFriendRepository : BaseGenericBackedDatabaseRepository<CharacterDatabaseContext, int, CharacterFriendModel>, ICharacterFriendRepository
	{
		public DatabaseBackedCharacterFriendRepository([JetBrains.Annotations.NotNull] CharacterDatabaseContext context)
			: base(context)
		{

		}

		public async Task<int[]> GetCharactersFriendsList(int characterId)
		{
			return await Context
				.CharacterFriends
				.Where(f => f.CharacterId == characterId)
				.Select(f => f.FriendCharacterId)
				.ToArrayAsync();
		}

		public async Task<bool> IsFriendshipPresentAsync(int characterId, int friendCharacterId)
		{
			CharacterFriendModel model = await Context.CharacterFriends
				.FindAsync(characterId, friendCharacterId);

			return model != null;
		}
	}
}