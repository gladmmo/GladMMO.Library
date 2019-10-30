using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GladMMO
{
	/// <summary>
	/// Contract for zone server entry data access.
	/// </summary>
	public interface IZoneServerRepository : IGenericRepositoryCrudable<int, ZoneInstanceEntryModel>
	{
		Task<ZoneInstanceEntryModel> FindFirstWithWorldId(long worldId);

		/// <summary>
		/// Removed all <see cref="ZoneInstanceEntryModel"/> that are expired.
		/// AKA haven't updated their checkin value in awhile.
		/// </summary>
		/// <returns>Awaitable.</returns>
		Task CleanupExpiredZonesAsync(CancellationToken cancellationToken = default(CancellationToken));
	}
}
