using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GladMMO
{
	/// <summary>
	/// Component that grabs the main camera in the scene
	/// and snaps it to the current position of the <see cref="Transform"/>.
	/// Also makes it a child of the object this <see cref="Component"/> is attached to.
	/// </summary>
	public sealed class LocalCameraGrabber : MonoBehaviour
	{
		void Awake()
		{
			//This zeros the camera out
			Camera.main.gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);

			Camera.main.gameObject.transform.parent = this.transform;
			Camera.main.gameObject.transform.localPosition = Vector3.zero;
			Camera.main.enabled = true;
		}
	}
}
