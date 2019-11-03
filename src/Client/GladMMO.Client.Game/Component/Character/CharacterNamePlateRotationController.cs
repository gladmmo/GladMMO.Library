using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace GladMMO
{
	public sealed class CharacterNamePlateRotationController : MonoBehaviour
	{
		[UsedImplicitly]
		[SerializeField]
		private Transform LookTransform;

		[UsedImplicitly]
		[SerializeField]
		private Transform TextObjectTransform;

		void Start()
		{
			//TODO: Camera.main is SLOW.
			ProjectVersionStage.AssertAlpha();
			LookTransform = Camera.main.transform;
		}

		void LateUpdate()
		{
			TextObjectTransform.LookAt(LookTransform);
			Vector3 angles = TextObjectTransform.eulerAngles;
			
			//To get it looking at the actual camera we need to rotate it 180 degrees and then
			//invert the X to compensate.
			TextObjectTransform.eulerAngles = new Vector3(-angles.x, angles.y + 180, 0);
		}
	}
}
