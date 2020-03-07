using System; using FreecraftCore;

namespace GladMMO
{
	/// <summary>
	/// Enumeration of scene types for a game initializable.
	/// </summary>
	public enum GameSceneType
	{
		TitleScreen = 0,

		//TODO: Remove this at some point. 
		[Obsolete("ZoneGameScene isn't a thing anymore. In PSOBB there are lobby and there are game. Maybe other types too.")]
		Unusued1 = 1,
		
		CharacterSelection = 2,

		//TODO: Remove this at some point.
		[Obsolete("WorldDownloadingScreen does not exist in PSOBB. See PreZoneBurstingScreen instead.")]
		Unusued2 = 3,

		/// <summary>
		/// The loading screen scene for loading instances.
		/// </summary>
		PreZoneBurstingScreen = 4,

		/// <summary>
		/// The default lobby type for PSOBB.
		/// </summary>
		InstanceServerScene = 5,

		CharacterCreationScreen = 6,
	}
}
