using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class InitializeGossipWindowEventListener : BaseGossipMenuCreateEventListener
	{
		private IUIGossipWindow GossipWindow { get; }

		public InitializeGossipWindowEventListener(IGossipMenuCreateEventSubscribable subscriptionService,
			[KeyFilter(UnityUIRegisterationKey.GossipWindow)] [NotNull] IUIGossipWindow gossipWindow) 
			: base(subscriptionService)
		{
			GossipWindow = gossipWindow ?? throw new ArgumentNullException(nameof(gossipWindow));
		}

		protected override void OnEventFired(object source, GossipMenuCreateEventArgs args)
		{
			//Clear the entire state of the gossip window first.
			//So we don't have left over options.
			GossipWindow.Clear();

			foreach (var menuEntry in args.GossipMenuEntries)
			{
				GossipWindow.GossipMenuButtons[menuEntry.EntryId].Text = menuEntry.MenuText;
				GossipWindow.GossipMenuButtons[menuEntry.EntryId].SetElementActive(true);
			}

			foreach(var questEntry in args.QuestMenuEntries)
			{
				//Find the first available slot.
				//Inefficient but doesn't really matter.
				IUILabeledButton button = GossipWindow.GossipQuestButtons.First(u => !u.isActive);
				button.Text = $"{questEntry.QuestId}: {questEntry.QuestTitle}";
				button.SetElementActive(true);
			}

			if (!String.IsNullOrWhiteSpace(args.Content))
				GossipWindow.GossipText.Text = args.Content;

			GossipWindow.RootElement.SetElementActive(true);
		}
	}
}
