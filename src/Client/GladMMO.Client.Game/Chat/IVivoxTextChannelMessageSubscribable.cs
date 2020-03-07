using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using VivoxUnity;

namespace GladMMO
{
	public interface IVivoxTextChannelMessageSubscribable
	{
		event EventHandler<IChannelTextMessage> OnChannelTextMessageRecieved;
	}

	public sealed class DefaultVivoxTextChannelSubscribableAdapter : IVivoxTextChannelMessageSubscribable
	{
		public event EventHandler<IChannelTextMessage> OnChannelTextMessageRecieved;

		public DefaultVivoxTextChannelSubscribableAdapter([NotNull] IChannelSession channel)
		{
			if (channel == null) throw new ArgumentNullException(nameof(channel));

			channel.MessageLog.AfterItemAdded += MessageLogOnAfterItemAdded;
		}

		private void MessageLogOnAfterItemAdded(object sender, [NotNull] QueueItemAddedEventArgs<IChannelTextMessage> e)
		{
			if (e == null) throw new ArgumentNullException(nameof(e));

			OnChannelTextMessageRecieved?.Invoke(sender, e.Value);
		}
	}
}
