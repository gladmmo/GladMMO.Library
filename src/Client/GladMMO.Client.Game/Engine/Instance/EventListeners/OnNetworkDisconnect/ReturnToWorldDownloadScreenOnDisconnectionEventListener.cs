﻿using System;
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
			[NotNull] IGeneralErrorEncounteredEventPublisher errorPublisher) 
			: base(subscriptionService)
		{
			ErrorPublisher = errorPublisher ?? throw new ArgumentNullException(nameof(errorPublisher));
		}

		protected override void OnThreadUnSafeEventFired(object source, EventArgs args)
		{
			ErrorPublisher.PublishEvent(this, new GeneralErrorEncounteredEventArgs($"Disconnected", "Connection to the instance server was lost.", () => SceneManager.LoadScene(GladMMOClientConstants.CHARACTER_SELECTION_SCENE_NAME)));
		}
	}
}
