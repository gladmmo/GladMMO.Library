using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreBugreportRepository : BaseGenericBackedDatabaseRepository<wotlk_charactersContext, UInt32, Bugreport>, ITrinityBugreportRepository
	{
		public TrinityCoreBugreportRepository([JetBrains.Annotations.NotNull] wotlk_charactersContext context) 
			: base(context)
		{

		}
	}
}