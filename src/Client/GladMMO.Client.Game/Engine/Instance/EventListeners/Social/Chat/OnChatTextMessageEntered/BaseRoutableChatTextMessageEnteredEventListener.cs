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
	public class BaseRoutableChatTextMessageEnteredEventListener : IGameInitializable
	{
		private ILog Logger { get; }

		protected ChatChannelType ChannelType { get; }

		public BaseRoutableChatTextMessageEnteredEventListener(IChatTextMessageEnteredEventSubscribable subscriptionService,
			[NotNull] IChatChannelJoinedEventSubscribable channelJoinedSubscriptionService,
			[NotNull] ILog logger, 
			ChatChannelType channelType)
		{
			if(channelJoinedSubscriptionService == null) throw new ArgumentNullException(nameof(channelJoinedSubscriptionService));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			ChannelType = channelType;

			//This just registers and wires up that when we join a channel we'll start recieveing that channels messages.
			channelJoinedSubscriptionService.OnChatChannelJoined += (o, e) =>
			{
				if(!CheckType(e))
					return;

				subscriptionService.OnChatMessageEntered += (o2, e2) =>
				{
					if(!CheckType(e2))
						return;

					OnEventFired(e.MessageSender, e2);
				};
			};
		}

		protected void OnEventFired(IChatChannelSender sender, [NotNull] ChatTextMessageEnteredEventArgs args)
		{
			if(args == null) throw new ArgumentNullException(nameof(args));

			//Post it so we can get exception information.
			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				try
				{
					await sender.SendAsync(args.Content);
				}
				catch(Exception e)
				{
					if(Logger.IsErrorEnabled)
						Logger.Error($"Failed to send message. Reason: {e.Message}");

					throw;
				}
			});
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected bool CheckType([NotNull] IChatChannelRoutable routable)
		{
			if(routable == null) throw new ArgumentNullException(nameof(routable));

			return routable.ChannelType == ChannelType;
		}

		//Hack to get into the scene.
		public Task OnGameInitialized()
		{
			return Task.CompletedTask;
		}
	}
}
