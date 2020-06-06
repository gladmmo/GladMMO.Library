using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using GladNet;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class OnLogoutDisconnectEventListener : BaseSingleEventListenerInitializable<IInstanceLogoutEventSubscribable>
	{
		private IConnectionService ConnectionService { get; }

		private INetworkClientManager ClientManager { get; }

		public OnLogoutDisconnectEventListener(IInstanceLogoutEventSubscribable subscriptionService,
			[NotNull] IConnectionService connectionService,
			[NotNull] INetworkClientManager clientManager) 
			: base(subscriptionService)
		{
			ConnectionService = connectionService ?? throw new ArgumentNullException(nameof(connectionService));
			ClientManager = clientManager ?? throw new ArgumentNullException(nameof(clientManager));
		}

		protected override void OnEventFired(object source, EventArgs args)
		{
			if (ConnectionService.isConnected)
			{
				ConnectionService.Disconnect();
				ClientManager.StopHandlingNetworkClient(true);
			}
		}
	}
}
