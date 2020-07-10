using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreCharactersRepository : BaseGenericBackedDatabaseRepository<wotlk_charactersContext, UInt32, Characters>, ITrinityCharactersRepository
	{
		public TrinityCoreCharactersRepository([JetBrains.Annotations.NotNull] wotlk_charactersContext context) 
			: base(context)
		{

		}
	}
}