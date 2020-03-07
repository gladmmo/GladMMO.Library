using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using UnityEngine.SceneManagement;

namespace GladMMO
{
	public interface ISceneManager
	{
		event Action OnSceneLoaded;

		event Action OnBeforeSceneChange;

		void LoadLevel(int id, LoadSceneMode mode);

		void LoadLevel(string name, LoadSceneMode mode);
	}
}
