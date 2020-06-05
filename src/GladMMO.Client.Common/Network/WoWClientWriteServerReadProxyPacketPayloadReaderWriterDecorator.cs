using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FreecraftCore;
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
	public class WoWClientWriteServerReadProxyPacketPayloadReaderWriterDecorator<TClientType, TReadPayloadBaseType, TWritePayloadBaseType, TPayloadConstraintType> : NetworkClientBase,
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

		public WoWClientWriteServerReadProxyPacketPayloadReaderWriterDecorator(TClientType decoratedClient, INetworkSerializationService serializer, int payloadBufferSize = 30000)
		{
			if(decoratedClient == null) throw new ArgumentNullException(nameof(decoratedClient));
			if(serializer == null) throw new ArgumentNullException(nameof(serializer));
			if(payloadBufferSize <= 0) throw new ArgumentOutOfRangeException(nameof(payloadBufferSize));

			DecoratedClient = decoratedClient;
			Serializer = serializer;

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
			//We are making the assumption they are writing a full payload
			//and opcode. So we only need to serialize ushort length
			//and then the length and opcode should be encrypted
			OutgoingClientPacketHeader header = new OutgoingClientPacketHeader(count - 2, (NetworkOperationCode)bytes.Reinterpret<short>(offset));

			//We subtract 2 from the payload data length because first 2 bytes are opcode and header contains opcode.
			//Then we reinterpet the first 2 bytes of the payload data because it's the opcode we need to use.
			byte[] clientPacketHeader = Serializer.Serialize(header);

			return CryptAndSend(bytes, clientPacketHeader, offset, count);
		}

		/// <inheritdoc />
		public virtual Task WriteAsync(TWritePayloadBaseType payload)
		{
			try
			{
				//Serializer the payload first so we can build the header
				byte[] payloadData = Serializer.Serialize(payload);

				OutgoingClientPacketHeader header = new OutgoingClientPacketHeader(payloadData.Length - 2, (NetworkOperationCode)payloadData.Reinterpret<short>(0));

				//We subtract 2 from the payload data length because first 2 bytes are opcode and header contains opcode.
				//Then we reinterpet the first 2 bytes of the payload data because it's the opcode we need to use.
				byte[] clientPacketHeader = Serializer.Serialize(header);

				return CryptAndSend(payloadData, clientPacketHeader, 0, payloadData.Length);
			}
			catch(Exception e)
			{
				UnityEngine.Debug.LogError($"Encountered Exception in serializing outgoing packet Type: {payload.GetType().Name}. Exception: {e.Message}");
				throw;
			}
		}

		private async Task CryptAndSend(byte[] payloadData, byte[] clientPacketHeader, int payloadBytesOffset, int payloadBytesCount)
		{
			//VERY critical we lock here otherwise we could write a header and then another unrelated body could be written inbetween
			using(await writeSynObj.LockAsync().ConfigureAwait(false))
			{
				//It's important to always write the header first
				await DecoratedClient.WriteAsync(clientPacketHeader)
					.ConfigureAwait(false);

				//We skip the first 2 bytes of the payload because it contains the opcode
				//Which is suppose to be in the header. Therefore we don't wnat to write it twice
				await DecoratedClient.WriteAsync(payloadData, 2 + payloadBytesOffset, payloadBytesCount - 2)
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
			IPacketHeader header = null;
			TReadPayloadBaseType payload = null;

			using(await readSynObj.LockAsync(token).ConfigureAwait(false))
			{
				//Check crypto first. We may need to decrypt this header
				//This is very complicated though for reading server headers
				//since they do not have a constant size
				header = await BuildHeaderWithDecryption(token)
					.ConfigureAwait(false);

				//If the header is null it means the socket disconnected
				if(header == null)
					return null;

				//if was canceled the header reading probably returned null anyway
				if(token.IsCancellationRequested)
					return null;

				//We need to read enough bytes to deserialize the payload
				await ReadAsync(PacketPayloadReadBuffer, 0, header.PayloadSize, token)
					.ConfigureAwait(false);//TODO: Should we timeout?

				//If the token was canceled then the buffer isn't filled and we can't make a message
				if(token.IsCancellationRequested)
					return null;

				try
				{
					//Deserialize the bytes starting from the begining but ONLY read up to the payload size. We reuse this buffer and it's large
					//so if we don't specify the length we could end up with an issue.
					payload = Serializer.Deserialize<TReadPayloadBaseType>(PacketPayloadReadBuffer, 0, header.PayloadSize);
				}
				catch (Exception e)
				{
					UnityEngine.Debug.LogError($"Encountered Exception in deserializing: {typeof(TReadPayloadBaseType).Name} with OpCode: {(NetworkOperationCode)PacketPayloadReadBuffer.Reinterpret<short>()}. Reason: {e.Message}");
					throw;
				}
			}

			//TODO: This is bad for performance because it copies of the buffer, only use this during dev.
			//HelloKitty: Kinda a hack here to add logging for unknown/default opcodes.
			if(payload is UnknownGamePayload)
				return new NetworkIncomingMessage<TReadPayloadBaseType>(header, new LoggableUnknownOpcodePayload(header.PacketSize, (NetworkOperationCode)PacketPayloadReadBuffer.Reinterpret<short>(), PacketPayloadReadBuffer.Skip(2).ToArray()) as TReadPayloadBaseType);

			return new NetworkIncomingMessage<TReadPayloadBaseType>(header, payload);
		}

		protected virtual async Task<IPacketHeader> BuildHeaderWithDecryption(CancellationToken token)
		{
			//Read first byte so we can see packet type
			await ReadAsync(PacketPayloadReadBuffer, 0, 1, token)
				.ConfigureAwait(false);

			byte firstByte = PacketPayloadReadBuffer[0];

			//Once we get the first byte we can determine the incoming header type
			if((firstByte & 0x80) != 0)
			{
				//If it's NOT 0 then it's a large packet header
				//We need to read an additional 2 bytes
				await ReadAsync(PacketPayloadReadBuffer, 0, 2, token)
					.ConfigureAwait(false);

				return new ServerPacketHeader(IncomingClientLargePacketHeader.DecodePacketSize(firstByte, PacketPayloadReadBuffer));
			}
			else
			{
				//otherwise it's a small header so only one more byte is needed
				//If it's NOT 0 then it's a large packet header
				//We need to read an additional 2 bytes
				await ReadAsync(PacketPayloadReadBuffer, 0, 1, token)
					.ConfigureAwait(false);

				return new ServerPacketHeader(IncomingClientSmallPacketHeader.DecodePacketSize(firstByte, PacketPayloadReadBuffer[0]));
			}
		}
	}
}