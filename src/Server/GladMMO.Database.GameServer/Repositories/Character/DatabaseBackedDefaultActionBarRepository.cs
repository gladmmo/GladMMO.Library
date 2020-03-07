using System; using FreecraftCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed class DatabaseBackedDefaultActionBarRepository :  ICharacterDefaultActionBarRepository
	{
		private CharacterDatabaseContext Context { get; }

		public DatabaseBackedDefaultActionBarRepository([JetBrains.Annotations.NotNull] CharacterDatabaseContext context)
		{
			Context = context ?? throw new ArgumentNullException(nameof(context));
		}

		public Task<CharacterDefaultActionBarEntry[]> RetrieveAllActionsAsync(EntityPlayerClassType classType)
		{
			if (!Enum.IsDefined(typeof(EntityPlayerClassType), classType)) throw new InvalidEnumArgumentException(nameof(classType), (int) classType, typeof(EntityPlayerClassType));

			return Context.DefaultCharacterActionBars
				.Where(dab => dab.ClassType == classType)
				.ToArrayAsync();
		}
	}
}
