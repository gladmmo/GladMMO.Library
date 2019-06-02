using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	public interface IGroupEntryRepository : IGenericRepositoryCrudable<int, CharacterGroupEntryModel>
	{
		/// <summary>
		/// Indicates if a character is the leader with <see cref="characterId"/>
		/// is the leader of a group.
		/// </summary>
		/// <param name="characterId"></param>
		/// <returns></returns>
		Task<bool> ContainsGroupWithLeader(int characterId);

		/// <summary>
		/// Attempts to retrieve an entry that has a leader
		/// with the associated <see cref="characterId"/>
		/// </summary>
		/// <param name="characterId"></param>
		/// <returns></returns>
		Task<CharacterGroupEntryModel> RetrieveByLeader(int characterId);
	}
}
