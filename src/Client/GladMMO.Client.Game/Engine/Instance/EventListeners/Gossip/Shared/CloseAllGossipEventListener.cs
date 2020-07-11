using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;

namespace GladMMO
{
	//TODO: We need a way to close ALL gossip windows without really having a direct reference to them
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class CloseAllGossipEventListener : BaseSingleEventListenerInitializable<IGossipCompleteEventSubscribable, GossipCompleteEventArgs>
	{
		private IGossipWindowCollection GossipWindows { get; }

		public CloseAllGossipEventListener([NotNull] IGossipCompleteEventSubscribable subscriptionService,
			[NotNull] IGossipWindowCollection gossipWindows) 
			: base(subscriptionService)
		{
			GossipWindows = gossipWindows ?? throw new ArgumentNullException(nameof(gossipWindows));
		}

		protected override void OnEventFired(object source, GossipCompleteEventArgs args)
		{
			GossipWindows.CloseAll();
		}
	}
}
