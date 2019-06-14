using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladMMO;
using UnityEngine;

namespace Booma.Proxy
{
	public sealed class StaticSpawnPointDefinition : GladMMOSDKMonoBehaviour, ISpawnPointStrategy
	{
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
			return new SpawnPointData(transform.position, transform.rotation);
		}
	}
}
