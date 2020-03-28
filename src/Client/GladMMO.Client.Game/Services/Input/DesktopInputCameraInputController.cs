using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;

namespace GladMMO
{
	public sealed class DesktopInputCameraInputController : ICameraInputController
	{
		public float CurrentHorizontal => Input.GetAxis("Mouse X");

		public float CurrentVertical => Input.GetAxis("Mouse Y");

		public bool isCameraControllerRunning => Input.GetMouseButton((int) MouseButton.RightMouse);

		public float LookSpeed { get; } = 3;
	}
}
