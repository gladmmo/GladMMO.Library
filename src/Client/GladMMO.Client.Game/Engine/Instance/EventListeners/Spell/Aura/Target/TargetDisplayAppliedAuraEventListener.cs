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
	public sealed class TargetDisplayAppliedAuraEventListener : DisplayAppliedAuraEventListener
	{
		private IReadonlyLocalPlayerDetails PlayerDetails { get; }

		public TargetDisplayAppliedAuraEventListener(IAuraApplicationAppliedEventSubscribable subscriptionService, 
			IAuraApplicationUpdatedEventSubscribable secondarySubscriptionService,
			[KeyFilter(UnityUIRegisterationKey.TargetAuraBuffCollection)] IUIAuraBuffCollection auraBuffUiCollection, 
			ILog logger,
			[NotNull] IReadonlyLocalPlayerDetails playerDetails) 
			: base(subscriptionService, secondarySubscriptionService, auraBuffUiCollection, logger)
		{
			PlayerDetails = playerDetails ?? throw new ArgumentNullException(nameof(playerDetails));
		}

		protected override bool IsHandlingTarget([NotNull] ObjectGuid target)
		{
			if (target == null) throw new ArgumentNullException(nameof(target));

			return PlayerDetails.EntityData.GetEntityGuidValue(EUnitFields.UNIT_FIELD_TARGET) == target;
		}
	}
}
