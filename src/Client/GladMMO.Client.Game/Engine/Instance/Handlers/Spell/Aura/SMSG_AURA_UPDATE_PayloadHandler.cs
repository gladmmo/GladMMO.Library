using System;
using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using GladMMO;
using GladNet;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class SMSG_AURA_UPDATE_PayloadHandler : BaseGameClientGameMessageHandler<SMSG_AURA_UPDATE_Payload>
	{
		private IAuraStateChangedEventPublisher AuraPublisher { get; }

		/// <inheritdoc />
		public SMSG_AURA_UPDATE_PayloadHandler(ILog logger,
			[NotNull] IAuraStateChangedEventPublisher auraPublisher)
			: base(logger)
		{
			AuraPublisher = auraPublisher ?? throw new ArgumentNullException(nameof(auraPublisher));
		}

		/// <inheritdoc />
		public override Task HandleMessage(IPeerMessageContext<GamePacketPayload> context, SMSG_AURA_UPDATE_Payload payload)
		{
			AuraPublisher.PublishEvent(this, new AuraStateChangedEventArgs(payload.TargetGuid, payload.Data));
			return Task.CompletedTask;
		}
	}
}