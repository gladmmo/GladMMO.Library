using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreCharacterActionRepository : BaseGenericBackedDatabaseRepository<wotlk_charactersContext, uint, CharacterAction>, ITrinityCharacterActionRepository
	{
		public TrinityCoreCharacterActionRepository([JetBrains.Annotations.NotNull] wotlk_charactersContext context) 
			: base(context)
		{

		}
	}
}