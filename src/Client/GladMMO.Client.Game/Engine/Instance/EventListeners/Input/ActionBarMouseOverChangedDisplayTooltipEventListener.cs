using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Common.Logging;
using FreecraftCore;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class ActionBarMouseOverChangedDisplayTooltipEventListener : BaseSingleEventListenerInitializable<IActionBarMouseOverStateChangeEventSubscribable, ActionBarMouseOverStateChangeEventArgs>
	{
		private IClientDataCollectionContainer ClientData { get; }

		private IUISpellTooltip StaticSpellTooltip { get; }

		private ILog Logger { get; }

		public ActionBarMouseOverChangedDisplayTooltipEventListener(IActionBarMouseOverStateChangeEventSubscribable subscriptionService,
			[NotNull] IClientDataCollectionContainer clientData,
			[KeyFilter(UnityUIRegisterationKey.StaticSpellTooltip)] [NotNull] IUISpellTooltip staticSpellTooltip,
			[NotNull] ILog logger) 
			: base(subscriptionService)
		{
			ClientData = clientData ?? throw new ArgumentNullException(nameof(clientData));
			StaticSpellTooltip = staticSpellTooltip ?? throw new ArgumentNullException(nameof(staticSpellTooltip));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void OnEventFired(object source, ActionBarMouseOverStateChangeEventArgs args)
		{
			//Simple case, it's just the mouse
			//leaving the action bar so we only need to disable tooltip
			if (!args.IsMousedOver)
			{
				StaticSpellTooltip.SetActive(false);
				return;
			}

			if (args.ActionType != ActionButtonType.ACTION_BUTTON_SPELL)
			{
				if(Logger.IsWarnEnabled)
					Logger.Warn($"ActionType: {args.ActionType} not implemented for mouse over tooltip.");

				StaticSpellTooltip.SetActive(false);
				return;
			}

			SetSpellTooltipData(args);

			StaticSpellTooltip.SetActive(true);
		}

		private void SetSpellTooltipData(ActionBarMouseOverStateChangeEventArgs args)
		{
			SpellEntry<string> entry = ClientData.AssertEntry<SpellEntry<string>>(args.ActionId);

			StaticSpellTooltip.SetText(SpellTooltipInfoSlot.Name, entry.SpellName.enUS);
			StaticSpellTooltip.SetSlotState(SpellTooltipInfoSlot.Name, true);

			StaticSpellTooltip.SetText(SpellTooltipInfoSlot.Info, entry.Description.enUS);
			StaticSpellTooltip.SetSlotState(SpellTooltipInfoSlot.Info, true);

			if (!string.IsNullOrEmpty(entry.Rank.enUS))
			{
				StaticSpellTooltip.SetText(SpellTooltipInfoSlot.Rank, entry.Rank.enUS);
				StaticSpellTooltip.SetSlotState(SpellTooltipInfoSlot.Rank, true);
			}
			else
				StaticSpellTooltip.SetSlotState(SpellTooltipInfoSlot.Rank, false);
		}
	}
}
