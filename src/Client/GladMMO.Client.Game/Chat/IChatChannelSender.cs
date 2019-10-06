using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Glader.Essentials;
using Nito.AsyncEx;
using VivoxUnity;

namespace GladMMO
{
	public interface IChatChannelSender
	{
		Task SendAsync(string messageContent);
	}

	public sealed class DefaultVivoxChatChannelSenderAdapter : IChatChannelSender
	{
		private IChannelSession Channel { get; }

		public DefaultVivoxChatChannelSenderAdapter([NotNull] IChannelSession channel)
		{
			Channel = channel ?? throw new ArgumentNullException(nameof(channel));
		}

		public Task SendAsync(string messageContent)
		{
			return Task.Factory.FromAsync(BeginSendText, Channel.EndSendText, messageContent);
		}

		//Adapt the the expected signature.
		public IAsyncResult BeginSendText(AsyncCallback callback, object state)
		{
			string message = (string)state;
			return Channel.BeginSendText(message, callback);
		}
	}
}
