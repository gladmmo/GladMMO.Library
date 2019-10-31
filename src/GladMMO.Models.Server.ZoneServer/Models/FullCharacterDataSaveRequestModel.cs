using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	/// <summary>
	/// Transferable model of entire savable character data.
	/// </summary>
	[JsonObject]
	public sealed class FullCharacterDataSaveRequest
	{
		/// <summary>
		/// Indicates if the position should be saved.
		/// </summary>
		[JsonProperty]
		public bool isPositionSaved { get; private set; }

		//Always sent for simplicity. Reciever may want to do something with it anyway.
		//Even if isPositionSaved is false.
		/// <summary>
		/// The character location data.
		/// </summary>
		[JsonProperty]
		public ZoneServerCharacterLocationSaveRequest CharacterLocationData { get; private set; }

		/// <summary>
		/// The raw entity data field collection snapshot in time when the entity despawned.
		/// </summary>
		[JsonProperty]
		public EntityFieldDataCollection PlayerDataSnapshot { get; private set; }

		
		public FullCharacterDataSaveRequest(bool isPositionSaved, [NotNull] ZoneServerCharacterLocationSaveRequest characterLocationData, [NotNull] EntityFieldDataCollection playerDataSnapshot)
		{
			this.isPositionSaved = isPositionSaved;
			CharacterLocationData = characterLocationData ?? throw new ArgumentNullException(nameof(characterLocationData));
			PlayerDataSnapshot = playerDataSnapshot ?? throw new ArgumentNullException(nameof(playerDataSnapshot));
		}

		[JsonConstructor]
		private FullCharacterDataSaveRequest()
		{
			
		}
	}
}
