using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Autofac.Features.AttributeFilters;
using Common.Logging;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class ActionBarButtonImageChangeEventListener : EventQueueBasedTickable<IActionBarButtonStateChangedEventSubscribable, ActionBarButtonStateChangedEventArgs>
	{
		//TODO: Support multiple actionbars.
		private IUIActionBarRow ActionBarRow { get; }

		private IContentIconDataCollection ContentIconCollection { get; }

		public ActionBarButtonImageChangeEventListener(IActionBarButtonStateChangedEventSubscribable subscriptionService,
			[NotNull] ILog logger,
			[KeyFilter(UnityUIRegisterationKey.ActionBarRow1)] [NotNull] IUIActionBarRow actionBarRow,
			[NotNull] IContentIconDataCollection contentIconCollection) 
			: base(subscriptionService, false, logger)
		{
			ActionBarRow = actionBarRow ?? throw new ArgumentNullException(nameof(actionBarRow));
			ContentIconCollection = contentIconCollection ?? throw new ArgumentNullException(nameof(contentIconCollection));
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

					//TODO: Abstract the icon content loading
					//TODO: Don't assume we have the content icon. Throw/log better exception
					ContentIconInstanceModel icon = ContentIconCollection[args.ActionId];

					ProjectVersionStage.AssertAlpha();
					//TODO: Load async
					Texture2D iconTexture = Resources.Load<Texture2D>(Path.Combine("Icon", icon.IconPathName));
					barButton.ActionBarImageIcon.SetSpriteTexture(iconTexture);
				}
			}
		}
	}
}
