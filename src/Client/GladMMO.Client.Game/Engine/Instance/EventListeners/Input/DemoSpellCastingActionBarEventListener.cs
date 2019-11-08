using System;
using System.Collections.Generic;
using System.Text;
using GladNet;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class DemoSpellCastingActionBarEventListener : BaseActionBarButtonPressedEventListener
	{
		private IPeerPayloadSendService<GameClientPacketPayload> SendService { get; }

		public DemoSpellCastingActionBarEventListener(IActionBarButtonPressedEventSubscribable subscriptionService,
			[NotNull] IPeerPayloadSendService<GameClientPacketPayload> sendService) 
			: base(subscriptionService)
		{
			SendService = sendService ?? throw new ArgumentNullException(nameof(sendService));
		}

		protected override void OnActionBarButtonPressed(ActionBarIndex index)
		{
			switch (index)
			{
				case ActionBarIndex.ActionBarIndex_01:
					SendService.SendMessage(new SpellCastRequestPayload(1));
					break;
				case ActionBarIndex.ActionBarIndex_02:
					SendService.SendMessage(new SpellCastRequestPayload(3));
					break;
				case ActionBarIndex.ActionBarIndex_03:
					SendService.SendMessage(new SpellCastRequestPayload(4));
					break;
				case ActionBarIndex.ActionBarIndex_04:
					SendService.SendMessage(new SpellCastRequestPayload(5));
					break;
				case ActionBarIndex.ActionBarIndex_05:
					break;
				case ActionBarIndex.ActionBarIndex_06:
					break;
				case ActionBarIndex.ActionBarIndex_07:
					break;
				case ActionBarIndex.ActionBarIndex_08:
					break;
				case ActionBarIndex.ActionBarIndex_09:
					break;
				case ActionBarIndex.ActionBarIndex_10:
					break;
				case ActionBarIndex.ActionBarIndex_11:
					break;
				case ActionBarIndex.ActionBarIndex_12:
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(index), index, null);
			}
		}
	}
}
