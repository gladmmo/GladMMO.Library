using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Common.Logging;
using GladNet;

namespace GladMMO
{
	[EntityActorMessageHandler(typeof(DefaultPlayerEntityActor))]
	public sealed class DisconnectNetworkPlayerEntityActorMessageHandler : BaseEntityActorMessageHandler<DefaultEntityActorStateContainer, DisconnectNetworkPlayerEntityActorMessage>
	{
		private IReadonlyEntityGuidMappable<IConnectionService> ConnectionServiceMappable { get; }

		private ILog Logger { get; }

		public DisconnectNetworkPlayerEntityActorMessageHandler([NotNull] IReadonlyEntityGuidMappable<IConnectionService> connectionServiceMappable,
			[NotNull] ILog logger)
		{
			ConnectionServiceMappable = connectionServiceMappable ?? throw new ArgumentNullException(nameof(connectionServiceMappable));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void HandleMessage(EntityActorMessageContext messageContext, DefaultEntityActorStateContainer state, DisconnectNetworkPlayerEntityActorMessage message)
		{
			if(Logger.IsInfoEnabled)
				Logger.Info($"Entity: {state.EntityGuid} actor handling disconnection message.");

			//TODO: This can throw, but the entity will die. We should make it so that entity data always exists if the actor is alive.
			ConnectionServiceMappable.RetrieveEntity(state.EntityGuid).Disconnect();
		}
	}
}
