using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Common.Logging;
using FreecraftCore;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class RemoveTargetPlayerAppliedAuraEventListener : RemoveAppliedAuraFromUIEventListener
	{
		private IReadonlyLocalPlayerDetails PlayerDetails { get; }

		public RemoveTargetPlayerAppliedAuraEventListener(IAuraApplicationRemovedEventSubscribable subscriptionService, 
			[KeyFilter(UnityUIRegisterationKey.TargetAuraBuffCollection)] IUIAuraBuffCollection auraBuffUiCollection, 
			ILog logger,
			[NotNull] IReadonlyLocalPlayerDetails playerDetails) 
			: base(subscriptionService, auraBuffUiCollection, logger)
		{
			PlayerDetails = playerDetails ?? throw new ArgumentNullException(nameof(playerDetails));
		}

		protected override bool IsHandlingTarget(ObjectGuid target)
		{
			//Does this match our target guid??
			return PlayerDetails.EntityData.GetEntityGuidValue(EUnitFields.UNIT_FIELD_TARGET) == target;
		}
	}
}
