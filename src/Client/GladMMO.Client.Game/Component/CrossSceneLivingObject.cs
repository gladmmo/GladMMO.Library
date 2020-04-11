using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public sealed class CrossSceneLivingObject : MonoBehaviour
	{
		[SerializeField]
		private CrossSceneObjectType _Type = CrossSceneObjectType.Unknown;

		public CrossSceneObjectType Type => _Type;

		private void Awake()
		{
			DontDestroyOnLoad(gameObject);
		}
	}
}
