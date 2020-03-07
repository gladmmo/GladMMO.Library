using System; using FreecraftCore;
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
		/// Indicates if the character session should be released after saving.
		/// </summary>
		[JsonProperty]
		public bool ShouldReleaseCharacterSession { get; private set; }

		//TODO: We should use this timestamp somehow to prevent stale character save data overwriting newer data in cases of backend recovery.
		//TODO: Create simple TimeStamp type
		[JsonProperty]
		public long UtcTickTimeStamp { get; private set; }

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
		
		public FullCharacterDataSaveRequest(bool shouldReleaseCharacterSession, bool isPositionSaved, [NotNull] ZoneServerCharacterLocationSaveRequest characterLocationData, [NotNull] EntityFieldDataCollection playerDataSnapshot)
		{
			ShouldReleaseCharacterSession = shouldReleaseCharacterSession;
			this.isPositionSaved = isPositionSaved;
			CharacterLocationData = characterLocationData ?? throw new ArgumentNullException(nameof(characterLocationData));
			PlayerDataSnapshot = playerDataSnapshot ?? throw new ArgumentNullException(nameof(playerDataSnapshot));

			//Snapshot current time.
			UtcTickTimeStamp = DateTime.UtcNow.Ticks;
		}

		[JsonConstructor]
		private FullCharacterDataSaveRequest()
		{
			
		}
	}
}
