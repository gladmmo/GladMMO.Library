using System;
using System.Collections.Generic;
using System.Text;
using GladNet;

namespace GladMMO
{
	[EntityActorMessageHandler(typeof(DefaultPlayerEntityActor))]
	public sealed class BroadcastToInterestSetMessageHandler : BaseEntityActorMessageHandler<NetworkedObjectActorState, BroadcastToInterestSetMessage>
	{
		private IReadonlyEntityGuidMappable<IPeerPayloadSendService<GameServerPacketPayload>> SendServiceMappable { get; }

		public BroadcastToInterestSetMessageHandler([NotNull] IReadonlyEntityGuidMappable<IPeerPayloadSendService<GameServerPacketPayload>> sendServiceMappable)
		{
			SendServiceMappable = sendServiceMappable ?? throw new ArgumentNullException(nameof(sendServiceMappable));
		}

		protected override void HandleMessage(EntityActorMessageContext messageContext, NetworkedObjectActorState state, BroadcastToInterestSetMessage message)
		{
			IPeerPayloadSendService<GameServerPacketPayload> sender = null;

			foreach (ObjectGuid guid in state.Interest.ContainedEntities)
			{
				//If it's not a player or the message specified that we shouldn't send to self and it's ourselves
				if (guid.EntityType != EntityType.Player || !message.SendToSelf && guid == state.EntityGuid)
					continue;

				//Try to get the sender and then just forward the message.
				if (SendServiceMappable.TryGetValue(guid, out sender))
					sender.SendMessage(message.Message);
			}
		}
	}
}
