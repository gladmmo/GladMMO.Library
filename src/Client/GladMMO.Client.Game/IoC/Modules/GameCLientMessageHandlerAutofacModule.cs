using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Autofac;
using Glader.Essentials;
using GladNet;

namespace GladMMO
{
	public sealed class GameClientMessageHandlerAutofacModule : Module
	{
		private GameSceneType SceneType { get; }

		/// <inheritdoc />
		public GameClientMessageHandlerAutofacModule(GameSceneType sceneType)
		{
			if(!Enum.IsDefined(typeof(GameSceneType), sceneType)) throw new InvalidEnumArgumentException(nameof(sceneType), (int)sceneType, typeof(GameSceneType));

			SceneType = sceneType;
		}

		private GameClientMessageHandlerAutofacModule()
		{
			
		}

		/// <inheritdoc />
		protected override void Load(ContainerBuilder builder)
		{
			//New IPeerContext generic param now so we register as implemented interface
			builder.RegisterType<DefaultServerPayloadHandler>()
				.AsImplementedInterfaces()
				.InstancePerLifetimeScope();

			/*builder.RegisterType<LoggableUnknownOpcodePayloadHandler>()
				.AsImplementedInterfaces()
				.AsSelf()
				.SingleInstance();*/

			builder.RegisterType<MessageHandlerService<GameServerPacketPayload, GameClientPacketPayload>>()
				.As<MessageHandlerService<GameServerPacketPayload, GameClientPacketPayload>>()
				.UsingConstructor(typeof(IEnumerable<IPeerMessageHandler<GameServerPacketPayload, GameClientPacketPayload>>), typeof(IPeerPayloadSpecificMessageHandler<GameServerPacketPayload, GameClientPacketPayload>))
				.InstancePerLifetimeScope();

			//HelloKitty: We just pass 1 since we don't really use the concept of scenes, so it can kinda be ignored.
			builder.RegisterModule(new BaseHandlerRegisterationModule<IPeerMessageHandler<GameServerPacketPayload, GameClientPacketPayload>>((int)SceneType, GetType().Assembly));
		}
	}
}
