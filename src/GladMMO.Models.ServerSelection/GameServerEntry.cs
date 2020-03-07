using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	/// <summary>
	/// Represents a single entry of a game listing.
	/// </summary>
	[JsonObject]
	public sealed class GameServerEntry
	{
		/// <summary>
		/// The endpoint for the gameserver.
		/// </summary>
		[JsonProperty]
		public ResolvedEndpoint ServerAddress { get; private set; }

		/// <summary>
		/// Informational flags set for the particular game server entry.
		/// </summary>
		[JsonProperty]
		public GameServerStatusFlags Flags { get; private set; }

		/// <summary>
		/// The name of the server.
		/// </summary>
		[JsonProperty]
		public string ServerMoniker { get; private set; }

		//TODO: Expand this with more information if needed by clients.

		/// <inheritdoc />
		public GameServerEntry(ResolvedEndpoint serverAddress, GameServerStatusFlags flags, string serverMoniker)
		{
			if(serverAddress == null) throw new ArgumentNullException(nameof(serverAddress));
			if(string.IsNullOrWhiteSpace(serverMoniker)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(serverMoniker));

			ServerAddress = serverAddress;
			Flags = flags;
			ServerMoniker = serverMoniker;
		}

		//Serializer ctor
		private GameServerEntry()
		{
			
		}
	}
}
