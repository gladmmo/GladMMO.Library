using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace GladMMO
{
	public abstract class BaseAzureServiceQueueManager
	{
		/// <summary>
		/// The async Azure Queue client.
		/// </summary>
		protected IQueueClient ServiceQueue { get; }

		protected BaseAzureServiceQueueManager([NotNull] IQueueClient serviceQueue)
		{
			ServiceQueue = serviceQueue ?? throw new ArgumentNullException(nameof(serviceQueue));

			SetupQueue(serviceQueue);
		}

		private void SetupQueue([NotNull] IQueueClient serviceQueue)
		{
			if (serviceQueue == null) throw new ArgumentNullException(nameof(serviceQueue));

			serviceQueue
				.RegisterMessageHandler(HandleQueueIncomingMessageAsync, GenerateQueueMessageHandlerOptions());
		}

		private MessageHandlerOptions GenerateQueueMessageHandlerOptions()
		{
			return new MessageHandlerOptions(HandleQueueExceptionAsync) {AutoComplete = false, MaxConcurrentCalls = Environment.ProcessorCount};
		}

		/// <summary>
		/// Handles Azure Queue exception events.
		/// </summary>
		/// <param name="exceptionEventArgs">The exception events.</param>
		/// <returns>Awaitable.</returns>
		protected abstract Task HandleQueueExceptionAsync([NotNull] ExceptionReceivedEventArgs exceptionEventArgs);

		/// <summary>
		/// Handles Azure Queue's incoming message.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="cancellationToken">Token that indicates cancellation state of the client.</param>
		/// <returns>Awaitable.</returns>
		protected abstract Task HandleQueueIncomingMessageAsync([NotNull] Message message, CancellationToken cancellationToken);
	}
}
