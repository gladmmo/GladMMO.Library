using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed class DatabaseBackedGroupMembershipRepository : BaseGenericBackedDatabaseRepository<CharacterDatabaseContext, int, CharacterGroupMembershipModel>, IGroupMembershipRepository
	{
		/// <inheritdoc />
		public DatabaseBackedGroupMembershipRepository([JetBrains.Annotations.NotNull] CharacterDatabaseContext context) 
			: base(context)
		{

		}

		/// <inheritdoc />
		public Task<CharacterGroupMembershipModel[]> RetrieveAllMembersOfGroup(int groupId)
		{
			return Context.GroupMembers
				.Where(gm => gm.GroupId == groupId)
				.ToArrayAsync();
		}

		/// <inheritdoc />
		public async Task<CharacterGroupMembershipModel[]> RetrieveAllMembersOfGroupByCharacterId(int characterId)
		{
			CharacterGroupMembershipModel characterGroupMembershipEntry = await Context.GroupMembers
				.FindAsync(characterId);

			if(characterGroupMembershipEntry == null)
				throw new InvalidOperationException($"Failed to load {nameof(CharacterGroupMembershipModel)} from database for Character: {characterId}");

			return await Context.GroupMembers
				.Where(gm => gm.GroupId == characterGroupMembershipEntry.GroupId)
				.ToArrayAsync();
		}
	}
}
