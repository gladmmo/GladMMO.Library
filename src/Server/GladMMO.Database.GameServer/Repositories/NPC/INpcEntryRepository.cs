using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	public interface INpcEntryRepository : IGenericRepositoryCrudable<int, NPCEntryModel>
	{
		/// <summary>
		/// Retrireves all <see cref="NPCEntryModel"/> with the specified <see cref="mapId"/>.
		/// This can be used to get the aggergate of NPCs in a particular zone/map.
		/// (Ex. You want all the NPCs in a city instance, so you query by the instance's mapId so that the zone server
		/// can load each of the NPCs into the city at start up).
		/// </summary>
		/// <param name="mapId">The id of the map to load the NPC data for.</param>
		/// <returns>A collection of all NPC entries with the specified <see cref="mapId"/>.</returns>
		Task<IReadOnlyCollection<NPCEntryModel>> RetrieveAllWithMapIdAsync(int mapId);
	}
}
