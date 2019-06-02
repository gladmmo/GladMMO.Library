using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using Autofac;
using Common.Logging;
using Common.Logging.Simple;
using GladNet;
using JetBrains.Annotations;
using ProtoBuf;

namespace GladMMO
{
	public sealed class ZoneServerApplicationBase : TcpServerServerApplicationBase<GameServerPacketPayload, GameClientPacketPayload>
	{
		private IManagedSessionFactory ManagedSessionFactory { get; }

		private IManagedClientSessionFactory ManagedClientSessionFactory { get; }

		/// <inheritdoc />
		public ZoneServerApplicationBase([NotNull] NetworkAddressInfo serverAddress, [NotNull] ILog logger, [NotNull] IManagedSessionFactory managedSessionFactory, [NotNull] IManagedClientSessionFactory managedClientSessionFactory) 
			: base(serverAddress, logger)
		{
			if(serverAddress == null) throw new ArgumentNullException(nameof(serverAddress));
			if(logger == null) throw new ArgumentNullException(nameof(logger));

			ManagedSessionFactory = managedSessionFactory ?? throw new ArgumentNullException(nameof(managedSessionFactory));
			ManagedClientSessionFactory = managedClientSessionFactory ?? throw new ArgumentNullException(nameof(managedClientSessionFactory));

			if(Logger.IsDebugEnabled)
				Logger.Debug($"Created server base.");
		}

		/// <inheritdoc />
		public ZoneServerApplicationBase([NotNull] NetworkAddressInfo serverAddress, [NotNull] INetworkMessageDispatchingStrategy<GameServerPacketPayload, GameClientPacketPayload> messageHandlingStrategy, [NotNull] IManagedSessionFactory managedSessionFactory, [NotNull] IManagedClientSessionFactory managedClientSessionFactory) 
			: base(serverAddress, messageHandlingStrategy, new NoOpLogger())
		{
			if(serverAddress == null) throw new ArgumentNullException(nameof(serverAddress));
			if(messageHandlingStrategy == null) throw new ArgumentNullException(nameof(messageHandlingStrategy));
			ManagedSessionFactory = managedSessionFactory ?? throw new ArgumentNullException(nameof(managedSessionFactory));
			ManagedClientSessionFactory = managedClientSessionFactory ?? throw new ArgumentNullException(nameof(managedClientSessionFactory));

			if(Logger.IsDebugEnabled)
				Logger.Debug($"Created server base.");
		}

		/// <inheritdoc />
		protected override bool IsClientAcceptable(TcpClient tcpClient)
		{
			//TODO: This is where you would reject clients if you had a reason to.
			return true;
		}

		/// <inheritdoc />
		protected override IManagedNetworkServerClient<GameServerPacketPayload, GameClientPacketPayload> CreateIncomingSessionPipeline(TcpClient client)
		{
			return ManagedSessionFactory.Create(new ManagedSessionCreationContext(client));
		}

		/// <inheritdoc />
		protected override ManagedClientSession<GameServerPacketPayload, GameClientPacketPayload> CreateIncomingSession(IManagedNetworkServerClient<GameServerPacketPayload, GameClientPacketPayload> client, SessionDetails details)
		{
			return ManagedClientSessionFactory.Create(new ManagedClientSessionCreationContext(client, details));
		}
	}
}
 