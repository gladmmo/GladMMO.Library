using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public sealed class VivoxDemoAPIKey : MonoBehaviour
	{
		[SerializeField]
		private string _AccessKey;

		public static string AccessKey { get; private set; }

		void Awake()
		{
			AccessKey = _AccessKey;
		}
	}
}
