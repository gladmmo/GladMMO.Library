using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public sealed class SceneTypeCreateGladMMO : SceneTypeCreateAttribute
	{
		/// <inheritdoc />
		public SceneTypeCreateGladMMO(GameSceneType sceneType) 
			: base((int)sceneType)
		{

		}
	}
}
