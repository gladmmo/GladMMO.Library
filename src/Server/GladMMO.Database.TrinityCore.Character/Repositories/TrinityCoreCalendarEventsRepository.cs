using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreCalendarEventsRepository : BaseGenericBackedDatabaseRepository<wotlk_charactersContext, UInt64, CalendarEvents>, ITrinityCalendarEventsRepository
	{
		public TrinityCoreCalendarEventsRepository([JetBrains.Annotations.NotNull] wotlk_charactersContext context) 
			: base(context)
		{

		}
	}
}