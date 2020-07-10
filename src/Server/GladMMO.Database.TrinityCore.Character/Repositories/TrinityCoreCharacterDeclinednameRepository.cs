using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreCharacterDeclinednameRepository : BaseGenericBackedDatabaseRepository<wotlk_charactersContext, UInt32, CharacterDeclinedname>, ITrinityCharacterDeclinednameRepository
	{
		public TrinityCoreCharacterDeclinednameRepository([JetBrains.Annotations.NotNull] wotlk_charactersContext context) 
			: base(context)
		{

		}
	}
}