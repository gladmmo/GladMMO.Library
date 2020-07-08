using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class ResetLocalPlayerMenuStateEventListener : BaseGossipMenuCreateEventListener
	{
		private LocalPlayerMenuState MenuState { get; }

		public ResetLocalPlayerMenuStateEventListener(IGossipMenuCreateEventSubscribable subscriptionService,
			[NotNull] LocalPlayerMenuState menuState) 
			: base(subscriptionService)
		{
			MenuState = menuState ?? throw new ArgumentNullException(nameof(menuState));
		}

		protected override void OnEventFired(object source, GossipMenuCreateEventArgs args)
		{
			MenuState.Clear();
			MenuState.Update(args.GossipSource, args.GossipMenuEntries, args.QuestMenuEntries);
		}
	}
}
