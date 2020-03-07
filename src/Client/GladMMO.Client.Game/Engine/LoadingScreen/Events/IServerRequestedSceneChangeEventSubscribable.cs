using System; using FreecraftCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GladMMO
{
	public interface IServerRequestedSceneChangeEventSubscribable
	{
		event EventHandler<ServerRequestedSceneChangeEventArgs> OnServerRequestedSceneChange;
	}

	public sealed class ServerRequestedSceneChangeEventArgs : EventArgs
	{
		public PlayableGameScene SceneRequested { get; }

		/// <inheritdoc />
		public ServerRequestedSceneChangeEventArgs(PlayableGameScene sceneRequested)
		{
			//HelloKitty: We actually aren't going to complain if it's unknown. If the server requests an unknown map, it should be handled somewhere else, not as an exception here.
			SceneRequested = sceneRequested;
		}
	}
}
