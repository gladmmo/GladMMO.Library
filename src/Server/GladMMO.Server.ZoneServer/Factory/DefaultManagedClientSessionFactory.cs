﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using GladNet;
using JetBrains.Annotations;

namespace GladMMO
{
	[AdditionalRegisterationAs(typeof(IFactoryCreatable<ManagedClientSession<GameServerPacketPayload, GameClientPacketPayload>, ManagedClientSessionCreationContext>))]
	[AdditionalRegisterationAs(typeof(IManagedClientSessionFactory))]
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class DefaultManagedClientSessionFactory : IManagedClientSessionFactory, IGameInitializable
	{
		private ILog Logger { get; }

		private MessageHandlerService<GameClientPacketPayload, GameServerPacketPayload, IPeerSessionMessageContext<GameServerPacketPayload>> HandlerService { get; }

		/// <inheritdoc />
		public DefaultManagedClientSessionFactory(
			[NotNull] ILog logger,
			[NotNull] MessageHandlerService<GameClientPacketPayload, GameServerPacketPayload, IPeerSessionMessageContext<GameServerPacketPayload>> handlerService)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			HandlerService = handlerService ?? throw new ArgumentNullException(nameof(handlerService));
		}

		/// <inheritdoc />
		public ManagedClientSession<GameServerPacketPayload, GameClientPacketPayload> Create([NotNull] ManagedClientSessionCreationContext context)
		{
			if(context == null) throw new ArgumentNullException(nameof(context));

			if(Logger.IsDebugEnabled)
				Logger.Debug($"Creating new session. Details: {context.Details}");

			try
			{

				ZoneClientSession clientSession = new ZoneClientSession(context.Client, context.Details, HandlerService, Logger);

				return clientSession;
			}
			catch(Exception e)
			{
				if(Logger.IsErrorEnabled)
					Logger.Error($"Failed to create Client: {context.Details} Error: {e.Message} \n\n Stack: {e.StackTrace}");

				throw;
			}
		}

		//TODO: This is a hack to get it into the scene.
		/// <inheritdoc />
		public Task OnGameInitialized()
		{
			return Task.CompletedTask;
		}
	}

	public sealed class ManagedClientSessionCreationContext
	{
		public IManagedNetworkServerClient<GameServerPacketPayload, GameClientPacketPayload> Client { get; }

		public SessionDetails Details { get; }

		/// <inheritdoc />
		public ManagedClientSessionCreationContext([NotNull] IManagedNetworkServerClient<GameServerPacketPayload, GameClientPacketPayload> client, [NotNull] SessionDetails details)
		{
			Client = client ?? throw new ArgumentNullException(nameof(client));
			Details = details ?? throw new ArgumentNullException(nameof(details));
		}
	}
}
