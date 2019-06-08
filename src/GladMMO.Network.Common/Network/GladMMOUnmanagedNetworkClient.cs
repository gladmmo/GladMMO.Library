using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using Common.Logging.Simple;
using GladNet;
using Nito.AsyncEx;
using Reinterpret.Net;

namespace GladMMO
{
	/// <summary>
	/// Decorator that decorates the provided <see cref="NetworkClientBase"/> with functionality
	/// that allows you to write <see cref="TWritePayloadBaseType"/> directly into the stream/client.
	/// Overloads the usage of <see cref="Write"/> to accomplish this.
	/// </summary>
	/// <typeparam name="TClientType">The type of decorated client.</typeparam>
	/// <typeparam name="TWritePayloadBaseType"></typeparam>
	/// <typeparam name="TReadPayloadBaseType"></typeparam>
	/// <typeparam name="TPayloadConstraintType">The constraint requirement for </typeparam>
	public class GladMMOUnmanagedNetworkClient<TClientType, TReadPayloadBaseType, TWritePayloadBaseType, TPayloadConstraintType> : NetworkClientBase,
		INetworkMessageClient<TReadPayloadBaseType, TWritePayloadBaseType>
		where TClientType : NetworkClientBase
		where TReadPayloadBaseType : class, TPayloadConstraintType
		where TWritePayloadBaseType : class, TPayloadConstraintType
	{
		/// <summary>
		/// The decorated client.
		/// </summary>
		protected TClientType DecoratedClient { get; }

		/// <summary>
		/// The serializer service.
		/// </summary>
		protected INetworkSerializationService Serializer { get; }

		/// <summary>
		/// Thread specific buffer used to deserialize the packet header bytes into.
		/// </summary>
		protected byte[] PacketPayloadReadBuffer { get; }

		/// <summary>
		/// Async read syncronization object.
		/// </summary>
		protected readonly AsyncLock readSynObj = new AsyncLock();

		/// <summary>
		/// Async write syncronization object.
		/// </summary>
		protected readonly AsyncLock writeSynObj = new AsyncLock();

		private ILog Logger { get; }

		public GladMMOUnmanagedNetworkClient(TClientType decoratedClient, INetworkSerializationService serializer, ILog logger, int payloadBufferSize = 30000)
		{
			if(decoratedClient == null) throw new ArgumentNullException(nameof(decoratedClient));
			if(serializer == null) throw new ArgumentNullException(nameof(serializer));
			if(payloadBufferSize <= 0) throw new ArgumentOutOfRangeException(nameof(payloadBufferSize));

			DecoratedClient = decoratedClient;
			Serializer = serializer;
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));

			//One of the lobby packets is 14,000 bytes. We may even need bigger.
			PacketPayloadReadBuffer = new byte[payloadBufferSize]; //TODO: Do we need a larger buffer for any packets?
		}

		/// <inheritdoc />
		public override Task<bool> ConnectAsync(string address, int port)
		{
			return DecoratedClient.ConnectAsync(address, port);
		}

		/// <inheritdoc />
		public override async Task ClearReadBuffers()
		{
			using(await readSynObj.LockAsync().ConfigureAwait(false))
				await DecoratedClient.ClearReadBuffers()
					.ConfigureAwait(false);
		}

		/// <inheritdoc />
		public override Task DisconnectAsync(int delay)
		{
			return DecoratedClient.DisconnectAsync(delay);
		}

		public void Write(TWritePayloadBaseType payload)
		{
			//Write the outgoing message, it will internally create the header and it will be serialized
			WriteAsync(payload).Wait();
		}

		/// <inheritdoc />
		public override Task WriteAsync(byte[] bytes, int offset, int count)
		{
			//Assume the caller knows what they are doing.
			return CryptAndSend(bytes, offset, count);
		}

		/// <inheritdoc />
		public virtual Task WriteAsync(TWritePayloadBaseType payload)
		{
			try
			{
				//Serializer the payload first so we can build the header
				byte[] payloadData = Serializer.Serialize(payload);
				return CryptAndSend(payloadData, 0, payloadData.Length);
			}
			catch(Exception e)
			{
				string exceptionMessage = $"Encountered Exception in serializing outgoing packet Type: {payload.GetType().Name}. Exception: {e.Message}";
				if(Logger.IsErrorEnabled)
					Logger.Error(exceptionMessage);

				throw new InvalidOperationException(exceptionMessage, e);
			}
		}

		private async Task CryptAndSend(byte[] payloadData, int offset, int payloadBytesCount)
		{
			//VERY critical we lock here otherwise we could write a header and then another unrelated body could be written inbetween
			using(await writeSynObj.LockAsync().ConfigureAwait(false))
			{
				//TODO: Optimize the header size writing.
				//We skip the first 2 bytes of the payload because it contains the opcode
				//Which is suppose to be in the header. Therefore we don't wnat to write it twice
				await DecoratedClient.WriteAsync(((short)payloadBytesCount).Reinterpret(), 0, 2)
					.ConfigureAwait(false);

				//We skip the first 2 bytes of the payload because it contains the opcode
				//Which is suppose to be in the header. Therefore we don't wnat to write it twice
				await DecoratedClient.WriteAsync(payloadData, offset, payloadBytesCount)
					.ConfigureAwait(false);
			}
		}

		/// <inheritdoc />
		public override Task<int> ReadAsync(byte[] buffer, int start, int count, CancellationToken token)
		{
			return DecoratedClient.ReadAsync(buffer, start, count, token);
		}

		public virtual async Task<NetworkIncomingMessage<TReadPayloadBaseType>> ReadAsync(CancellationToken token)
		{
			using(await readSynObj.LockAsync(token).ConfigureAwait(false))
			{
				//if was canceled the header reading probably returned null anyway
				if(token.IsCancellationRequested)
					return null;

				await ReadAsync(PacketPayloadReadBuffer, 0, 2, token)
					.ConfigureAwait(false);

				//We read from the payload buffer 2 bytes, it's the size.
				int payloadSize = PacketPayloadReadBuffer.Reinterpret<short>(0);

				//If the token was canceled then the buffer isn't filled and we can't make a message
				if (token.IsCancellationRequested)
					return null;

				//We need to read enough bytes to deserialize the payload
				await ReadAsync(PacketPayloadReadBuffer, 0, payloadSize, token)
					.ConfigureAwait(false);//TODO: Should we timeout?

				//If the token was canceled then the buffer isn't filled and we can't make a message
				if(token.IsCancellationRequested)
					return null;

				//Deserialize the bytes starting from the begining but ONLY read up to the payload size. We reuse this buffer and it's large
				//so if we don't specify the length we could end up with an issue.
				var payload = Serializer.Deserialize<TReadPayloadBaseType>(PacketPayloadReadBuffer, 0, payloadSize);

				return new NetworkIncomingMessage<TReadPayloadBaseType>(new HeaderlessPacketHeader(payloadSize), payload);
			}
		}
	}
}