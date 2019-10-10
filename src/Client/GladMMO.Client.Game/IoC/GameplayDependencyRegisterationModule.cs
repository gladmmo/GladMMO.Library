using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using SceneJect.Common;
using UnityEngine;

namespace GladMMO
{
	public sealed class GameplayDependencyRegisterationModule : AutofacBasedDependencyRegister<GameplayDependencyRegisterationAutofacModule>
	{
		public override void Register(ContainerBuilder register)
		{
			base.Register(register);
			register.RegisterModule<DynamicContentDownloadingAutofacModule>();
		}
	}
}
