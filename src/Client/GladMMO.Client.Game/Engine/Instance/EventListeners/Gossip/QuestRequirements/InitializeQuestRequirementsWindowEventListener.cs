using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class InitializeQuestRequirementsWindowEventListener : BaseQuestRequirementsWindowCreateEventListener
	{
		private IUIQuestRequirementWindow QuestRequirementsWindow { get; }

		private IGossipWindowCollection GossipWindows { get; }

		public InitializeQuestRequirementsWindowEventListener(IQuestRequirementsWindowCreateEventSubscribable subscriptionService,
			[KeyFilter(UnityUIRegisterationKey.QuestRequirementsWindow)] [NotNull] IUIQuestRequirementWindow questRequirementsWindow,
			[NotNull] IGossipWindowCollection gossipWindows)
			: base(subscriptionService)
		{
			QuestRequirementsWindow = questRequirementsWindow ?? throw new ArgumentNullException(nameof(questRequirementsWindow));
			GossipWindows = gossipWindows ?? throw new ArgumentNullException(nameof(gossipWindows));
		}

		protected override void OnEventFired(object source, QuestRequirementsWindowCreateEventArgs args)
		{
			GossipWindows.CloseAll();

			QuestRequirementsWindow.Title.Text = $"{args.QuestId} - TODO: Name!";
			QuestRequirementsWindow.Description.Text = args.Requirements;

			//TODO: Continue button handling.
			//QuestWindow.AcceptButton.IsInteractable = true;

			QuestRequirementsWindow.RootElement.SetElementActive(true);
		}
	}
}
