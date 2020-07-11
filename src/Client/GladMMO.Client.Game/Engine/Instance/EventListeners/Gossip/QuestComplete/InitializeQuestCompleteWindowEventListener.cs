using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class InitializeQuestCompleteWindowEventListener : BaseQuestCompleteWindowCreateEventListener
	{
		private IUIQuestCompleteWindow QuestCompleteWindow { get; }

		private IGossipWindowCollection GossipWindows { get; }

		public InitializeQuestCompleteWindowEventListener(IQuestCompleteWindowCreateEventSubscribable subscriptionService,
			[KeyFilter(UnityUIRegisterationKey.QuestCompleteWindow)] [NotNull] IUIQuestCompleteWindow questCompleteWindow,
			[NotNull] IGossipWindowCollection gossipWindows)
			: base(subscriptionService)
		{
			QuestCompleteWindow = questCompleteWindow ?? throw new ArgumentNullException(nameof(questCompleteWindow));
			GossipWindows = gossipWindows ?? throw new ArgumentNullException(nameof(gossipWindows));
		}

		protected override void OnEventFired(object source, QuestCompleteWindowCreateEventArgs args)
		{
			GossipWindows.CloseAll();

			QuestCompleteWindow.Title.Text = $"{args.QuestId} - TODO: Name!";
			QuestCompleteWindow.Description.Text = args.CompletionText;
			QuestCompleteWindow.AcceptButton.IsInteractable = true;

			QuestCompleteWindow.RootElement.SetElementActive(true);
		}
	}
}
