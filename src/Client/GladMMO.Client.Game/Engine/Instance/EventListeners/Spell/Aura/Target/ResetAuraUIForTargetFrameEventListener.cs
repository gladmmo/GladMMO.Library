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

		private IReadonlyEntityGuidMappable<IAuraApplicationCollection> AuraApplicationMappable { get; }

		private IEnumerable<IAuraDataUpdateApplyable> AuraDataUpdateApplyable { get; }

		public ResetAuraUIForTargetFrameEventListener(ILocalPlayerTargetChangedEventListener subscriptionService, 
			ILog logger,
			[KeyFilter(UnityUIRegisterationKey.TargetUnitFrame)] IUIUnitFrame targetUnitFrame,
			[KeyFilter(UnityUIRegisterationKey.TargetAuraBuffCollection)] [NotNull] IUIAuraBuffCollection targetUiAuraBuffCollection,
			[NotNull] IReadonlyEntityGuidMappable<IAuraApplicationCollection> auraApplicationMappable,
			[NotNull] IEnumerable<IAuraDataUpdateApplyable> auraDataUpdateApplyable)
			: base(subscriptionService, logger, targetUnitFrame)
		{
			TargetUIAuraBuffCollection = targetUiAuraBuffCollection ?? throw new ArgumentNullException(nameof(targetUiAuraBuffCollection));
			AuraApplicationMappable = auraApplicationMappable ?? throw new ArgumentNullException(nameof(auraApplicationMappable));
			AuraDataUpdateApplyable = auraDataUpdateApplyable ?? throw new ArgumentNullException(nameof(auraDataUpdateApplyable));
		}

		protected override void OnLocalPlayerTargetChanged(LocalPlayerTargetChangedEventArgs args)
		{
			//Disables ALL the elements.
			foreach (IUIAuraBuffSlot slot in TargetUIAuraBuffCollection.EnumerateActive())
				slot.RootElement.SetElementActive(false);

			IAuraApplicationCollection applicationCollection = AuraApplicationMappable.RetrieveEntity(args.TargetedEntity);

			//This will enable ALL the slots that should be active from the actual entity
			//application collection.
			foreach (var entry in applicationCollection)
			{
				if (entry.Data.IsAuraRemoved)
					continue;

				AuraApplicationUpdatedEventArgs spoofEventArgs = new AuraApplicationUpdatedEventArgs(entry.Data.SlotIndex, args.TargetedEntity, entry.Data.AuraSpellId, entry.Data.State);

				foreach(var applyable in AuraDataUpdateApplyable)
					applyable.ApplyAuraData(spoofEventArgs);
			}
		}
	}
}
