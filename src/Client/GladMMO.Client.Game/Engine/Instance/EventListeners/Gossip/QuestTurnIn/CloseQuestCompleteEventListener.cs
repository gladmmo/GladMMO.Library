using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class CloseQuestCompleteEventListener : BaseQuestTurnInEventListener
	{
		private IUIQuestCompleteWindow QuestCompletedWindow { get; }

		private LocalPlayerMenuState MenuState { get; }

		public CloseQuestCompleteEventListener(IQuestTurnInEventSubscribable subscriptionService,
			[KeyFilter(UnityUIRegisterationKey.QuestCompleteWindow)] [NotNull] IUIQuestCompleteWindow questCompletedWindow,
			[NotNull] LocalPlayerMenuState menuState) : base(subscriptionService)
		{
			QuestCompletedWindow = questCompletedWindow ?? throw new ArgumentNullException(nameof(questCompletedWindow));
			MenuState = menuState ?? throw new ArgumentNullException(nameof(menuState));
		}

		protected override void OnEventFired(object source, QuestTurnInEventArgs args)
		{
			//Not the same completed quest UI as the event, so don't CLOSE!!
			if (MenuState.SelectedQuest == null || MenuState.SelectedQuest.QuestId != args.QuestId)
				return;

			QuestCompletedWindow.RootElement.SetElementActive(false);
		}
	}
}
