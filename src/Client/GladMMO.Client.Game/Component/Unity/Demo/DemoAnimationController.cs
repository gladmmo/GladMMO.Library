using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public class DemoAnimationController : MonoBehaviour
	{
		private Vector3 LastPosition;

		private float LastSpeed;

		[SerializeField]
		private Animator AnimatorComponent;

		//Important to use root forward because we MAY have our model rotated.
		private Transform RootTransform;

		private void Start()
		{
			RootTransform = transform.root;
			LastPosition = RootTransform.position;
			LastSpeed = 0.0f;
		}

		private void Update()
		{
			Vector3 directionNonNormalized = (RootTransform.position - LastPosition);

			float distanceTraveled = directionNonNormalized.magnitude;
			float speed = distanceTraveled / Time.deltaTime;

			float dot = Vector3.Dot(RootTransform.forward, directionNonNormalized.normalized);

			if (dot < -0.05f) //stutter will occur if you do exactly 0.0f
				speed = -speed;

			if (Math.Abs(LastSpeed - speed) > (float.Epsilon * 5.0f))
				AnimatorComponent.SetFloat("Speed", speed);

			LastSpeed = speed;
			LastPosition = RootTransform.position;
		}
	}
}
