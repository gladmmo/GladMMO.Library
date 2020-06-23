using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class WorldTransform
	{
		public float PositionX { get; set; }

		public float PositionY { get; set; }

		public float PositionZ { get; set; }

		public float YAxisRotation { get; set; }

		public WorldTransform(float x, float y, float z, float yAxisRotation)
		{
			PositionX = x;
			PositionY = y;
			PositionZ = z;
			YAxisRotation = yAxisRotation;
		}
	}
}
