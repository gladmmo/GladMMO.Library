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
	public sealed class DisplayLocalPlayerAppliedAuraEventListener : DisplayAppliedAuraEventListener
	{
		private IReadonlyLocalPlayerDetails PlayerDetails { get; }

		public DisplayLocalPlayerAppliedAuraEventListener(IAuraApplicationAppliedEventSubscribable subscriptionService, 
			IAuraApplicationUpdatedEventSubscribable secondarySubscriptionService,
			[KeyFilter(UnityUIRegisterationKey.LocalPlayerAuraBuffCollection)] IUIAuraBuffCollection auraBuffUiCollection, 
			ILog logger,
			[NotNull] IReadonlyLocalPlayerDetails playerDetails) 
			: base(subscriptionService, secondarySubscriptionService, auraBuffUiCollection, logger)
		{
			PlayerDetails = playerDetails ?? throw new ArgumentNullException(nameof(playerDetails));
		}

		protected override bool IsHandlingTarget(ObjectGuid target)
		{
			return PlayerDetails.LocalPlayerGuid == target;
		}
	}
}
