using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using GladNet;

namespace GladMMO
{
	[AdditionalRegisterationAs(typeof(INetworkEntityVisibleEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class PlayerSelfSpawnEventPayloadHandler : BaseGameClientGameMessageHandler<PlayerSelfSpawnEventPayload>, INetworkEntityVisibleEventSubscribable
	{
		/// <inheritdoc />
		public event EventHandler<NetworkEntityNowVisibleEventArgs> OnNetworkEntityNowVisible;

		private IFactoryCreatable<NetworkEntityNowVisibleEventArgs, EntityCreationData> VisibileEventFactory { get; }

		public PlayerSelfSpawnEventPayloadHandler(ILog logger, [NotNull] IFactoryCreatable<NetworkEntityNowVisibleEventArgs, EntityCreationData> visibileEventFactory) 
			: base(logger)
		{
			VisibileEventFactory = visibileEventFactory ?? throw new ArgumentNullException(nameof(visibileEventFactory));
		}

		public override Task HandleMessage(IPeerMessageContext<GameClientPacketPayload> context, PlayerSelfSpawnEventPayload payload)
		{
			NetworkEntityNowVisibleEventArgs visibilityEvent = VisibileEventFactory.Create(payload.CreationData);

			//Now we broadcast that an entity is now visible.
			OnNetworkEntityNowVisible?.Invoke(this, visibilityEvent);
			return Task.CompletedTask;
		}
	}
}
