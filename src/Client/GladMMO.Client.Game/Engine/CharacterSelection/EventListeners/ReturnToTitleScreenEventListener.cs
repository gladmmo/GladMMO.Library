using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.CharacterSelection)]
	public sealed class ReturnToTitleScreenEventListener : ButtonClickedEventListener<ISceneBackButtonClickedSubscribable>
	{
		private ILog Logger { get; }

		public ReturnToTitleScreenEventListener(ISceneBackButtonClickedSubscribable subscriptionService,
			[NotNull] ILog logger) 
			: base(subscriptionService)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void OnEventFired(object source, ButtonClickedEventArgs args)
		{
			args.Button.IsInteractable = false;
			SceneManager.LoadSceneAsync(GladMMOClientConstants.TITLE_SCREEN_NAME).allowSceneActivation = true;
		}
	}
}
