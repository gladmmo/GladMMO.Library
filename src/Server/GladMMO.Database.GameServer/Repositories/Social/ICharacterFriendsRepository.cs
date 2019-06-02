using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	public interface ICharacterFriendsRepository : IGenericRepositoryCrudable<int, CharacterFriendRelationshipModel>
	{
		/// <summary>
		/// Indicates if the two ids <see cref="characterIdOne"/>
		/// and <see cref="characterIdTwo"/> have a friend relationship.
		/// This can mean pending or blocked.
		/// </summary>
		/// <param name="characterIdOne">The id of the first character.</param>
		/// <param name="characterIdTwo">The id of the second character.</param>
		/// <returns>True of the two characters have a friend relationship.</returns>
		Task<bool> HasFriendRelationship(int characterIdOne, int characterIdTwo);

		/// <summary>
		/// Gets an array of relationship models with pending status
		/// for the character with id <see cref="characterId"/>.
		/// </summary>
		/// <param name="characterId"></param>
		/// <returns></returns>
		Task<CharacterFriendRelationshipModel[]> GetCharactersPendingFriendRequests(int characterId);

		/// <summary>
		/// Gets an array of characterIds that represent
		/// the friend's list of the provided character <see cref="characterId"/>.
		/// </summary>
		/// <param name="characterId"></param>
		/// <returns></returns>
		Task<int[]> GetCharactersFriendsList(int characterId);
	}
}
