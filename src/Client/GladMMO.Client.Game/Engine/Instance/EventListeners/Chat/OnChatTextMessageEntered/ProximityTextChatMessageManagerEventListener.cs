using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class ProximityTextChatMessageManagerEventListener : IGameInitializable
	{
		private ILog Logger { get; }

		public ProximityTextChatMessageManagerEventListener(IChatTextMessageEnteredEventSubscribable subscriptionService,
			[NotNull] IChatChannelJoinedEventSubscribable channelJoinedSubscriptionService,
			[NotNull] ILog logger)
		{
			if (channelJoinedSubscriptionService == null) throw new ArgumentNullException(nameof(channelJoinedSubscriptionService));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));

			//This just registers and wires up that when we join a channel we'll start recieveing that channels messages.
			channelJoinedSubscriptionService.OnChatChannelJoined += (o, e) =>
			{
				if (!CheckType(e))
					return;

				subscriptionService.OnChatMessageEntered += (o2, e2) =>
				{
					if (!CheckType(e2))
						return;

					OnEventFired(e2);
				};
			};
		}

		protected void OnEventFired([NotNull] ChatTextMessageEnteredEventArgs args)
		{
			if (args == null) throw new ArgumentNullException(nameof(args));

			Logger.Debug($"Recieved proximity chat message.");
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool CheckType([NotNull] IChatChannelRoutable routable)
		{
			if (routable == null) throw new ArgumentNullException(nameof(routable));

			return routable.ChannelType == ChatChannelType.Proximity;
		}

		public async Task OnGameInitialized()
		{

		}
	}
}
