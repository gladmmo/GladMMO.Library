using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Glader.Essentials;
using Nito.AsyncEx;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class ReturnToCharacterSelectionOnLogoutEventListener : BaseSingleEventListenerInitializable<IInstanceLogoutEventSubscribable>
	{
		private ILog Logger { get; }

		public ReturnToCharacterSelectionOnLogoutEventListener(IInstanceLogoutEventSubscribable subscriptionService,
			[NotNull] ILog logger)
			: base(subscriptionService)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void OnEventFired(object source, EventArgs args)
		{
			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				await GladMMOSceneManager.UnloadAllAddressableScenesAsync();

				//TODO: This is a hack because we've unloaded all scenes.
				GladMMOSceneManager.LoadAddressableSceneAdditiveAsync(GladMMOClientConstants.CHARACTER_SELECTION_SCENE_NAME, true);
			});
		}
	}
}
