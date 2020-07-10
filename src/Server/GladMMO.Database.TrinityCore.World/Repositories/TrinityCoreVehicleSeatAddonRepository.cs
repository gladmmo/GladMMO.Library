using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCoreVehicleSeatAddonRepository : BaseGenericBackedDatabaseRepository<wotlk_worldContext, UInt32, VehicleSeatAddon>, ITrinityVehicleSeatAddonRepository
	{
		public TrinityCoreVehicleSeatAddonRepository([JetBrains.Annotations.NotNull] wotlk_worldContext context) 
			: base(context)
		{

		}
	}
}