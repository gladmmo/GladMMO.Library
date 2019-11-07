using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Common.Logging;
using GladNet;
using JetBrains.Annotations;
using ProtoBuf;
using Refit;
using SceneJect.Common;
using UnityEngine;
using UnityEngine.Rendering;

namespace GladMMO
{
	//TODO: Refactor to the Autofac module system
	public sealed class DefaultZoneServerDependencyRegistrar : NonBehaviourDependency
	{
		public override void Register(ContainerBuilder builder)
		{
			builder.RegisterModule<DefaultZoneServerDependencyModule>();
			builder.RegisterModule<ActorSystemDependencyAutofacModule>();
			builder.RegisterModule<SpellDataServiceDependencyAutofacModule>();
		}
	}
}
