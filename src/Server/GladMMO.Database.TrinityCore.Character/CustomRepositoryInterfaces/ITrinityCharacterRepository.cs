using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	/// <summary>
	/// TrinityCore version of <see cref="ICharacterRepository"/>.
	/// </summary>
	public partial interface ITrinityCharactersRepository : 
		IGenericRepositoryCrudable<uint, Characters>, INameQueryableRepository<uint>
	{
		//TODO: Doc
		Task<Characters> RetrieveAsync(string characterName);

		/// <summary>
		/// Checks if the repository contains a model with the specified charactername.
		/// </summary>
		/// <param name="characterName">The character name to check.</param>
		/// <returns>True if the name is taken.</returns>
		Task<bool> ContainsAsync(string characterName);

		/// <summary>
		/// Tries to load all the characters with the provided <see cref="accountId"/>.
		/// If none exist it will produce an empty collection.
		/// </summary>
		/// <param name="accountId">The account id to check.</param>
		/// <returns></returns>
		Task<int[]> CharacterIdsForAccountId(int accountId);

		/// <summary>
		/// Indicates if the account (not a character or characterid)
		/// has an active on-going session.
		/// </summary>
		/// <param name="accountId">The account id to check.</param>
		/// <returns>True if an active session exists for the account id.</returns>
		Task<bool> AccountHasActiveSession(int accountId);

		/// <summary>
		/// TODO: Doc
		/// </summary>
		/// <param name="accountId"></param>
		/// <returns></returns>
		Task<Characters> RetrieveClaimedSessionByAccountId(int accountId);
	}
}
