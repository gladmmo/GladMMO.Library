using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
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
			SceneManager.LoadSceneAsync(GladMMOClientConstants.CHARACTER_SELECTION_SCENE_NAME).allowSceneActivation = true;
		}
	}
}
