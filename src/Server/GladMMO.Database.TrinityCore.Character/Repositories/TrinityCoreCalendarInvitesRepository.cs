using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreCalendarInvitesRepository : BaseGenericBackedDatabaseRepository<wotlk_charactersContext, UInt64, CalendarInvites>, ITrinityCalendarInvitesRepository
	{
		public TrinityCoreCalendarInvitesRepository([JetBrains.Annotations.NotNull] wotlk_charactersContext context) 
			: base(context)
		{

		}
	}
}