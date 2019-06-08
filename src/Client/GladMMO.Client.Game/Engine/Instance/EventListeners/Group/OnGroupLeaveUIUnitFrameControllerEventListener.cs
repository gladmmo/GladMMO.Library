using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class OnGroupLeaveUIUnitFrameControllerEventListener : EventQueueBasedTickable<IPlayerGroupLeftEventSubscribable, PlayerLeftGroupEventArgs>
	{
		private IGroupUnitFrameManager GroupUnitframeManager { get; }

		/// <inheritdoc />
		public OnGroupLeaveUIUnitFrameControllerEventListener(IPlayerGroupLeftEventSubscribable subscriptionService, 
			ILog logger,
			[NotNull] IGroupUnitFrameManager groupUnitframeManager) 
			: base(subscriptionService, true, logger)
		{
			GroupUnitframeManager = groupUnitframeManager ?? throw new ArgumentNullException(nameof(groupUnitframeManager));
		}

		/// <inheritdoc />
		protected override void HandleEvent(PlayerLeftGroupEventArgs args)
		{
			if(GroupUnitframeManager.Contains(args.PlayerGuid))
			{
				GroupUnitframeManager[args.PlayerGuid].SetElementActive(false);

				if(GroupUnitframeManager.TryReleaseUnitFrame(args.PlayerGuid) != GroupUnitFrameReleaseResult.Sucess)
					LogUnknownPlayerLeavingGroup();
			}
			else
				LogUnknownPlayerLeavingGroup();
		}

		private void LogUnknownPlayerLeavingGroup()
		{
			if(Logger.IsWarnEnabled)
				Logger.Warn($"Encountered {nameof(PlayerLeftGroupEventArgs)} without a group unitframe properly associated.");
		}
	}
}
