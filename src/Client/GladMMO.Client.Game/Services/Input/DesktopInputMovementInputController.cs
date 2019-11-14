using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO.Services.Input
{
	public sealed class DesktopInputMovementInputController : IMovementInputController
	{
		public float CurrentHorizontal => UnityEngine.Input.GetAxisRaw("Horizontal");

		public float CurrentVertical => UnityEngine.Input.GetAxisRaw("Vertical");
	}
}
