using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public sealed class AvatarLerper : MonoBehaviour
	{
		private Vector3 lastPosition;

		private Quaternion lastRotation;

		[SerializeField]
		private Transform TransformToFollow;

		[Range(0.0f, 1.0f)]
		[SerializeField]
		private float LerpPower = 0.7f;

		void Start()
		{
			lastPosition = transform.position;
			lastRotation = transform.rotation;
		}

		void LateUpdate()
		{
			lastPosition = transform.position = Vector3.Lerp(lastPosition, TransformToFollow.position, LerpPower);
			lastRotation = transform.rotation = Quaternion.Slerp(lastRotation, TransformToFollow.rotation, LerpPower);
		}
	}
}
