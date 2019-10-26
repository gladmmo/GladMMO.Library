using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	public interface IGuildCharacterMembershipRepository : IGenericRepositoryCrudable<int, CharacterGuildMemberRelationshipModel>
	{
		Task<int[]> GetEntireGuildRosterAsync(int guildId);
	}
}
