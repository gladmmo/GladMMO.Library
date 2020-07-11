using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class InitializeQuestWindowEventListener : BaseQuestWindowCreateEventListener
	{
		private IUIQuestWindow QuestWindow { get; }

		private IGossipWindowCollection GossipWindows { get; }

		public InitializeQuestWindowEventListener(IQuestWindowCreateEventSubscribable subscriptionService,
			[KeyFilter(UnityUIRegisterationKey.QuestWindow)] [NotNull] IUIQuestWindow questWindow,
			[NotNull] IGossipWindowCollection gossipWindows) : base(subscriptionService)
		{
			QuestWindow = questWindow ?? throw new ArgumentNullException(nameof(questWindow));
			GossipWindows = gossipWindows ?? throw new ArgumentNullException(nameof(gossipWindows));
		}

		protected override void OnEventFired(object source, QuestWindowCreateEventArgs args)
		{
			GossipWindows.CloseAll();

			QuestWindow.Title.Text = args.Content.Title;
			QuestWindow.Description.Text = args.Content.Details;
			QuestWindow.Objective.Text = args.Content.Objectives;
			QuestWindow.AcceptButton.IsInteractable = true;

			QuestWindow.RootElement.SetElementActive(true);
		}
	}
}
