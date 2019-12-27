using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class WorldTransform
	{
		public float PositionX { get; }

		public float PositionY { get; }

		public float PositionZ { get; }

		public float YAxisRotation { get; }

		public WorldTransform(float x, float y, float z, float yAxisRotation)
		{
			PositionX = x;
			PositionY = y;
			PositionZ = z;
			YAxisRotation = yAxisRotation;
		}
	}
}
