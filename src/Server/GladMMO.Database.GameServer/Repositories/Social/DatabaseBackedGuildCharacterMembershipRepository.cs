using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed class DatabaseBackedGuildCharacterMembershipRepository : BaseGenericBackedDatabaseRepository<CharacterDatabaseContext, int, CharacterGuildMemberRelationshipModel>, IGuildCharacterMembershipRepository
	{
		public DatabaseBackedGuildCharacterMembershipRepository([JetBrains.Annotations.NotNull] CharacterDatabaseContext context) 
			: base(context)
		{

		}

		public Task<int[]> GetEntireGuildRosterAsync(int guildId)
		{
			if (guildId <= 0) throw new ArgumentOutOfRangeException(nameof(guildId));

			return Context.GuildMembers
				.Where(gm => gm.GuildId == guildId)
				.Select(gm => gm.CharacterId)
				.ToArrayAsync();
		}
	}
}
