using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using GladNet;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class DemoSpellCastingActionBarEventListener : BaseActionBarButtonPressedEventListener
	{
		private IPeerPayloadSendService<GamePacketPayload> SendService { get; }

		private IReadonlyActionBarCollection ActionBarCollection { get; }

		private ILog Logger { get; }

		private ILocalPlayerDetails PlayerDetails { get; }

		private byte CastIncrement = 1;

		public DemoSpellCastingActionBarEventListener(IActionBarButtonPressedEventSubscribable subscriptionService,
			[NotNull] IPeerPayloadSendService<GamePacketPayload> sendService,
			[NotNull] IReadonlyActionBarCollection actionBarCollection,
			[NotNull] ILog logger,
			[NotNull] ILocalPlayerDetails playerDetails) 
			: base(subscriptionService)
		{
			SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));
			ActionBarCollection = actionBarCollection ?? throw new ArgumentNullException(nameof(actionBarCollection));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			PlayerDetails = playerDetails ?? throw new ArgumentNullException(nameof(playerDetails));
		}

		protected override void OnActionBarButtonPressed(ActionBarIndex index)
		{
			//If we have a set index then we can just send the request to interact/cast
			if (ActionBarCollection.IsSet(index))
			{
				if(Logger.IsDebugEnabled)
					Logger.Debug($"Action bar Index: {index} pressed.");

				Logger.Error($"TODO REIMPLEMENT SPELL CASTING");
				if (ActionBarCollection[index].Type == ActionButtonType.ACTION_BUTTON_SPELL)
				{
					//We try to get a target
					ObjectGuid target = PlayerDetails.EntityData.GetEntityGuidValue(EUnitFields.UNIT_FIELD_TARGET);
					if(target.isEmpty())
						SendService.SendMessage(new CMSG_CAST_SPELL_Payload(CalculateNewCastCount() , ActionBarCollection[index].ActionId, SpellTargetInfo.CreateSingleTargetUnitCast(PlayerDetails.LocalPlayerGuid)));
					else
						SendService.SendMessage(new CMSG_CAST_SPELL_Payload(CalculateNewCastCount(), ActionBarCollection[index].ActionId, SpellTargetInfo.CreateSingleTargetUnitCast(target)));
				}
			}
			else
			{
				if(Logger.IsDebugEnabled)
					Logger.Debug($"Action bar Index: {index} pressed but no associated action.");
			}
		}

		private byte CalculateNewCastCount()
		{
			return CastIncrement = (byte) ((CastIncrement + 1) % 255);
		}
	}
}
