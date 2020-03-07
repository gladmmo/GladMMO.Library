using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class DatabaseBackedCharacterAppearanceRepository : BaseGenericBackedDatabaseRepository<CharacterDatabaseContext, int, CharacterAppearanceModel>, ICharacterAppearanceRepository
	{
		public DatabaseBackedCharacterAppearanceRepository([JetBrains.Annotations.NotNull] CharacterDatabaseContext context) 
			: base(context)
		{

		}
	}
}
