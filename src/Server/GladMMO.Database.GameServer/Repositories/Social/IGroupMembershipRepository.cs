using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	public interface IGroupMembershipRepository : IGenericRepositoryCrudable<int, CharacterGroupMembershipModel>
	{
		/// <summary>
		/// Attempts to retrieve the members of a particular group
		/// with the provided id.
		/// </summary>
		/// <param name="groupId">The group id.</param>
		/// <returns></returns>
		Task<CharacterGroupMembershipModel[]> RetrieveAllMembersOfGroup(int groupId);

		/// <summary>
		/// Attempts to retrieve the members of a particular group that
		/// a character is apart of.
		/// </summary>
		/// <param name="characterId">The character id.</param>
		/// <returns></returns>
		Task<CharacterGroupMembershipModel[]> RetrieveAllMembersOfGroupByCharacterId(int characterId);
	}
}
