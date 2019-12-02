using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Common.Logging;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class ActionBarButtonImageChangeEventListener : EventQueueBasedTickable<IActionBarButtonStateChangedEventSubscribable, ActionBarButtonStateChangedEventArgs>
	{
		//TODO: Support multiple actionbars.
		private IUIActionBarRow ActionBarRow { get; }

		public ActionBarButtonImageChangeEventListener(IActionBarButtonStateChangedEventSubscribable subscriptionService,
			[NotNull] ILog logger,
			[KeyFilter(UnityUIRegisterationKey.ActionBarRow1)] [NotNull] IUIActionBarRow actionBarRow) 
			: base(subscriptionService, false, logger)
		{
			ActionBarRow = actionBarRow ?? throw new ArgumentNullException(nameof(actionBarRow));
		}

		protected override void HandleEvent(ActionBarButtonStateChangedEventArgs args)
		{
			//TODO: Check actionbar index and handle multiple rows.
			if (ActionBarRow.ContainsKey(args.Index))
			{
				IUIActionBarButton barButton = ActionBarRow[args.Index];

				if (args.ActionType == ActionBarIndexType.Empty)
				{
					barButton.SetElementActive(false);
				}
				else
				{
					barButton.SetElementActive(true);

					//TODO: Set the action bar icon image.
					if(Logger.IsWarnEnabled)
						Logger.Warn($"TODO: Icon handling for actionbar.");
				}
			}
		}
	}
}
