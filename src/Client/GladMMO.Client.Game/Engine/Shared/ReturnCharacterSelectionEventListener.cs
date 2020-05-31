using System; using FreecraftCore;
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
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class ReturnCharacterSelectionEventListener : ButtonClickedEventListener<ISceneBackButtonClickedSubscribable>
	{
		private ILog Logger { get; }

		private ILifetimeScope AutofacScope { get; }

		public ReturnCharacterSelectionEventListener(ISceneBackButtonClickedSubscribable subscriptionService,
			[NotNull] ILog logger,
			[NotNull] ILifetimeScope autofacScope) 
			: base(subscriptionService)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			AutofacScope = autofacScope ?? throw new ArgumentNullException(nameof(autofacScope));
		}

		protected override void OnEventFired(object source, ButtonClickedEventArgs args)
		{
			args.Button.IsInteractable = false;

			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				AutofacScope.Dispose();

				GladMMOSceneManager.LoadAddressableSceneAsync(GladMMOClientConstants.CHARACTER_SELECTION_SCENE_NAME);
			});
		}
	}
}
