using System; using FreecraftCore;
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

		private ITextChatEventFactory FormattedTextChatFactory { get; }

		public TextChatMessageEventPublisher([NotNull] ITextChatEventFactory formattedTextChatFactory)
		{
			FormattedTextChatFactory = formattedTextChatFactory ?? throw new ArgumentNullException(nameof(formattedTextChatFactory));
		}

		public void PublishEvent(object sender, [NotNull] TextChatEventArgs eventArgs)
		{
			if (eventArgs == null) throw new ArgumentNullException(nameof(eventArgs));

			//We need to handle unformatted text chat events.
			if(eventArgs.isFormattedText)
				OnTextChatMessageRecieved?.Invoke(sender, eventArgs);
			else
			{
				OnTextChatMessageRecieved?.Invoke(sender, FormattedTextChatFactory.CreateChatData(eventArgs));
			}
		}

		public async Task OnGameInitialized()
		{
			//Hack to load it into the scene
		}
	}
}
