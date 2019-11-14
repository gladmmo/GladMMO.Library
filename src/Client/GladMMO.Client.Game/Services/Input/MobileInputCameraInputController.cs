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

		public float CurrentVertical => InputDelta.y;

		public TouchPhase CurrentTouchPhase = TouchPhase.Ended;
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
					InputDelta = touch.deltaPosition;
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
