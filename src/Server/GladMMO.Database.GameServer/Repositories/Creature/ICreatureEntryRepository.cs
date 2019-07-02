using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	public interface ICreatureEntryRepository : IGenericRepositoryCrudable<int, CreatureEntryModel>
	{
		/// <summary>
		/// Retrieves all <see cref="CreatureEntryModel"/> with the specified <see cref="worldId"/>.
		/// This can be used to get the aggregate of Creatures in a particular zone/map.
		/// (Ex. You want all the Creatures in a city instance, so you query by the instance's mapId so that the zone server
		/// can load each of the Creatures into the city at start up).
		/// </summary>
		/// <param name="worldId">The id of the map to load the Creature instance data for.</param>
		/// <returns>A collection of all Creature entries with the specified <see cref="worldId"/>.</returns>
		Task<IReadOnlyCollection<CreatureEntryModel>> RetrieveAllWorldIdAsync(int worldId);

		//TODO: Doc
		Task<IReadOnlyCollection<CreatureTemplateEntryModel>> RetrieveTemplatesByWorldIdAsync(int worldId);
	}
}
