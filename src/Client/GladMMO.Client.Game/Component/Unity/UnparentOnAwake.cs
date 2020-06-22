using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public sealed class UnparentOnAwake : MonoBehaviour
	{
		private void Awake()
		{
			gameObject.transform.parent = null;
		}
	}
}
