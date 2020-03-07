﻿using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface ICameraInputController : IAxisInputController
	{
		bool isCameraControllerRunning { get; }

		float LookSpeed { get; }
	}
}
