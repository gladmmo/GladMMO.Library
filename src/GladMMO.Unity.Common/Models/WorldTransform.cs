using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public sealed class WorldTransform
	{
		public Vector3 Position { get; }

		public float YAxisRotation { get; }

		public WorldTransform(Vector3 position, float yAxisRotation)
		{
			Position = position;
			YAxisRotation = yAxisRotation;
		}
	}
}
