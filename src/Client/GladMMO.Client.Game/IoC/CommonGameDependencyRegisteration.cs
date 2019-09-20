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

			return new CommonGameDependencyModule(SceneType, "http://127.0.0.1:5000", this.GetType().Assembly);
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
