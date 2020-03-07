using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Autofac;
using GladNet;
using SceneJect.Common;
using UnityEngine;

namespace GladMMO
{
	/// <summary>
	/// Simplified creatable type for <see cref="AuthenticationDependencyAutofacModule"/>
	/// </summary>
	public sealed class AuthenticationRegisterModule : AutofacBasedDependencyRegister<AuthenticationDependencyAutofacModule>
	{

	}
}
