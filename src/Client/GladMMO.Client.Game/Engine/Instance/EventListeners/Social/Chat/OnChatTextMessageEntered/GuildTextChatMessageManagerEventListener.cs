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
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class GuildTextChatMessageManagerEventListener : BaseRoutableChatTextMessageEnteredEventListener
	{
		public GuildTextChatMessageManagerEventListener(IChatTextMessageEnteredEventSubscribable subscriptionService,
			[NotNull] IChatChannelJoinedEventSubscribable channelJoinedSubscriptionService,
			[NotNull] ILog logger)
			: base(subscriptionService, channelJoinedSubscriptionService, logger, ChatChannelType.Guild)
		{
			
		}
	}
}
