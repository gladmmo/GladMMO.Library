using System; using FreecraftCore;

namespace GladMMO
{
	public class GenericChatMessageContext<TMessageType>
		where TMessageType : class
	{
		public TMessageType IncomingMessage { get; }

		public IHubConnectionMessageContext HubContext { get; }

		/// <inheritdoc />
		public GenericChatMessageContext([JetBrains.Annotations.NotNull] TMessageType incomingMessage, [JetBrains.Annotations.NotNull] IHubConnectionMessageContext hubContext)
		{
			IncomingMessage = incomingMessage ?? throw new ArgumentNullException(nameof(incomingMessage));
			HubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
		}
	}
}