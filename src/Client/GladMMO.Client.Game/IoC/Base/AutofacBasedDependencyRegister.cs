using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Autofac.Core;
using SceneJect.Common;

namespace GladMMO
{
	/// <summary>
	/// Base asbtract type for easily registering Sceneject dependency collections
	/// that are implemented within <see cref="Autofac.Module"/>'s.
	/// </summary>
	/// <typeparam name="TModuleType">The module to register.</typeparam>
	public abstract class AutofacBasedDependencyRegister<TModuleType> : NonBehaviourDependency
		where TModuleType : IModule, new()
	{
		protected AutofacBasedDependencyRegister()
		{
			
		}

		/// <inheritdoc />
		public override void Register(ContainerBuilder register)
		{
			//We just register the autofac module.
			register.RegisterModule(CreateModule());
		}

		protected virtual TModuleType CreateModule()
		{
			return new TModuleType();
		}
	}
}
