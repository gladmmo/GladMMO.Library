using System; using FreecraftCore;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using Nito.AsyncEx;

namespace GladMMO
{
	//This is for sending Chat messages to TrinityCore (the real-time gameserver)
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class RealTimeGameServerCommandMessageManagerEventListener : BaseRoutableChatTextMessageEnteredEventListener
	{
		public RealTimeGameServerCommandMessageManagerEventListener(IChatTextMessageEnteredEventSubscribable subscriptionService,
			[NotNull] IChatChannelJoinedEventSubscribable channelJoinedSubscriptionService,
			[NotNull] ILog logger)
			: base(subscriptionService, channelJoinedSubscriptionService, logger, ChatChannelType.RealtimeServerCommand)
		{
			
		}
	}
}
