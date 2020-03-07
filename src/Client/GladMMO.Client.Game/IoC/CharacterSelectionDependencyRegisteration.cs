using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace GladMMO
{
	public sealed class CharacterSelectionDependencyRegisteration : AutofacBasedDependencyRegister<CharacterSelectionDependencyAutofacModule>
	{
		public override void Register(ContainerBuilder register)
		{
			base.Register(register);
			register.RegisterModule<DynamicContentDownloadingAutofacModule>();
			register.RegisterModule<ContentServerDependencyAutofacModule>();
		}
	}
}
