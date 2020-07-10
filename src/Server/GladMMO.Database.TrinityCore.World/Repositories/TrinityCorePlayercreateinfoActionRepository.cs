using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCorePlayercreateinfoActionRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, Byte, PlayercreateinfoAction>, ITrinityPlayercreateinfoActionRepository
	{
		public TrinityCorePlayercreateinfoActionRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}