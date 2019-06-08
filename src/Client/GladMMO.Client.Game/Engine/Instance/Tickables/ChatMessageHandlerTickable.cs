using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	public sealed class ChatMessageHandlerTickable : IGameTickable
	{
		private Queue<TextChatEventData> ChatEventQueue { get; }

		private IChatMessageBoxReciever ChatReciever { get; }

		/// <inheritdoc />
		public ChatMessageHandlerTickable([NotNull] Queue<TextChatEventData> chatEventQueue, [NotNull] IChatMessageBoxReciever chatReciever)
		{
			ChatEventQueue = chatEventQueue ?? throw new ArgumentNullException(nameof(chatEventQueue));
			ChatReciever = chatReciever ?? throw new ArgumentNullException(nameof(chatReciever));
		}

		/// <inheritdoc />
		public void Tick()
		{
			if(ChatEventQueue.Count == 0)
				return;

			//We only want to do one message a frame, or however often this runs.
			TextChatEventData chatEventData = ChatEventQueue.Dequeue();

			ChatReciever.ReceiveChatMessage(1, chatEventData.Message);
		}
	}
}
