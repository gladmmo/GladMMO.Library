using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public sealed class CrossSceneLivingObject : MonoBehaviour
	{
		[SerializeField]
		private CrossSceneObjectType Type = CrossSceneObjectType.Unknown;

		private void Awake()
		{
			DontDestroyOnLoad(gameObject);
		}
	}
}
