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
	public sealed class RemoveLocalPlayerAppliedAuraEventListener : RemoveAppliedAuraFromUIEventListener
	{
		private IReadonlyLocalPlayerDetails PlayerDetails { get; }

		public RemoveLocalPlayerAppliedAuraEventListener(IAuraApplicationRemovedEventSubscribable subscriptionService,
			[KeyFilter(UnityUIRegisterationKey.LocalPlayerAuraBuffCollection)] [NotNull] IUIAuraBuffCollection auraBuffUiCollection,
			IReadonlyLocalPlayerDetails playerDetails, 
			ILog logger) 
			: base(subscriptionService, auraBuffUiCollection, logger)
		{
			PlayerDetails = playerDetails;
		}

		protected override bool IsHandlingTarget(ObjectGuid target)
		{
			return PlayerDetails.LocalPlayerGuid == target;
		}
	}
}
