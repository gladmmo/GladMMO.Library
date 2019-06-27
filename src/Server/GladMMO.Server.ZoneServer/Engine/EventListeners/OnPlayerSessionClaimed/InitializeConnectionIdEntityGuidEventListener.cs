using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class InitializeConnectionIdEntityGuidEventListener : BaseSingleEventListenerInitializable<IPlayerSessionClaimedEventSubscribable, PlayerSessionClaimedEventArgs>
	{
		private IConnectionEntityCollection ConnectionIdToControllingEntityMap { get; }

		public InitializeConnectionIdEntityGuidEventListener(IPlayerSessionClaimedEventSubscribable subscriptionService,
			[NotNull] IConnectionEntityCollection connectionIdToControllingEntityMap) 
			: base(subscriptionService)
		{
			ConnectionIdToControllingEntityMap = connectionIdToControllingEntityMap ?? throw new ArgumentNullException(nameof(connectionIdToControllingEntityMap));
		}

		protected override void OnEventFired(object source, PlayerSessionClaimedEventArgs args)
		{
			ConnectionIdToControllingEntityMap.Add(args.SessionContext.ConnectionId, args.EntityGuid);
		}
	}
}
