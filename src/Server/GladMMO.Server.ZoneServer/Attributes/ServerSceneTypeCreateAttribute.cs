using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public sealed class ServerSceneTypeCreateAttribute : SceneTypeCreateAttribute
	{
		/// <inheritdoc />
		public ServerSceneTypeCreateAttribute(ServerSceneType sceneType)
			: base((int)sceneType)
		{

		}
	}
}