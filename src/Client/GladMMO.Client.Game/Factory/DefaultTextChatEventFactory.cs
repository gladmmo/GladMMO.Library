using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Glader.Essentials;

namespace GladMMO
{
	[AdditionalRegisterationAs(typeof(ITextChatEventFactory))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class DefaultTextChatEventFactory : ITextChatEventFactory, IGameInitializable
	{
		/// <inheritdoc />
		public TextChatEventArgs CreateChatData<TMessageType>([NotNull] EntityAssociatedData<TMessageType> incomingChatMessageEventData, [NotNull] string associatedEntityName) 
			where TMessageType : ITextMessageContainable, IChatChannelRoutable
		{
			if(incomingChatMessageEventData == null) throw new ArgumentNullException(nameof(incomingChatMessageEventData));
			if(associatedEntityName == null) throw new ArgumentNullException(nameof(associatedEntityName));

			ChatChannelType messageType = incomingChatMessageEventData.Data.ChannelType;

			string renderableMessage = $"<color=#{ComputeColorFromChatType(messageType)}>{ComputeChannelText(messageType)} {associatedEntityName}: {incomingChatMessageEventData.Data.Message}</color>";

			return new TextChatEventArgs(renderableMessage, incomingChatMessageEventData.EntityGuid, messageType);
		}

		/// <inheritdoc />
		public TextChatEventArgs CreateChatData<TMessageType>([NotNull] TMessageType incomingChatMessageEventData) 
			where TMessageType : ITextMessageContainable, IChatChannelRoutable
		{
			if(incomingChatMessageEventData == null) throw new ArgumentNullException(nameof(incomingChatMessageEventData));

			ChatChannelType messageType = incomingChatMessageEventData.ChannelType;

			string renderableMessage = $"<color=#{ComputeColorFromChatType(messageType)}>{ComputeChannelText(messageType)}: {incomingChatMessageEventData.Message}</color>";

			return new TextChatEventArgs(renderableMessage, messageType);
		}

		private string ComputeColorFromChatType(ChatChannelType messageType)
		{
			switch(messageType)
			{
				case ChatChannelType.System:
					return "ff0000ff";
				case ChatChannelType.Zone:
					return "AA9E92ff";
				case ChatChannelType.Guild:
					return "42f442ff";
				case ChatChannelType.Proximity:
					return "ffffffff";
			}

			throw new NotImplementedException($"Cannot handle Chat Type: {messageType}:{(int)messageType}");
		}

		private string ComputeChannelText(ChatChannelType messageType)
		{
			switch(messageType)
			{
				case ChatChannelType.Proximity:
					return "[Say]";
				case ChatChannelType.System:
					return "[System]";
				case ChatChannelType.Zone:
					return "[1. Zone]";
				case ChatChannelType.Guild:
					return "[Guild]";
			}

			throw new NotImplementedException($"Cannot handle Chat Type: {messageType}:{(int)messageType}");
		}

		public async Task OnGameInitialized()
		{

		}
	}
}