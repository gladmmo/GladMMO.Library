using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

namespace GladMMO
{
	public sealed class DesktopInputCameraInputController : ICameraInputController
	{
		public float CurrentHorizontal => Input.GetAxis("Mouse X");

		public float CurrentVertical => Input.GetAxis("Mouse Y");

		public bool isCameraControllerRunning => Input.GetMouseButton((int) MouseButton.RightMouse);
	}
}
