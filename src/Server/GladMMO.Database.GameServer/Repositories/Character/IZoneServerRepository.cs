using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	/// <summary>
	/// Contract for zone server entry data access.
	/// </summary>
	public interface IZoneServerRepository : IGenericRepositoryCrudable<int, ZoneInstanceEntryModel>
	{

	}
}
