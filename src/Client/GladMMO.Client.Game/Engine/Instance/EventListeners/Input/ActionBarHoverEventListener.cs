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
	public sealed class ActionBarHoverEventListener : BaseActionBarButtonStateChangedEventListener
	{
		/// <summary>
		/// This set manages if we've already rigged up callbacks for the index type.
		/// Exists for the purposes of LAZY initialized hover rigging.
		/// </summary>
		private HashSet<ActionBarIndex> SetIndex { get; } = new HashSet<ActionBarIndex>();

		private IActionBarCollection ActionBarDataCollection { get; }

		//TODO: Support multiple actionbars.
		private IUIActionBarRow ActionBarRow { get; }

		private ILog Logger { get; }

		private IActionBarMouseOverStateChangeEventPublisher ActionBarMouseOverPublisher { get; }

		public ActionBarHoverEventListener(IActionBarButtonStateChangedEventSubscribable subscriptionService,
			[NotNull] IActionBarCollection actionBarDataCollection,
			[KeyFilter(UnityUIRegisterationKey.ActionBarRow1)] [NotNull] IUIActionBarRow actionBarRow,
			[NotNull] ILog logger,
			[NotNull] IActionBarMouseOverStateChangeEventPublisher actionBarMouseOverPublisher) 
			: base(subscriptionService)
		{
			ActionBarDataCollection = actionBarDataCollection ?? throw new ArgumentNullException(nameof(actionBarDataCollection));
			ActionBarRow = actionBarRow ?? throw new ArgumentNullException(nameof(actionBarRow));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			ActionBarMouseOverPublisher = actionBarMouseOverPublisher ?? throw new ArgumentNullException(nameof(actionBarMouseOverPublisher));
		}

		protected override void OnEventFired(object source, ActionBarButtonStateChangedEventArgs args)
		{
			//We already setup a callback
			if (SetIndex.Contains(args.Index))
				return;

			//Possible we load multi-action bars not yet implemented in the client.
			if (!ActionBarRow.ContainsKey(args.Index))
			{
				if(Logger.IsWarnEnabled)
					Logger.Warn($"ActionBar event for Index: {args.Index} but does not map to UI element.");
				return;
			}

			IUIActionBarButton button = ActionBarRow[args.Index];

			//It's ok to register this in a way that makes the callback unremovable since we only reigster it once
			button.OnActionBarMouseOverChanged += (sender, state) =>
			{
				//Empty bar, do nothing for mouse over
				if (!ActionBarDataCollection.IsSet(args.Index))
					return;

				CharacterActionBarInstanceModel data = ActionBarDataCollection[args.Index];
				ActionBarMouseOverPublisher.PublishEvent(this, new ActionBarMouseOverStateChangeEventArgs(data.ActionId, data.Type, state));
			};

			SetIndex.Add(args.Index);
		}
	}
}
