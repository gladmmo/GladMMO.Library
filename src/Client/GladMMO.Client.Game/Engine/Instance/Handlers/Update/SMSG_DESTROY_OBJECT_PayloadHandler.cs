using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using FreecraftCore;
using GladNet;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class SMSG_DESTROY_OBJECT_PayloadHandler : BaseGameClientGameMessageHandler<SMSG_DESTROY_OBJECT_Payload>
	{
		public INetworkEntityVisibilityLostEventPublisher VisibilityLostPublisher { get; }

		public SMSG_DESTROY_OBJECT_PayloadHandler(ILog logger, [NotNull] INetworkEntityVisibilityLostEventPublisher visibilityLostPublisher)
			: base(logger)
		{
			VisibilityLostPublisher = visibilityLostPublisher ?? throw new ArgumentNullException(nameof(visibilityLostPublisher));
		}

		public override Task HandleMessage(IPeerMessageContext<GamePacketPayload> context, SMSG_DESTROY_OBJECT_Payload payload)
		{
			//Only log info for players.
			if(Logger.IsInfoEnabled && payload.DestroyedObject.isType(EntityTypeId.TYPEID_PLAYER))
				Logger.Info($"Entity: {payload.DestroyedObject} SMSG_DESTROY_OBJECT Death: {payload.IsForDeath}");

			VisibilityLostPublisher.PublishEvent(this, new NetworkEntityVisibilityLostEventArgs(payload.DestroyedObject));
			return Task.CompletedTask;
		}
	}
}
