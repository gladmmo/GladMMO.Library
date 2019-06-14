using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladMMO;
using UnityEngine;

namespace Booma.Proxy
{
	public sealed class StaticSpawnPointDefinition : GladMMOSDKMonoBehaviour
	{
		//TODO: IFDEF this out for deployment builds
		private void OnDrawGizmos()
		{
			Color originalColor = Gizmos.color;

			Gizmos.color = Color.blue;
			Gizmos.DrawSphere(transform.position, transform.localScale.magnitude);
			Gizmos.color = originalColor;

			//TODO: Add spawnpoint image icon
			//Gizmos.DrawIcon(transform.position, "SpawnPoint.png", true);
		}

		[Button]
		private void StickSpawnPointToCollider()
		{
			transform.rotation = Quaternion.identity;

			RaycastHit hitInfo;
			if(Physics.Raycast(transform.position, Vector3.down, out hitInfo, 10f)) //don't use a layer mask
			{
				transform.position = new Vector3(transform.position.x, hitInfo.point.y, transform.position.z);
			}
		}
	}
}
