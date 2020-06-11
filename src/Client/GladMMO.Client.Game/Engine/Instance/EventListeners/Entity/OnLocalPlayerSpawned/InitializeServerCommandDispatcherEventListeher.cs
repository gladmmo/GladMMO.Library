using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore;
using GladNet;
using VivoxUnity;

namespace GladMMO
{
	public class StubTextChannelMessageSubscribable : IVivoxTextChannelMessageSubscribable
	{
		public event EventHandler<IChannelTextMessage> OnChannelTextMessageRecieved;
	}

	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class InitializeServerCommandDispatcherEventListener : OnLocalPlayerSpawnedEventListener
	{
		private class RealtimeServerCommandChannelSender : IChatChannelSender
		{
			private IPeerPayloadSendService<GamePacketPayload> SendService { get; }

			public RealtimeServerCommandChannelSender([NotNull] IPeerPayloadSendService<GamePacketPayload> sendService)
			{
				SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));
			}

			public async Task SendAsync(string messageContent)
			{
				await SendService.SendMessage(new ChatMessageRequest(new SayPlayerChatMessage(ChatLanguage.LANG_ADDON, messageContent)));
			}
		}

		//We use this service to publish a fake-ish channel for command messages.
		private IChatChannelJoinedEventPublisher ChannelJoinPublisher { get; }

		private IPeerPayloadSendService<GamePacketPayload> SendService { get; }

		public InitializeServerCommandDispatcherEventListener([NotNull] ILocalPlayerSpawnedEventSubscribable subscriptionService,
			[NotNull] IChatChannelJoinedEventPublisher channelJoinPublisher,
			[NotNull] IPeerPayloadSendService<GamePacketPayload> sendService) 
			: base(subscriptionService)
		{
			ChannelJoinPublisher = channelJoinPublisher ?? throw new ArgumentNullException(nameof(channelJoinPublisher));
			SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));
		}

		protected override void OnLocalPlayerSpawned(LocalPlayerSpawnedEventArgs args)
		{
			ChannelJoinPublisher.PublishEvent(this, new ChatChannelJoinedEventArgs(ChatChannelType.RealtimeServerCommand, new StubTextChannelMessageSubscribable(), new RealtimeServerCommandChannelSender(SendService)));
		}
	}
}
