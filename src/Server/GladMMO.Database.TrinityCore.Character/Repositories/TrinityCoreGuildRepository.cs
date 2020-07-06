using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	public sealed class TrinityCoreGuildRepository : BaseGenericBackedDatabaseRepository<wotlk_charactersContext, uint, Guild>, ITrinityGuildRepository
	{
		public TrinityCoreGuildRepository([JetBrains.Annotations.NotNull] wotlk_charactersContext context) 
			: base(context)
		{

		}

		public async Task<string> RetrieveNameAsync(uint key)
		{
			Guild guild = await Context
				.Guild
				.FindAsync(key);

			return guild.Name;
		}
	}
}
