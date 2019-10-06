using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Glader.Essentials;

namespace GladMMO
{
	/// <summary>
	/// Simplified interface for <see cref="IEventPublisher{TEventPublisherInterface,TEventArgsType}"/>
	/// for event <see cref="IChatTextMessageRecievedEventSubscribable"/>.
	/// </summary>
	public interface IChatTextMessageRecievedEventPublisher : IEventPublisher<IChatTextMessageRecievedEventSubscribable, TextChatEventArgs>
	{

	}

	[AdditionalRegisterationAs(typeof(IEventPublisher<IChatTextMessageRecievedEventSubscribable, TextChatEventArgs>))]
	[AdditionalRegisterationAs(typeof(IChatTextMessageRecievedEventPublisher))]
	[AdditionalRegisterationAs(typeof(IChatTextMessageRecievedEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class TextChatMessageEventPublisher : IChatTextMessageRecievedEventPublisher, IChatTextMessageRecievedEventSubscribable, IGameInitializable
	{
		public event EventHandler<TextChatEventArgs> OnTextChatMessageRecieved;

		public void PublishEvent(object sender, [NotNull] TextChatEventArgs eventArgs)
		{
			if (eventArgs == null) throw new ArgumentNullException(nameof(eventArgs));

			OnTextChatMessageRecieved?.Invoke(sender, eventArgs);
		}

		public async Task OnGameInitialized()
		{
			//Hack to load it into the scene
		}
	}
}
