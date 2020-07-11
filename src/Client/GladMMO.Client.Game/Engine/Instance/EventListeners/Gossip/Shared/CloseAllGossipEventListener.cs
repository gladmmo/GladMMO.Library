using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;

namespace GladMMO
{
	//TODO: We need a way to close ALL gossip windows without really having a direct reference to them
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class CloseAllGossipEventListener : BaseSingleEventListenerInitializable<IGossipCompleteEventSubscribable, GossipCompleteEventArgs>
	{
		private IUIGossipWindow GossipWindow { get; }

		private IUIQuestWindow QuestWindow { get; }

		private IUIQuestRequirementWindow QuestRequirementsWindow { get; }

		private IUIQuestCompleteWindow QuestCompleteWindow { get; }

		public CloseAllGossipEventListener([NotNull] IGossipCompleteEventSubscribable subscriptionService,
			[KeyFilter(UnityUIRegisterationKey.GossipWindow)] [NotNull] IUIGossipWindow gossipWindow,
			[KeyFilter(UnityUIRegisterationKey.QuestWindow)] [NotNull] IUIQuestWindow questWindow,
			[KeyFilter(UnityUIRegisterationKey.QuestRequirementsWindow)] [NotNull] IUIQuestRequirementWindow questRequirementsWindow,
			[KeyFilter(UnityUIRegisterationKey.QuestCompleteWindow)] [NotNull] IUIQuestCompleteWindow questCompleteWindow) 
			: base(subscriptionService)
		{
			GossipWindow = gossipWindow ?? throw new ArgumentNullException(nameof(gossipWindow));
			QuestWindow = questWindow ?? throw new ArgumentNullException(nameof(questWindow));
			QuestRequirementsWindow = questRequirementsWindow ?? throw new ArgumentNullException(nameof(questRequirementsWindow));
			QuestCompleteWindow = questCompleteWindow ?? throw new ArgumentNullException(nameof(questCompleteWindow));
		}

		protected override void OnEventFired(object source, GossipCompleteEventArgs args)
		{
			GossipWindow.RootElement.SetElementActive(false);
			QuestWindow.RootElement.SetElementActive(false);
			QuestRequirementsWindow.RootElement.SetElementActive(false);
			QuestCompleteWindow.RootElement.SetElementActive(false);
		}
	}
}
