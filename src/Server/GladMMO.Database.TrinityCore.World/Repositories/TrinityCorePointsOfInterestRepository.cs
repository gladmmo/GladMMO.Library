using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCorePointsOfInterestRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, UInt32, PointsOfInterest>, ITrinityPointsOfInterestRepository
	{
		public TrinityCorePointsOfInterestRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}