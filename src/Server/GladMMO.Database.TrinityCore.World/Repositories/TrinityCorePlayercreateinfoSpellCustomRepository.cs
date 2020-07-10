using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCorePlayercreateinfoSpellCustomRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, UInt32, PlayercreateinfoSpellCustom>, ITrinityPlayercreateinfoSpellCustomRepository
	{
		public TrinityCorePlayercreateinfoSpellCustomRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}