using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GladNet;

namespace GladMMO
{
	public interface IEntitySessionMessageSender
	{
		/// <summary>
		/// Sends the message to the specified entity.
		/// </summary>
		/// <param name="entityGuid"></param>
		/// <param name="payload"></param>
		/// <returns></returns>
		Task<SendResult> SendMessageAsync(ObjectGuid entityGuid, GameServerPacketPayload payload);
	}

	//TODO: Extract
	public sealed class DefaultEntitySessionMessageSender : IEntitySessionMessageSender
	{
		private IReadonlyEntityGuidMappable<IPeerPayloadSendService<GameServerPacketPayload>> SessionMappable { get; }

		public DefaultEntitySessionMessageSender([NotNull] IReadonlyEntityGuidMappable<IPeerPayloadSendService<GameServerPacketPayload>> sessionMappable)
		{
			SessionMappable = sessionMappable ?? throw new ArgumentNullException(nameof(sessionMappable));
		}

		public Task<SendResult> SendMessageAsync(ObjectGuid entityGuid, GameServerPacketPayload payload)
		{
			//TODO: We need to make broadcasting more efficient.
			//We can expect it to sometimes be null. If the client disconnected in the middle of sending it a message
			//or something.
			var payloadSendService = SessionMappable[entityGuid];
			if (payloadSendService == null)
				return Task.FromResult(SendResult.Error);

			return payloadSendService.SendMessage(payload);
		}
	}
}
