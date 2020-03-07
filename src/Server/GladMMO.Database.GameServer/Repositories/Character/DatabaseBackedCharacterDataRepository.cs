using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class DatabaseBackedCharacterDataRepository : BaseGenericBackedDatabaseRepository<CharacterDatabaseContext, int, CharacterDataModel>, ICharacterDataRepository
	{
		public DatabaseBackedCharacterDataRepository([JetBrains.Annotations.NotNull] CharacterDatabaseContext context) 
			: base(context)
		{

		}
	}
}
