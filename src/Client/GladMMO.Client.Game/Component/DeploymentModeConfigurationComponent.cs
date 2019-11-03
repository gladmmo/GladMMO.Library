using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public sealed class DeploymentModeConfigurationComponent : MonoBehaviour
	{
		private static DeploymentModeConfigurationComponent Instance;

		private static readonly Lazy<DeploymentMode> LazyInitializedMode;

		static DeploymentModeConfigurationComponent()
		{
			LazyInitializedMode = new Lazy<DeploymentMode>(() => DeploymentModeConfigurationComponent.Instance._Mode);
		}

		public static DeploymentMode Mode => LazyInitializedMode.Value;

		[SerializeField]
		private DeploymentMode _Mode;

		public DeploymentModeConfigurationComponent()
		{
			Instance = this;
		}
	}
}
