using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladMMO
{
	/// <summary>
	/// A quick development implementation of the
	/// <see cref="IGameServersStoreRepository"/> interface.
	/// </summary>
	public sealed class LocalDevelopmentGameServersRepository : IGameServersStoreRepository
	{
		/// <inheritdoc />
		public async Task<GameServerEntry[]> RetriveServers()
		{
			//TODO: Add static domain for endpoint
			return new GameServerEntry[]
			{
				new GameServerEntry(new ResolvedEndpoint(@"http://localhost.", 5003), GameServerStatusFlags.NewServer | GameServerStatusFlags.Online, "DevServer1")
			};
		}
	}
}
