using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreGuildMemberRepository : BaseGenericBackedDatabaseRepository<wotlk_charactersContext, uint, GuildMember>, ITrinityGuildMemberRepository
	{
		public async Task<string> RetrieveNameAsync(uint key)
		{
			Guild guild = await Context
				.Guild
				.FindAsync(key);

			return guild.Name;
		}

		public async Task<int[]> GetEntireGuildRosterAsync(int guildId)
		{
			return await Context
				.GuildMember
				.Where(gm => gm.Guildid == guildId)
				.Select(gm => (int)gm.Guid)
				.ToArrayAsync();
		}

		//Sometimes manual handling of the context disposal is required.
		public void Dispose()
		{
			Context.Dispose();
		}
	}
}
