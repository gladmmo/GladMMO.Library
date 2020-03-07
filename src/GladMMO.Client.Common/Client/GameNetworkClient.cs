using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;
using GladMMO;
using SceneJect.Common;

namespace GladMMO
{
	/// <summary>
	/// The component that manages the game network client.
	/// </summary>
	public sealed class GameNetworkClient : BaseUnityNetworkClient<GamePacketPayload, GamePacketPayload>, INetworkClientManager, INetworkClientDisconnectedEventSubscribable
	{
		public event EventHandler OnNetworkClientDisconnected;

		/// <inheritdoc />
		public GameNetworkClient(MessageHandlerService<GamePacketPayload, GamePacketPayload> handlers, ILog logger, IPeerMessageContextFactory messageContextFactory)
			: base(handlers, logger, messageContextFactory)
		{

		}

		protected override void OnClientStoppedHandlingMessages()
		{
			OnNetworkClientDisconnected?.Invoke(this, EventArgs.Empty);
		}
	}
}
