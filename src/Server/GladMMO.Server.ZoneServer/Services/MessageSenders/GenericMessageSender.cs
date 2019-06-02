using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;
using JetBrains.Annotations;

namespace GladMMO
{
	public sealed class GenericMessageSender<TPayloadType> : INetworkMessageSender<GenericSingleTargetMessageContext<TPayloadType>> 
		where TPayloadType : GameServerPacketPayload
	{
		private IReadonlyEntityGuidMappable<IPeerPayloadSendService<GameServerPacketPayload>> SessionMappable { get; }

		private ILog Logger { get; }

		/// <inheritdoc />
		public GenericMessageSender([NotNull] IReadonlyEntityGuidMappable<IPeerPayloadSendService<GameServerPacketPayload>> sessionMappable, [NotNull] ILog logger)
		{
			SessionMappable = sessionMappable ?? throw new ArgumentNullException(nameof(sessionMappable));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		/// <inheritdoc />
		public void Send(GenericSingleTargetMessageContext<TPayloadType> context)
		{
			if(context == null) throw new ArgumentNullException(nameof(context));

			if(!SessionMappable.ContainsKey(context.EntityGuid))
			{
				LogNoSessionError(context.EntityGuid);
			}
			else
			{
				//We MUST unwrap the task otherwise we will not get exceptions. Which is BAD.
				//TODO: Develivery method?
				SessionMappable[context.EntityGuid].SendMessageImmediately(context.PayloadToSend)
					.ConfigureAwait(false)
					.GetAwaiter()
					.GetResult();
			}
		}

		private void LogNoSessionError([NotNull] NetworkEntityGuid guid)
		{
			if(guid == null) throw new ArgumentNullException(nameof(guid));

			if(Logger.IsErrorEnabled)
				Logger.Error($"Cannot send message to: {guid}. No session associated with it.");
		}

		/// <inheritdoc />
		public async Task SendAsync(GenericSingleTargetMessageContext<TPayloadType> context)
		{
			if(context == null) throw new ArgumentNullException(nameof(context));

			if(!SessionMappable.ContainsKey(context.EntityGuid))
			{
				LogNoSessionError(context.EntityGuid);
			}
			else
			{
				//Should we configure await false?
				//TODO: Develivery method?
				await SessionMappable[context.EntityGuid].SendMessage(context.PayloadToSend)
					.ConfigureAwait(false);
			}
		}
	}
}
