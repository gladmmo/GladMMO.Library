using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IZoneDataRepository : IReadonlyZoneDataRepository
	{
		//The reason we don't use a property setter is due to MS conventions
		//as it would hide a potentially LARGE, complex and far reaching state change (or at least I expect it to).
		/// <summary>
		/// Sets the zone id to the provided <see cref="zoneId"/>
		/// value.
		/// Resets and sets dirty all other data in this repository
		/// if the current <see cref="ZoneId"/> does not match.
		/// </summary>
		/// <param name="zoneId"></param>
		void UpdateZoneId(int zoneId);
	}

	public interface IReadonlyZoneDataRepository
	{
		/// <summary>
		/// The zone id.
		/// </summary>
		int ZoneId { get; }
	}
}
