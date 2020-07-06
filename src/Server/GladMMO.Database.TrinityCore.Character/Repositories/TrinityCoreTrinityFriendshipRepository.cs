using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed class TrinityCoreTrinityFriendshipRepository : BaseGenericBackedDatabaseRepository<wotlk_charactersContext, uint, CharacterSocial>, ITrinityFriendshipRepository
	{
		public TrinityCoreTrinityFriendshipRepository([JetBrains.Annotations.NotNull] wotlk_charactersContext context) 
			: base(context)
		{

		}

		public async Task<int[]> GetCharactersFriendsList(int characterId)
		{
			return await Context
				.CharacterSocial
				.Where(f => f.Guid == characterId && (f.Flags & 0x01) != 0) //TODO: Use SocialFlag enum
				.Select(f => (int)f.Friend)
				.ToArrayAsync();
		}

		public async Task<bool> IsFriendshipPresentAsync(int characterId, int otherCharacterId)
		{
			return await Context
				.CharacterSocial
				.AnyAsync(f => f.Guid == characterId && f.Friend == otherCharacterId);
		}
	}
}
