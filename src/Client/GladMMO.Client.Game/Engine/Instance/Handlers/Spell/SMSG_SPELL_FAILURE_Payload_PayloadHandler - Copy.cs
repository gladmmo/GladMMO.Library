using System; using FreecraftCore;
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
	public sealed class SMSG_SPELL_GO_Payload_PayloadHandler : BaseGameClientGameMessageHandler<SMSG_SPELL_GO_Payload>
	{
		private ILocalPlayerSpellCastingStateChangedEventPublisher SpellCastingStatePublisher { get; }

		private ILocalPlayerDetails PlayerDetails { get; }

		/// <inheritdoc />
		public SMSG_SPELL_GO_Payload_PayloadHandler(ILog logger, [NotNull] ILocalPlayerDetails playerDetails,
			[NotNull] ILocalPlayerSpellCastingStateChangedEventPublisher spellCastingStatePublisher)
			: base(logger)
		{
			PlayerDetails = playerDetails ?? throw new ArgumentNullException(nameof(playerDetails));
			SpellCastingStatePublisher = spellCastingStatePublisher ?? throw new ArgumentNullException(nameof(spellCastingStatePublisher));
		}

		public override Task HandleMessage(IPeerMessageContext<GamePacketPayload> context, SMSG_SPELL_GO_Payload payload)
		{
			if(payload.CastData.SpellSource == PlayerDetails.LocalPlayerGuid)
				SpellCastingStatePublisher.PublishEvent(this, new SpellCastingStateChangedEventArgs(payload.CastData.SpellId, (int)payload.CastData.TimeDiff, SpellCastingEventChangeState.Stopped, payload.CastData.CalculateTemporallyUniqueKey()));

			return Task.CompletedTask;
		}
	}
}