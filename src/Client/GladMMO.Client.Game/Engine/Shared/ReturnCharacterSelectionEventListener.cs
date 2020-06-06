﻿using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Common.Logging;
using Glader.Essentials;
using Nito.AsyncEx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.CharacterCreationScreen)]
	public sealed class ReturnCharacterSelectionEventListener : ButtonClickedEventListener<ISceneBackButtonClickedSubscribable>
	{
		private ILog Logger { get; }

		public ReturnCharacterSelectionEventListener(ISceneBackButtonClickedSubscribable subscriptionService,
			[NotNull] ILog logger) 
			: base(subscriptionService)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void OnEventFired(object source, ButtonClickedEventArgs args)
		{
			args.Button.IsInteractable = false;

			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				await GladMMOSceneManager.UnloadAllAddressableScenesAsync();

				//TODO: This is a hack because we've unloaded all scenes.
				GladMMOSceneManager.LoadAddressableSceneAdditiveAsync(GladMMOClientConstants.CHARACTER_SELECTION_SCENE_NAME, true);
			});
		}
	}
}
