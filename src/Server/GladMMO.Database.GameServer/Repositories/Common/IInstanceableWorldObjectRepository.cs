using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	public interface IInstanceableWorldObjectRepository<TInstanceModelType>
	{
		/// <summary>
		/// Retrieves all <see cref="TInstanceModelType"/> with the specified <see cref="worldId"/>.
		/// This can be used to get the aggregate of Instanceable Objects in a particular zone/map.
		/// (Ex. You want all the Objects of this type in a city instance, so you query by the instance's mapId so that the zone server
		/// can load each of the Objects into the city at start up).
		/// </summary>
		/// <param name="worldId">The id of the map to load the Object instance data for.</param>
		/// <returns>A collection of all Object entries with the specified <see cref="worldId"/>.</returns>
		Task<IReadOnlyCollection<TInstanceModelType>> RetrieveAllWorldIdAsync(int worldId);
	}
}
