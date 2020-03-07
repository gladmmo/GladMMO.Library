using System; using FreecraftCore;
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

		private ISpellDataCollection SpellDataCollection { get; }

		public ActionBarButtonImageChangeEventListener(IActionBarButtonStateChangedEventSubscribable subscriptionService,
			[NotNull] ILog logger,
			[KeyFilter(UnityUIRegisterationKey.ActionBarRow1)] [NotNull] IUIActionBarRow actionBarRow,
			[NotNull] IContentIconDataCollection contentIconCollection,
			[NotNull] ISpellDataCollection spellDataCollection) 
			: base(subscriptionService, false, logger)
		{
			ActionBarRow = actionBarRow ?? throw new ArgumentNullException(nameof(actionBarRow));
			ContentIconCollection = contentIconCollection ?? throw new ArgumentNullException(nameof(contentIconCollection));
			SpellDataCollection = spellDataCollection ?? throw new ArgumentNullException(nameof(spellDataCollection));
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

					//TODO: Refactor for spell/item
					if (args.ActionType == ActionBarIndexType.Spell)
					{
						//TODO: Abstract the icon content loading
						//TODO: Don't assume we have the content icon. Throw/log better exception
						SpellDefinitionDataModel definition = SpellDataCollection.GetSpellDefinition(args.ActionId);
						ContentIconInstanceModel icon = ContentIconCollection[definition.SpellIconId];

						ProjectVersionStage.AssertAlpha();
						//TODO: Load async
						Texture2D iconTexture = Resources.Load<Texture2D>(Path.Combine("Icon", Path.GetFileNameWithoutExtension(icon.IconPathName)));
						barButton.ActionBarImageIcon.SetSpriteTexture(iconTexture);
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
