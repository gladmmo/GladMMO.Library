using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using GladNet;

namespace GladMMO
{
	[EntityActorMessageHandler(typeof(DefaultPlayerEntityActor))]
	public sealed class DisconnectNetworkPlayerEntityActorMessageHandler : BaseEntityActorMessageHandler<DefaultEntityActorStateContainer, DisconnectNetworkPlayerEntityActorMessage>
	{
		private IReadonlyEntityGuidMappable<IConnectionService> ConnectionServiceMappable { get; }

		public DisconnectNetworkPlayerEntityActorMessageHandler([NotNull] IReadonlyEntityGuidMappable<IConnectionService> connectionServiceMappable)
		{
			ConnectionServiceMappable = connectionServiceMappable ?? throw new ArgumentNullException(nameof(connectionServiceMappable));
		}

		protected override void HandleMessage(EntityActorMessageContext messageContext, DefaultEntityActorStateContainer state, DisconnectNetworkPlayerEntityActorMessage message)
		{
			//TODO: This can throw, but the entity will die. We should make it so that entity data always exists if the actor is alive.
			ConnectionServiceMappable.RetrieveEntity(state.EntityGuid).Disconnect();
		}
	}
}
