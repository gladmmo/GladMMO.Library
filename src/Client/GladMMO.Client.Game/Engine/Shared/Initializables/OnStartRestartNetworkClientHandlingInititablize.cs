using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using GladNet;

namespace GladMMO
{
	//Basically, this component can be used to restart the network client
	//AND also broadcast the connection established event.
	[AdditionalRegisterationAs(typeof(INetworkConnectionEstablishedEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.DefaultLobby)]
	public sealed class OnStartRestartNetworkClientHandlingInititablize : IGameInitializable, INetworkConnectionEstablishedEventSubscribable
	{
		/// <summary>
		/// The managed network client that the Unity3D client is implemented on-top of.
		/// </summary>
		private IManagedNetworkClient<GameClientPacketPayload, GameServerPacketPayload> Client { get; }

		private INetworkClientManager ClientManager { get; }

		private ILog Logger { get; }

		/// <inheritdoc />
		public event EventHandler OnNetworkConnectionEstablished;

		/// <inheritdoc />
		public OnStartRestartNetworkClientHandlingInititablize(
			[NotNull] IManagedNetworkClient<GameClientPacketPayload, GameServerPacketPayload> client, 
			[NotNull] INetworkClientManager clientManager,
			[NotNull] ILog logger)
		{
			Client = client ?? throw new ArgumentNullException(nameof(client));
			ClientManager = clientManager ?? throw new ArgumentNullException(nameof(clientManager));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		/// <inheritdoc />
		public async Task OnGameInitialized()
		{
			if(!Client.isConnected)
			{
				if(Logger.IsWarnEnabled)
					Logger.Warn($"Network Connection is disconnected. Cannot restart handling.");
			}
			else
			{
				//TODO: Should we check if it's still connected?
				await ClientManager.StartHandlingNetworkClient(Client)
					.ConfigureAwait(true); //it's just scene start, it's probably ok to capture the sync context

				if(Logger.IsInfoEnabled)
					Logger.Warn($"Network Connection restarted. Message handling resumed. Dispatching {nameof(INetworkConnectionEstablishedEventSubscribable)}");

				OnNetworkConnectionEstablished?.Invoke(this, EventArgs.Empty);
			}
		}
	}
}