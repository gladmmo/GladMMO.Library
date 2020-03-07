using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	/// <summary>
	/// The response model for the gameserver.
	/// </summary>
	[JsonObject]
	public sealed class GameServerListResponseModel
	{
		/// <summary>
		/// The game server entries.
		/// </summary>
		[JsonProperty(PropertyName = "Entries")]
		private GameServerEntry[] _Entries { get; set; }

		/// <summary>
		/// The entries of all gameservers.
		/// </summary>
		[JsonIgnore]
		public IReadOnlyCollection<GameServerEntry> Entries => _Entries;

		/// <inheritdoc />
		public GameServerListResponseModel(GameServerEntry[] entries)
		{
			if(entries == null) throw new ArgumentNullException(nameof(entries));

			_Entries = entries;
		}

		//serializer ctor
		private GameServerListResponseModel()
		{
			
		}
	}
}
