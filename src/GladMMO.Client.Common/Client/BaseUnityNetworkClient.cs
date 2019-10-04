using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Common.Logging;
using GladNet;
using SceneJect.Common;

namespace GladMMO
{
	/// <summary>
	/// Abstract base network client for Unity3D.
	/// </summary>
	/// <typeparam name="TIncomingPayloadType"></typeparam>
	/// <typeparam name="TOutgoingPayloadType"></typeparam>
	public abstract class BaseUnityNetworkClient<TIncomingPayloadType, TOutgoingPayloadType> : INetworkClientManager<TIncomingPayloadType, TOutgoingPayloadType>
		where TOutgoingPayloadType : class 
		where TIncomingPayloadType : class
	{
		/// <summary>
		/// The message handler service.
		/// </summary>
		protected MessageHandlerService<TIncomingPayloadType, TOutgoingPayloadType> Handlers { get; set; }

		/// <summary>
		/// The logger for the client.
		/// </summary>
		public ILog Logger { get; private set; }

		/// <summary>
		/// The message context factory that builds the contexts
		/// for the handlers.
		/// </summary>
		protected IPeerMessageContextFactory MessageContextFactory { get; private set; }

		/// <summary>
		/// The token source for canceling the read message await.
		/// </summary>
		protected CancellationTokenSource CancelTokenSource { get; } = new CancellationTokenSource();

		/// <inheritdoc />
		protected BaseUnityNetworkClient(
			MessageHandlerService<TIncomingPayloadType, TOutgoingPayloadType> handlers, 
			ILog logger, 
			IPeerMessageContextFactory messageContextFactory)
		{
			Handlers = handlers;
			Logger = logger;
			MessageContextFactory = messageContextFactory;
		}

		/// <summary>
		/// Starts dispatching the messages and won't yield until
		/// the client has stopped or has disconnected.
		/// </summary>
		/// <returns></returns>
		protected async Task StartDispatchingAsync([NotNull] IManagedNetworkClient<TOutgoingPayloadType, TIncomingPayloadType> client)
		{
			if(client == null) throw new ArgumentNullException(nameof(client));

			try
			{
				IPeerRequestSendService<TOutgoingPayloadType> requestService = new PayloadInterceptMessageSendService<TOutgoingPayloadType>(client, client);

				if (!client.isConnected && Logger.IsWarnEnabled)
					Logger.Warn($"The client was not connected before dispatching started.");

				while (client.isConnected && !CancelTokenSource.IsCancellationRequested) //if we exported we should reading messages
				{
					NetworkIncomingMessage<TIncomingPayloadType> message = await client.ReadMessageAsync(CancelTokenSource.Token)
						.ConfigureAwait(false);

					//Supress and continue reading
					try
					{
						//We don't do anything with the result. We should hope someone registered
						//a default handler to deal with this situation
						bool result = await Handlers.TryHandleMessage(MessageContextFactory.Create(client, client, requestService), message)
							.ConfigureAwait(false);
					}
					catch (Exception e)
					{
						if (Logger.IsInfoEnabled)
							Logger.Info($"Error: {e.Message}\n\n Stack Trace: {e.StackTrace}");
					}

				}
			}
			catch (Exception e)
			{
				if (Logger.IsInfoEnabled)
					Logger.Info($"Error: {e.Message}\n\n Stack Trace: {e.StackTrace}");

				throw;
			}
			finally
			{
				if(Logger.IsInfoEnabled)
					Logger.Info("Network client stopped reading.");

				OnClientStoppedHandlingMessages();
			}
		}

		protected abstract void OnClientStoppedHandlingMessages();

		/// <inheritdoc />
		public Task StartHandlingNetworkClient(IManagedNetworkClient<TOutgoingPayloadType, TIncomingPayloadType> client)
		{
			//Don't await because we want start to end.
			Task.Factory.StartNew(async () => await StartDispatchingAsync(client).ConfigureAwait(false), CancelTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default)
				.ConfigureAwait(false);

			//We don't want to await it, it needs to run at the same time
			return Task.CompletedTask;
		}

		/// <inheritdoc />
		public Task StopHandlingNetworkClient()
		{
			CancelTokenSource.Cancel();
			//TODO: Should we await for the dispatch thread to actually end??

			return Task.CompletedTask;
		}
	}
}
