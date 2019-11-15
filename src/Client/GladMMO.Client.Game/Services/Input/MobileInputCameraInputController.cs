using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	public sealed class MobileInputCameraInputController : ICameraInputController, IGameTickable
	{
		public Vector2 InputDelta { get; private set; } = Vector2.zero;

		public float CurrentHorizontal => InputDelta.x;

		//Mobile shouldn't be able to rotate Vertical. Just side to side.
		public float CurrentVertical => 0;

		public TouchPhase CurrentTouchPhase = TouchPhase.Ended;

		public float LookSpeed => 1;

		public void Tick()
		{
			Touch touch = Input.GetTouch(0);
			CurrentTouchPhase = touch.phase;

			switch (touch.phase)
			{
				case TouchPhase.Began:
					InputDelta = Vector2.zero;
					break;
				case TouchPhase.Moved:
					InputDelta = 360 * touch.deltaPosition * (1 / (float)Screen.currentResolution.width);
					break;
				case TouchPhase.Stationary:
					InputDelta = Vector2.zero;
					break;
				case TouchPhase.Ended:
					InputDelta = Vector2.zero;
					break;
				case TouchPhase.Canceled:
					InputDelta = Vector2.zero;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public bool isCameraControllerRunning => CurrentTouchPhase == TouchPhase.Moved;
	}
}
