using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using GladMMO;
using GladNet;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class SpellCastResponsePayloadHandler : BaseGameClientGameMessageHandler<SpellCastResponsePayload>
	{
		private IReadonlyNetworkTimeService TimeService { get; }

		private IReadonlyLocalPlayerDetails PlayerDetails { get; }

		public SpellCastResponsePayloadHandler(ILog logger, [NotNull] IReadonlyNetworkTimeService timeService, [NotNull] IReadonlyLocalPlayerDetails playerDetails)
			: base(logger)
		{
			TimeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
			PlayerDetails = playerDetails ?? throw new ArgumentNullException(nameof(playerDetails));
		}

		/// <inheritdoc />
		public override Task HandleMessage(IPeerMessageContext<GameClientPacketPayload> context, SpellCastResponsePayload payload)
		{
			if (!payload.isSuccessful)
			{
				if(Logger.IsDebugEnabled)
					Logger.Debug($"Failed to cast Spell: {payload.SpellId} Reason: {payload.Result}");

				return Task.CompletedTask;
			}

			//If successful we should do some prediction
			long predictedStartTime = TimeService.CurrentRemoteTime - TimeService.CurrentLatency;
			int spellId = payload.SpellId;

			//These must be set atomically.
			lock (PlayerDetails.EntityData.SyncObj)
			{
				PlayerDetails.EntityData.SetFieldValue(EntityObjectField.UNIT_FIELD_CASTING_SPELLID, spellId);
				PlayerDetails.EntityData.SetFieldValue(EntityObjectField.UNIT_FIELD_CASTING_STARTTIME, predictedStartTime);
			}

			return Task.CompletedTask;
		}
	}
}