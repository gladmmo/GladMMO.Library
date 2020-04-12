using System; using FreecraftCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GladMMO
{
	public interface IRequestedSceneChangeEventSubscribable
	{
		event EventHandler<RequestedSceneChangeEventArgs> OnRequestedSceneChange;
	}

	public sealed class RequestedSceneChangeEventArgs : EventArgs
	{
		public PlayableGameScene SceneRequested { get; }

		/// <summary>
		/// The map id that will be loaded into the new scene.
		/// </summary>
		public int MapId { get; private set; }

		/// <summary>
		/// Indicates if the scene loading is loading a specific map.
		/// </summary>
		public bool isLoadingSpecificMap => MapId >= 0;

		/// <inheritdoc />
		public RequestedSceneChangeEventArgs(PlayableGameScene sceneRequested)
		{
			if (!Enum.IsDefined(typeof(PlayableGameScene), sceneRequested)) throw new InvalidEnumArgumentException(nameof(sceneRequested), (int) sceneRequested, typeof(PlayableGameScene));

			//HelloKitty: We actually aren't going to complain if it's unknown. If the server requests an unknown map, it should be handled somewhere else, not as an exception here.
			SceneRequested = sceneRequested;
		}

		public RequestedSceneChangeEventArgs(PlayableGameScene sceneRequested, int mapId)
		{
			if (!Enum.IsDefined(typeof(PlayableGameScene), sceneRequested)) throw new InvalidEnumArgumentException(nameof(sceneRequested), (int) sceneRequested, typeof(PlayableGameScene));

			SceneRequested = sceneRequested;
			MapId = mapId;
		}
	}
}
