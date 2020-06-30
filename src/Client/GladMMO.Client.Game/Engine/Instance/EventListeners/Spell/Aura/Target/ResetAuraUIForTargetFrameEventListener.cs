using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Common.Logging;
using Glader.Essentials;

namespace GladMMO
{
	//When the target changes the UI for the target unit frame's buffs/auras are WRONG
	//so we must turn them all off and also service them and reactivate them all... which is quite annoying.
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class ResetAuraUIForTargetFrameEventListener : LocalPlayerTargetChangedEventListener
	{
		private IUIAuraBuffCollection TargetUIAuraBuffCollection { get; }

		public ResetAuraUIForTargetFrameEventListener(ILocalPlayerTargetChangedEventListener subscriptionService, 
			ILog logger,
			[KeyFilter(UnityUIRegisterationKey.TargetUnitFrame)] IUIUnitFrame targetUnitFrame,
			[KeyFilter(UnityUIRegisterationKey.TargetAuraBuffCollection)] [NotNull] IUIAuraBuffCollection targetUiAuraBuffCollection) 
			: base(subscriptionService, logger, targetUnitFrame)
		{
			TargetUIAuraBuffCollection = targetUiAuraBuffCollection ?? throw new ArgumentNullException(nameof(targetUiAuraBuffCollection));
		}

		protected override void OnLocalPlayerTargetChanged(LocalPlayerTargetChangedEventArgs args)
		{
			foreach (IUIAuraBuffSlot slot in TargetUIAuraBuffCollection.EnumerateActive())
				slot.RootElement.SetElementActive(false);
		}
	}
}
