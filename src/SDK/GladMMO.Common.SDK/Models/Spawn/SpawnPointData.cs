using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public sealed class SpawnPointData
	{
		/// <summary>
		/// The world position of the spawn data.
		/// </summary>
		public Vector3 WorldPosition { get; }

		/// <summary>
		/// The world rotation of the spawn data.
		/// </summary>
		public Quaternion WorldRotation { get; }

		public SpawnPointData(Vector3 worldPosition, Quaternion worldRotation)
		{
			WorldPosition = worldPosition;
			WorldRotation = worldRotation;
		}
	}
}
