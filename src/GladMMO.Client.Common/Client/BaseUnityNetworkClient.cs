using System; using FreecraftCore;
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

		private volatile bool StopSilently = false;

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

				//TODO: Read the next message before awaiting the result of the dispatch message handling.
				while (client.isConnected && !CancelTokenSource.IsCancellationRequested) //if we exported we should reading messages
				{
					NetworkIncomingMessage<TIncomingPayloadType> message = await client.ReadMessageAsync(CancelTokenSource.Token)
						.ConfigureAwaitFalse();

					//Supress and continue reading
					try
					{
						//We don't do anything with the result. We should hope someone registered
						//a default handler to deal with this situation
						bool result = await Handlers.TryHandleMessage(MessageContextFactory.Create(client, client, requestService), message)
							.ConfigureAwaitFalse();
					}
					catch (OperationCanceledException e)
					{
						if (!StopSilently)
						{
							if(Logger.IsInfoEnabled)
								Logger.Info($"Error: {e.Message}\n\n Stack Trace: {e.StackTrace}");
						}
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

				if(!StopSilently)
					OnClientStoppedHandlingMessages();
			}
		}

		protected abstract void OnClientStoppedHandlingMessages();

		public bool isNetworkHandling { get; private set; } = false;

		/// <inheritdoc />
		public Task StartHandlingNetworkClient(IManagedNetworkClient<TOutgoingPayloadType, TIncomingPayloadType> client)
		{
			isNetworkHandling = true;

			//Don't await because we want start to end.
			Task.Factory.StartNew(async () => await StartDispatchingAsync(client).ConfigureAwaitFalseVoid(), TaskCreationOptions.LongRunning)
				.ConfigureAwaitFalse();

			//We don't want to await it, it needs to run at the same time
			return Task.CompletedTask;
		}

		/// <inheritdoc />
		public Task StopHandlingNetworkClient(bool handleImmediately = false, bool silent = false)
		{
			//If not handling, 
			if(!isNetworkHandling)
				return Task.CompletedTask;

			isNetworkHandling = false;

			StopSilently = silent;
			CancelTokenSource.Cancel();
			
			if(handleImmediately && !silent)
				OnClientStoppedHandlingMessages();

			return Task.CompletedTask;
		}
	}
}
