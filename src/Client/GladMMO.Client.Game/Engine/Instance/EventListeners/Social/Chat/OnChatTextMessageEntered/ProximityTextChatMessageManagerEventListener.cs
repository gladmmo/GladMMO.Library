using System;
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

					OnEventFired(e.MessageSender, e2);
				};
			};
		}

		protected void OnEventFired(IChatChannelSender sender, [NotNull] ChatTextMessageEnteredEventArgs args)
		{
			if (args == null) throw new ArgumentNullException(nameof(args));

			//Post it so we can get exception information.
			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				try
				{
					await sender.SendAsync(args.Content);
				}
				catch (Exception e)
				{
					if(Logger.IsErrorEnabled)
						Logger.Error($"Failed to send message. Reason: {e.Message}");

					throw;
				}
			});
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
