using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore;

namespace GladMMO
{
	public interface ITrinityFriendshipRepository : IGenericRepositoryCrudable<uint, CharacterSocial>
	{
		Task<int[]> GetCharactersFriendsList(int characterId);

		Task<bool> IsFriendshipPresentAsync(int characterId, int otherCharacterId);
	}
}
