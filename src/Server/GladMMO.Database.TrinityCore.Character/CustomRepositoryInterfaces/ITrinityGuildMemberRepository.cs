using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	public partial interface ITrinityGuildMemberRepository : IGenericRepositoryCrudable<uint, GuildMember>, INameQueryableRepository<uint>, IDisposable
	{
		Task<int[]> GetEntireGuildRosterAsync(int guildId);
	}
}
