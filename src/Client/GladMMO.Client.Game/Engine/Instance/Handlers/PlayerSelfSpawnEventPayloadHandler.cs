using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using GladNet;

namespace GladMMO
{
	/*[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class PlayerSelfSpawnEventPayloadHandler : BaseGameClientGameMessageHandler<PlayerSelfSpawnEventPayload>
	{
		private INetworkEntityVisibilityEventPublisher VisibilityEventPublisher { get; }

		private IFactoryCreatable<NetworkEntityNowVisibleEventArgs, ObjectCreationData> VisibileEventFactory { get; }

		public PlayerSelfSpawnEventPayloadHandler(ILog logger, 
			[NotNull] IFactoryCreatable<NetworkEntityNowVisibleEventArgs, ObjectCreationData> visibileEventFactory
			, [NotNull] INetworkEntityVisibilityEventPublisher visibilityEventPublisher) 
			: base(logger)
		{
			VisibileEventFactory = visibileEventFactory ?? throw new ArgumentNullException(nameof(visibileEventFactory));
			VisibilityEventPublisher = visibilityEventPublisher ?? throw new ArgumentNullException(nameof(visibilityEventPublisher));
		}

		public override async Task HandleMessage(IPeerMessageContext<GamePacketPayload> context, PlayerSelfSpawnEventPayload payload)
		{
			NetworkEntityNowVisibleEventArgs visibilityEvent = VisibileEventFactory.Create(payload.CreationData);

			VisibilityEventPublisher.Publish(visibilityEvent);
		}
	}*/
}
