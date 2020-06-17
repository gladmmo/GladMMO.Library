using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public static class MathWoWExtensions
	{
		public static float ToUnity3DYAxisRotation(this float orientation)
		{
			//See TrinityCore: Position::NormalizeOrientation
			return -((orientation) / (2.0f * (float)Math.PI) * 360.0f) % 360.0f;
		}
	}
}
