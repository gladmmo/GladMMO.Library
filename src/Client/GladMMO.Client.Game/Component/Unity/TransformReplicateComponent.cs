using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public sealed class TransformReplicateComponent : MonoBehaviour
	{
		[SerializeField]
		private Transform TransformToReplicate;

		private void Update()
		{
			//We just check the rotation of the transform and match it.
			//TODO: Handle position for "leaning forward" in VR, if that's even a thing
			transform.rotation = TransformToReplicate.rotation;
		}
	}
}