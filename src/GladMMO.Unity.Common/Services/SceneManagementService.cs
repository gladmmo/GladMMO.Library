using System; using FreecraftCore;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace GladMMO
{
	public sealed class SceneManagementService : MonoBehaviour, ISceneManager
	{
		[SerializeField]
		private UnityEvent _OnSceneLoaded;

		public event Action OnSceneLoaded
		{
			add
			{
				_OnSceneLoaded.AddListener(new UnityAction(value));
			}
			remove
			{
				//TODO: How should we support removal?
			}
		}

		[SerializeField]
		private UnityEvent _OnBeforeSceneChange;

		public event Action OnBeforeSceneChange
		{
			add
			{
				_OnBeforeSceneChange.AddListener(new UnityAction(value));
			}
			remove
			{
				//TODO: How should we support removal?
			}
		}

		void Awake()
		{
			SceneManager.sceneLoaded += SceneManagerOnSceneLoaded;
		}

		private void SceneManagerOnSceneLoaded(Scene arg0, LoadSceneMode arg1)
		{
			_OnBeforeSceneChange?.Invoke();
		}


		void OnDestroy()
		{
			SceneManager.sceneLoaded -= SceneManagerOnSceneLoaded;
		}

		//We need this overload for firigin in editor with events
		public void LoadLevel(int id)
		{
			LoadLevel(id, LoadSceneMode.Single);
		}

		public void LoadLevel(int id, LoadSceneMode mode)
		{
			SceneManager.LoadSceneAsync(id, mode).completed += operation =>
			{
				_OnBeforeSceneChange?.Invoke();
				operation.allowSceneActivation = true;
			};
		}

		//We need this overload for firigin in editor with events
		public void LoadLevel(string name)
		{
			LoadLevel(name, LoadSceneMode.Single);
		}

		public void LoadLevel(string name, LoadSceneMode mode)
		{
			SceneManager.LoadSceneAsync(name, mode).completed += operation =>
			{
				_OnBeforeSceneChange?.Invoke();
				operation.allowSceneActivation = true;
			};
		}
	}
}
