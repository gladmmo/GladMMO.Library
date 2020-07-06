using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	public interface ITrinityGuildMembershipRepository : IGenericRepositoryCrudable<uint, GuildMember>, INameQueryableRepository<uint>, IDisposable
	{
		Task<int[]> GetEntireGuildRosterAsync(int guildId);
	}
}
