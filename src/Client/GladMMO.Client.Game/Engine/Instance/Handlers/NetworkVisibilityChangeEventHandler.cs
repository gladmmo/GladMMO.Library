using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using GladMMO;
using GladNet;

namespace GladMMO
{
	[AdditionalRegisterationAs(typeof(INetworkEntityVisibilityLostEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class NetworkVisibilityChangeEventHandler : BaseGameClientGameMessageHandler<NetworkObjectVisibilityChangeEventPayload>,
		INetworkEntityVisibilityLostEventSubscribable
	{
		/// <inheritdoc />
		public event EventHandler<NetworkEntityVisibilityLostEventArgs> OnNetworkEntityVisibilityLost;

		private INetworkEntityVisibilityEventPublisher VisibilityEventPublisher { get; }

		private IFactoryCreatable<NetworkEntityNowVisibleEventArgs, EntityCreationData> VisibileEventFactory { get; }

		/// <inheritdoc />
		public NetworkVisibilityChangeEventHandler(ILog logger,
			[NotNull] INetworkEntityVisibilityEventPublisher visibilityEventPublisher,
			[NotNull] IFactoryCreatable<NetworkEntityNowVisibleEventArgs, EntityCreationData> visibileEventFactory)
			: base(logger)
		{
			VisibilityEventPublisher = visibilityEventPublisher ?? throw new ArgumentNullException(nameof(visibilityEventPublisher));
			VisibileEventFactory = visibileEventFactory ?? throw new ArgumentNullException(nameof(visibileEventFactory));
		}

		/// <inheritdoc />
		public override async Task HandleMessage(IPeerMessageContext<GameClientPacketPayload> context, NetworkObjectVisibilityChangeEventPayload payload)
		{
			foreach(var entity in payload.EntitiesToCreate)
			{
				if(Logger.IsDebugEnabled)
					Logger.Debug($"Encountered new entity: {entity.EntityGuid}");
			}

			foreach(var entity in payload.OutOfRangeEntities)
			{
				if(Logger.IsErrorEnabled)
					Logger.Debug($"Leaving entity: {entity}");
			}

			//Assume it's a player for now
			foreach(var creationData in payload.EntitiesToCreate)
			{
				NetworkEntityNowVisibleEventArgs visibilityEvent = VisibileEventFactory.Create(creationData);

				VisibilityEventPublisher.Publish(visibilityEvent);
			}

			foreach(var destroyData in payload.OutOfRangeEntities)
			{
				OnNetworkEntityVisibilityLost?.Invoke(this, new NetworkEntityVisibilityLostEventArgs(destroyData));
			}
		}
	}
}