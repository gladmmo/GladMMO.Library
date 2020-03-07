using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	public interface ICharacterFriendRepository : IGenericRepositoryCrudable<int, CharacterFriendModel>
	{
		/// <summary>
		/// Gets an array of characterIds that represent
		/// the friend's list of the provided character <see cref="characterId"/>.
		/// </summary>
		/// <param name="characterId">The character to query friends list for.</param>
		/// <returns>Array of friend characterIds</returns>
		Task<int[]> GetCharactersFriendsList(int characterId);

		Task<bool> IsFriendshipPresentAsync(int characterId, int friendCharacterId);
	}
}
