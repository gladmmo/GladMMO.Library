using System;
using System.Collections.Generic;
using System.Text;
using GladNet;

namespace GladMMO
{
	public class DefaultPlayerEntityActorState : NetworkedObjectActorState
	{
		private IPeerPayloadSendService<GameServerPacketPayload> SendService { get; }

		public DefaultPlayerEntityActorState(IEntityDataFieldContainer entityData, NetworkEntityGuid entityGuid, InterestCollection interest, [NotNull] IPeerPayloadSendService<GameServerPacketPayload> sendService) 
			: base(entityData, entityGuid, interest)
		{
			SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));
		}
	}
}
