using System; using FreecraftCore;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Autofac.Features.AttributeFilters;
using Common.Logging;
using Glader.Essentials;
using Nito.AsyncEx;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class ActionBarButtonImageChangeEventListener : EventQueueBasedTickable<IActionBarButtonStateChangedEventSubscribable, ActionBarButtonStateChangedEventArgs>
	{
		//TODO: Support multiple actionbars.
		private IUIActionBarRow ActionBarRow { get; }

		private IClientDataCollectionContainer ClientData { get; }

		private IAddressableContentLoader ContentLoadService { get; }

		public ActionBarButtonImageChangeEventListener(IActionBarButtonStateChangedEventSubscribable subscriptionService,
			[NotNull] ILog logger,
			[KeyFilter(UnityUIRegisterationKey.ActionBarRow1)] [NotNull] IUIActionBarRow actionBarRow,
			[NotNull] IClientDataCollectionContainer clientData,
			[NotNull] IAddressableContentLoader contentLoadService) 
			: base(subscriptionService, false, logger)
		{
			ActionBarRow = actionBarRow ?? throw new ArgumentNullException(nameof(actionBarRow));
			ClientData = clientData ?? throw new ArgumentNullException(nameof(clientData));
			ContentLoadService = contentLoadService ?? throw new ArgumentNullException(nameof(contentLoadService));
		}

		protected override void HandleEvent(ActionBarButtonStateChangedEventArgs args)
		{
			//TODO: Check actionbar index and handle multiple rows.
			if (ActionBarRow.ContainsKey(args.Index))
			{
				IUIActionBarButton barButton = ActionBarRow[args.Index];

				//TODO: Change way we handle EMPTY/remove
				/*if (args.ActionType == ActionBarIndexType.Empty)
				{
					barButton.SetElementActive(false);
				}
				else*/
				{
					barButton.SetElementActive(true);

					//TODO: Refactor for spell/item
					if (args.ActionType == ActionButtonType.ACTION_BUTTON_SPELL)
					{
						ProjectVersionStage.AssertAlpha();

						//Check the SPELL DBC
						SpellEntry<string> spellEntry = ClientData.AssertEntry<SpellEntry<string>>(args.ActionId);
						SpellIconEntry<string> iconEntry = ClientData.AssertEntry<SpellIconEntry<string>>((int) spellEntry.SpellIconID);

						UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
						{
							Texture2D icon = await ContentLoadService.LoadContentAsync<Texture2D>(iconEntry.TextureFileName);

							barButton.ActionBarImageIcon.SetSpriteTexture(icon);
						});
					}
					else
					{
						throw new InvalidOperationException($"TODO: Implement empty/item action bar support");
					}
				}
			}
		}
	}
}
