using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class AutofacCleanupOnDisconnectionEventListener : ThreadUnSafeBaseSingleEventListenerInitializable<INetworkClientDisconnectedEventSubscribable>
	{
		private ILifetimeScope AutofacScope { get; }

		public AutofacCleanupOnDisconnectionEventListener(INetworkClientDisconnectedEventSubscribable subscriptionService,
			[NotNull] ILifetimeScope autofacScope) 
			: base(subscriptionService)
		{
			AutofacScope = autofacScope ?? throw new ArgumentNullException(nameof(autofacScope));
		}

		protected override void OnThreadUnSafeEventFired(object source, EventArgs args)
		{
			//Most importantly, this will dipose of the resources of
			//all currently loaded asset bundles.
			AutofacScope.Dispose();
		}
	}
}
