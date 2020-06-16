using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Glader.Essentials;

namespace GladMMO
{
	public abstract class OnGroupJoinedEventListener : EventQueueBasedTickable<IPlayerGroupJoinedEventSubscribable, PlayerJoinedGroupEventArgs>
	{
		protected IGroupUnitFrameManager GroupUnitFrameManager { get; }

		protected OnGroupJoinedEventListener(IPlayerGroupJoinedEventSubscribable subscriptionService, 
			ILog logger,
			[NotNull] IGroupUnitFrameManager groupUnitFrameManager) 
			: base(subscriptionService, true, logger)
		{
			GroupUnitFrameManager = groupUnitFrameManager ?? throw new ArgumentNullException(nameof(groupUnitFrameManager));
		}

		protected override void HandleEvent(PlayerJoinedGroupEventArgs args)
		{
			//Ensure we claimed a unit frame
			if (GroupUnitFrameManager.TryClaimUnitFrame(args.PlayerGuid) == GroupUnitFrameIssueResult.Success)
				GroupUnitFrameManager[args.PlayerGuid].SetElementActive(true);

			OnGroupJoined(args);
		}

		protected abstract void OnGroupJoined(PlayerJoinedGroupEventArgs joinArgs);
	}
}
