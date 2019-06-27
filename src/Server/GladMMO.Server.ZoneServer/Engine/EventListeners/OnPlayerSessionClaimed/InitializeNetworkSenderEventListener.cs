using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using GladNet;

namespace GladMMO
{
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class InitializeNetworkSenderEventListener : BaseSingleEventListenerInitializable<IPlayerSessionClaimedEventSubscribable, PlayerSessionClaimedEventArgs>
	{
		private IEntityGuidMappable<IPeerPayloadSendService<GameServerPacketPayload>> GuidToSessionMappable { get; }

		public InitializeNetworkSenderEventListener(IPlayerSessionClaimedEventSubscribable subscriptionService,
			[NotNull] IEntityGuidMappable<IPeerPayloadSendService<GameServerPacketPayload>> guidToSessionMappable) : base(subscriptionService)
		{
			GuidToSessionMappable = guidToSessionMappable ?? throw new ArgumentNullException(nameof(guidToSessionMappable));
		}

		protected override void OnEventFired(object source, PlayerSessionClaimedEventArgs args)
		{
			GuidToSessionMappable.AddObject(args.EntityGuid, args.SessionContext.ZoneSession);
		}
	}
}
