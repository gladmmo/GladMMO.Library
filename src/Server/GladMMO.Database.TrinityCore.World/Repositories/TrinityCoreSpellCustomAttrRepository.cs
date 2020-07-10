using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreSpellCustomAttrRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, UInt32, SpellCustomAttr>, ITrinitySpellCustomAttrRepository
	{
		public TrinityCoreSpellCustomAttrRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}