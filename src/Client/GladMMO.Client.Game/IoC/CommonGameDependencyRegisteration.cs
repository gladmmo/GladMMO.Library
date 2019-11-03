using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Common.Logging;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO.Client
{
	public sealed class CommonGameDependencyRegisteration : AutofacBasedDependencyRegister<CommonGameDependencyModule>
	{
		[SerializeField]
		public GameSceneType SceneType;

		/// <inheritdoc />
		protected override CommonGameDependencyModule CreateModule()
		{
			UnityAsyncHelper.UnityUIAsyncContinuationBehaviour = this.gameObject.AddComponent<UnityUIAsyncContinuationBehaviour>();

			string serviceDiscoveryUrl = null;

			switch (DeploymentModeConfiguration.Mode)
			{
				case DeploymentMode.Local:
					serviceDiscoveryUrl = "http://72.190.177.214:5000";
					break;
				case DeploymentMode.AzureTest:
					serviceDiscoveryUrl = "https://test-guardians-servicediscovery.azurewebsites.net";
					break;
				case DeploymentMode.AzureProduction:
					serviceDiscoveryUrl = "https://prod-guardians-servicediscovery.azurewebsites.net";
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			return new CommonGameDependencyModule(SceneType, serviceDiscoveryUrl, this.GetType().Assembly, typeof(GladMMOClientCommonMetadataMarker).Assembly);
		}

		public override void Register(ContainerBuilder register)
		{
			base.Register(register);
			register.RegisterModule(new UIDependencyRegisterationModule<UnityUIRegisterationKey>());

			//Set the sync context
			UnityAsyncHelper.InitializeSyncContext();

			register.RegisterType<UnityLogger>()
				.As<ILog>()
				.SingleInstance();
		}

		//TODO: Move this somewhere else
		internal class UnityUIAsyncContinuationBehaviour : MonoBehaviour
		{

		}
	}
}
