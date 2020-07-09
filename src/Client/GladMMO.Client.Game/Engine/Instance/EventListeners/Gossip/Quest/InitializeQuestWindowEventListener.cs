﻿using System;
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

		public InitializeQuestWindowEventListener(IQuestWindowCreateEventSubscribable subscriptionService,
			[KeyFilter(UnityUIRegisterationKey.QuestWindow)] [NotNull] IUIQuestWindow questWindow) : base(subscriptionService)
		{
			QuestWindow = questWindow ?? throw new ArgumentNullException(nameof(questWindow));
		}

		protected override void OnEventFired(object source, QuestWindowCreateEventArgs args)
		{
			QuestWindow.Title.Text = args.Content.Title;
			QuestWindow.Description.Text = args.Content.Details;
			QuestWindow.Objective.Text = args.Content.Objectives;

			QuestWindow.RootElement.SetElementActive(true);
		}
	}
}