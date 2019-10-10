using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Glader.Essentials;
using GladNet;
using SceneJect.Common;
using UnityEngine;

namespace GladMMO
{
	public sealed class CommonInstanceServerDependencyRegister : NonBehaviourDependency
	{
		[SerializeField]
		public ServerSceneType SceneType;

		/// <inheritdoc />
		public override void Register(ContainerBuilder register)
		{
			register.RegisterModule(new EngineInterfaceRegisterationModule((int)SceneType, GetType().Assembly));

			register.RegisterModule(new BaseHandlerRegisterationModule<IPeerMessageHandler<GameClientPacketPayload, GameServerPacketPayload, IPeerSessionMessageContext<GameServerPacketPayload>>>((int)SceneType, GetType().Assembly));

			register.RegisterModule<CharacterServiceDependencyAutofacModule>();
		}
	}
}
