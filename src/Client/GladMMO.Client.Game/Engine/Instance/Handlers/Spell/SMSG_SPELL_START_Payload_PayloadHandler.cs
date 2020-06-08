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
	[AdditionalRegisterationAs(typeof(ILocalPlayerSpellCastingStateChangedEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class SMSG_SPELL_START_Payload_PayloadHandler : BaseGameClientGameMessageHandler<SMSG_SPELL_START_Payload>, ILocalPlayerSpellCastingStateChangedEventSubscribable
	{
		public event EventHandler<SpellCastingStateChangedEventArgs> OnSpellCastingStateChanged;

		private ILocalPlayerDetails PlayerDetails { get; }

		/// <inheritdoc />
		public SMSG_SPELL_START_Payload_PayloadHandler(ILog logger, [NotNull] ILocalPlayerDetails playerDetails)
			: base(logger)
		{
			PlayerDetails = playerDetails ?? throw new ArgumentNullException(nameof(playerDetails));
		}

		public override Task HandleMessage(IPeerMessageContext<GamePacketPayload> context, SMSG_SPELL_START_Payload payload)
		{
			if(payload.CastData.SpellSource == PlayerDetails.LocalPlayerGuid)
				OnSpellCastingStateChanged?.Invoke(this, new SpellCastingStateChangedEventArgs(payload.CastData.SpellId, (int)payload.CastData.TimeDiff));

			return Task.CompletedTask;
		}
	}
}