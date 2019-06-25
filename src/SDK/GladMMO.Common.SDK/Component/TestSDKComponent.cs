using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public sealed class TestSDKComponent : GladMMOSDKMonoBehaviour
	{
		[Button]
		public void TestLogging()
		{
			Debug.Log($"Button pressed.");
		}
	}
}
