using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	public sealed class DatabaseBackedGuildCharacterMembershipRepository : BaseGenericBackedDatabaseRepository<CharacterDatabaseContext, int, CharacterGuildMemberRelationshipModel>, IGuildCharacterMembershipRepository
	{
		public DatabaseBackedGuildCharacterMembershipRepository([JetBrains.Annotations.NotNull] CharacterDatabaseContext context) 
			: base(context)
		{

		}
	}
}
