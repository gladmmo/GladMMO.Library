﻿using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using UnityEngine.SceneManagement;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class ReturnToWorldDownloadScreenOnDisconnectionEventListener : ThreadUnSafeBaseSingleEventListenerInitializable<INetworkClientDisconnectedEventSubscribable>
	{
		private IGeneralErrorEncounteredEventPublisher ErrorPublisher { get; }

		public ReturnToWorldDownloadScreenOnDisconnectionEventListener(INetworkClientDisconnectedEventSubscribable subscriptionService,
			IInstanceLogoutEventSubscribable logoutEventSubscriptionService,
			[NotNull] IGeneralErrorEncounteredEventPublisher errorPublisher) 
			: base(subscriptionService)
		{
			ErrorPublisher = errorPublisher ?? throw new ArgumentNullException(nameof(errorPublisher));

			//We want to NOT display the disconnected client error if we're doing a normal logout.
			logoutEventSubscriptionService.OnInstanceLogout += (sender, args) => this.Unsubscribe();
		}

		protected override void OnThreadUnSafeEventFired(object source, EventArgs args)
		{
			ErrorPublisher.PublishEvent(this, new GeneralErrorEncounteredEventArgs($"Disconnected", "Connection to the instance server was lost.", () => GladMMOSceneManager.LoadAddressableSceneAsync(GladMMOClientConstants.CHARACTER_SELECTION_SCENE_NAME)));
		}
	}
}
