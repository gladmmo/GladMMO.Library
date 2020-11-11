using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public static class GladMMOClientConstants
	{
		public const string INSTANCE_SERVER_SCENE_NAME = "InstanceServerScene";

		public const string CHARACTER_SELECTION_SCENE_NAME = "CharacterSelection";

		public const string WORLD_DOWNLOAD_SCENE_NAME = "PreLobbyLoadScreen";

		public const string CHARACTER_CREATION_SCENE_NAME = "CharacterCreation";

		public const string TITLE_SCREEN_NAME = "MainTitleScreen";

		public const string SHARED_SCENE_NAME = "SharedScene";

		/// <summary>
		/// Controls/indicates the mode for the client.
		/// </summary>
		public const ClientGameMode CLIENT_MODE = ClientGameMode.Default;
	}
}
