using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladMMO;
using UnityEngine;

namespace GladMMO
{
	public abstract class StaticSpawnPointDefinition : GladMMOSDKMonoBehaviour, ISpawnPointStrategy
	{
		private Vector3 CachedSpawnPosition;

		private Quaternion CachedSpawnRotation;

		public abstract EntitySpawnType EntityType { get; }

		void Awake()
		{
			CachedSpawnPosition = transform.position;
			CachedSpawnRotation = transform.rotation;
		}

		//TODO: IFDEF this out for deployment builds
		private void OnDrawGizmos()
		{
			Color originalColor = Gizmos.color;

			Gizmos.color = Color.cyan;
			Gizmos.DrawSphere(transform.position, 0.1f);
			Gizmos.DrawLine(transform.position, transform.position + (transform.forward * 0.3f));
			Gizmos.color = originalColor;
		}

		[Button]
		private void StickSpawnPointToCollider()
		{
			RaycastHit[] hitInfos = Physics.RaycastAll(transform.position, Vector3.down, 10f);

			if(hitInfos == null || hitInfos.Length == 0)
				return;

			RaycastHit hitInfo = hitInfos
				.OrderBy(h => h.distance)
				.First();

			transform.position = new Vector3(transform.position.x, hitInfo.point.y + 0.001f, transform.position.z);
		}

		public SpawnPointData GetSpawnPoint()
		{
			//Cached references mostly for thread safety purposes
			return new SpawnPointData(CachedSpawnPosition, CachedSpawnRotation);
		}
	}
}
