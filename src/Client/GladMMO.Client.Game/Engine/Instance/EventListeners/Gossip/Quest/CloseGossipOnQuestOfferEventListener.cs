using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class CloseGossipOnQuestOfferEventListener : BaseQuestWindowCreateEventListener
	{
		public IUIGossipWindow GossipWindow { get; }

		public CloseGossipOnQuestOfferEventListener(IQuestWindowCreateEventSubscribable subscriptionService,
		[KeyFilter(UnityUIRegisterationKey.GossipWindow)] [NotNull] IUIGossipWindow gossipWindow) 
			: base(subscriptionService)
		{
			GossipWindow = gossipWindow;
		}

		protected override void OnEventFired(object source, QuestWindowCreateEventArgs args)
		{
			GossipWindow.RootElement.SetElementActive(false);
		}
	}
}
