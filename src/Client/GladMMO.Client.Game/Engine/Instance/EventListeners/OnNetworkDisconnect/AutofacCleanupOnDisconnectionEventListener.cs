using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Common.Logging;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class AutofacCleanupOnDisconnectionEventListener : ThreadUnSafeBaseSingleEventListenerInitializable<INetworkClientDisconnectedEventSubscribable>
	{
		private ILifetimeScope AutofacScope { get; }

		private ILog Logger { get; }

		public AutofacCleanupOnDisconnectionEventListener(INetworkClientDisconnectedEventSubscribable subscriptionService,
			[NotNull] ILifetimeScope autofacScope,
			[NotNull] ILog logger) 
			: base(subscriptionService)
		{
			AutofacScope = autofacScope ?? throw new ArgumentNullException(nameof(autofacScope));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void OnThreadUnSafeEventFired(object source, EventArgs args)
		{
			if(Logger.IsInfoEnabled)
				Logger.Info("Disposing AutoFac.");

			//Most importantly, this will dipose of the resources of
			//all currently loaded asset bundles.
			AutofacScope.Dispose();
		}
	}
}
