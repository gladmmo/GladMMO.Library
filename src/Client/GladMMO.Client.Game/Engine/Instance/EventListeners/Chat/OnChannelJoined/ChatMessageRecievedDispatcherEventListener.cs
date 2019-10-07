using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Fasterflect;
using Glader.Essentials;
using GladMMO.Chat;
using VivoxUnity;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class ChatMessageRecievedDispatcherEventListener : BaseSingleEventListenerInitializable<IChatChannelJoinedEventSubscribable, ChatChannelJoinedEventArgs>
	{
		private IChatTextMessageRecievedEventPublisher MessageRecievedPublisher { get; }

		private ITextChatEventFactory TextDataFactory { get; }

		public ChatMessageRecievedDispatcherEventListener(IChatChannelJoinedEventSubscribable subscriptionService,
			[NotNull] IChatTextMessageRecievedEventPublisher messageRecievedPublisher,
			[NotNull] ITextChatEventFactory textDataFactory) 
			: base(subscriptionService)
		{
			MessageRecievedPublisher = messageRecievedPublisher ?? throw new ArgumentNullException(nameof(messageRecievedPublisher));
			TextDataFactory = textDataFactory ?? throw new ArgumentNullException(nameof(textDataFactory));
		}

		protected override void OnEventFired(object source, ChatChannelJoinedEventArgs args)
		{
			//We just register a callback to the message received and specificy the channel from the join args.
			args.Channel.OnChannelTextMessageRecieved += (s, e) => OnChatMessageRecieved(args.ChannelType, e);
		}

		private void OnChatMessageRecieved(ChatChannelType channelType, [NotNull] IChannelTextMessage args)
		{
			if (args == null) throw new ArgumentNullException(nameof(args));
			if (!Enum.IsDefined(typeof(ChatChannelType), channelType)) throw new InvalidEnumArgumentException(nameof(channelType), (int) channelType, typeof(ChatChannelType));

			AccountId id = args.Sender;
			int characterId = int.Parse(id.Name);

			//TODO: We need to translate the guid to a name.
			NetworkEntityGuid guid = NetworkEntityGuidBuilder.New()
				.WithType(EntityType.Player)
				.WithId(characterId)
				.Build();

			TextChatEventArgs data = TextDataFactory.CreateChatData(new EntityAssociatedData<VivoxChannelTextMessageChatMessageAdapter>(guid, new VivoxChannelTextMessageChatMessageAdapter(args, channelType)), id.Name);

			MessageRecievedPublisher.PublishEvent(this, data);
		}
	}
}
