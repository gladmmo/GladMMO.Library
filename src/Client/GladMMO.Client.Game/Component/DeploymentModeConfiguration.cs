using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public sealed class DeploymentModeConfiguration : MonoBehaviour
	{
		private static DeploymentModeConfiguration Instance;

		private static readonly Lazy<DeploymentMode> LazyInitializedMode;

		static DeploymentModeConfiguration()
		{
			LazyInitializedMode = new Lazy<DeploymentMode>(() => DeploymentModeConfiguration.Instance._Mode);
		}

		public static DeploymentMode Mode => LazyInitializedMode.Value;

		[SerializeField]
		private DeploymentMode _Mode;

		public DeploymentModeConfiguration()
		{
			Instance = this;
		}
	}
}
