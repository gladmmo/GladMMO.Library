using System; using FreecraftCore;
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

		private IInstanceLogoutEventPublisher LogoutEventPublisher { get; }

		public ReturnToWorldDownloadScreenOnDisconnectionEventListener(INetworkClientDisconnectedEventSubscribable subscriptionService,
			IInstanceLogoutEventSubscribable logoutEventSubscriptionService,
			[NotNull] IGeneralErrorEncounteredEventPublisher errorPublisher,
			[NotNull] IInstanceLogoutEventPublisher logoutEventPublisher) 
			: base(subscriptionService)
		{
			ErrorPublisher = errorPublisher ?? throw new ArgumentNullException(nameof(errorPublisher));
			LogoutEventPublisher = logoutEventPublisher ?? throw new ArgumentNullException(nameof(logoutEventPublisher));

			//We want to NOT display the disconnected client error if we're doing a normal logout.
			logoutEventSubscriptionService.OnInstanceLogout += (sender, args) => this.Unsubscribe();
		}

		protected override void OnThreadUnSafeEventFired(object source, EventArgs args)
		{
			//Idea here, is we just have the error menu broadcast a logout event/request.
			//So it goes through the same setup pipeline.
			ErrorPublisher.PublishEvent(this, new GeneralErrorEncounteredEventArgs($"Disconnected", "Connection to the instance server was lost.", () => LogoutEventPublisher.PublishEvent(this, EventArgs.Empty)));
		}
	}
}
