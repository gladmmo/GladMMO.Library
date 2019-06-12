using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class PlayerNetworkTrackerChangeUpdateEventHandler : BaseGameClientGameMessageHandler<PlayerNetworkTrackerChangeUpdateEvent>, IPlayerTrackerTransformChangedEventSubscribable
	{
		public event EventHandler<PlayerTrackerTransformChangedEventArgs> OnTrackerTransformChanged;

		public PlayerNetworkTrackerChangeUpdateEventHandler(ILog logger) 
			: base(logger)
		{

		}

		public override Task HandleMessage(IPeerMessageContext<GameClientPacketPayload> context, PlayerNetworkTrackerChangeUpdateEvent payload)
		{
			//TODO: Do some validation here.
			OnTrackerTransformChanged?.Invoke(this, new PlayerTrackerTransformChangedEventArgs(payload.PlayerTrackerUpdate));
			return Task.CompletedTask;
		}
	}
}
