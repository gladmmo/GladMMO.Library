using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	public interface ICharacterSessionRepository
		: IGenericRepositoryCrudable<int, CharacterSessionModel>
	{
		/// <summary>
		/// Indicates if the account (not a character or characterid)
		/// has an active on-going session.
		/// </summary>
		/// <param name="accountId">The account id to check.</param>
		/// <returns>True if an active session exists for the account id.</returns>
		Task<bool> AccountHasActiveSession(int accountId);

		/// <summary>
		/// Indicates if the account (not a character or characterid)
		/// has an active on-going session.
		/// </summary>
		/// <param name="characterId">The character id to check.</param>
		/// <returns>True if an active character session exists for the account id.</returns>
		Task<bool> CharacterHasActiveSession(int characterId);

		/// <summary>
		/// Tries to claim an available session with the provided <see cref="characterId"/>
		/// using <see cref="accountId"/> to verify ownership.
		/// </summary>
		/// <param name="accountId"></param>
		/// <param name="characterId"></param>
		/// <returns></returns>
		Task<bool> TryClaimUnclaimedSession(int accountId, int characterId);

		/// <summary>
		/// Attempts to remove/delete the available session that the provided <see cref="characterId"/>
		/// may have.
		/// </summary>
		/// <param name="characterId">The character id to delete the claimed session for.</param>
		/// <returns>True if the claimed session was removed.</returns>
		Task<bool> TryDeleteClaimedSession(int characterId);

		/// <summary>
		/// TODO: Doc
		/// </summary>
		/// <param name="accountId"></param>
		/// <returns></returns>
		Task<ClaimedSessionsModel> RetrieveClaimedSessionByAccountId(int accountId);
	}
}
