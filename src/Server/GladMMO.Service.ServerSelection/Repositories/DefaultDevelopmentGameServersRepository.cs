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
	public sealed class DefaultDevelopmentGameServersRepository : IGameServersStoreRepository
	{
		/// <inheritdoc />
		public async Task<GameServerEntry[]> RetriveServers()
		{
			//TODO: Add static domain for endpoint
			return new GameServerEntry[]
			{
				new GameServerEntry(new ResolvedEndpoint(@"http://Guardiansservicediscovery-DevServer1.us-east-2.elasticbeanstalk.com", 80), GameServerStatusFlags.NewServer | GameServerStatusFlags.Online, "DevServer1")
			};
		}
	}
}
