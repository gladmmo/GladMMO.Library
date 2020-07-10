using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreInstanceRepository : BaseGenericBackedDatabaseRepository<wotlk_charactersContext, UInt32, Instance>, ITrinityInstanceRepository
	{
		public TrinityCoreInstanceRepository([JetBrains.Annotations.NotNull] wotlk_charactersContext context) 
			: base(context)
		{

		}
	}
}