using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Common.Logging;
using GladNet;
using ProtoBuf;
using SceneJect.Common;
using UnityEngine;

namespace GladMMO
{
	/// <summary>
	/// Module responsible for registering the dependencies associated with the game client.
	/// </summary>
	public sealed class GameServerNetworkClientDependencyContainer : AutofacBasedDependencyRegister<GameServerNetworkClientAutofacModule>
	{
		public override void Register(ContainerBuilder register)
		{
			base.Register(register);
			register.RegisterModule<GladMMONetworkSerializerAutofacModule>();
		}
	}
}
