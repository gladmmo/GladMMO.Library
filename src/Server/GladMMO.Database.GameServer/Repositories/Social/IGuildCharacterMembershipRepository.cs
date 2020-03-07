﻿using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	public interface IGuildCharacterMembershipRepository : IGenericRepositoryCrudable<int, CharacterGuildMemberRelationshipModel>, IDisposable
	{
		Task<int[]> GetEntireGuildRosterAsync(int guildId);
	}
}
