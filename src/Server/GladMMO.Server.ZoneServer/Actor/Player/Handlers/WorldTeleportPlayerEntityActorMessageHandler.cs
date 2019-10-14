using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace GladMMO
{
	[EntityActorMessageHandler(typeof(DefaultPlayerEntityActor))]
	public sealed class WorldTeleportPlayerEntityActorMessageHandler : BaseEntityActorMessageHandler<DefaultEntityActorStateContainer, WorldTeleportPlayerEntityActorMessage>
	{
		//TODO: Enable ways for actors to access ONLY data associated with them.
		private IReadonlyEntityGuidMappable<EntitySaveableConfiguration> PersistenceConfigurationMappable { get; }

		private IZoneServerToGameServerClient ZoneToSeverClient { get; }

		public WorldTeleportPlayerEntityActorMessageHandler([NotNull] IReadonlyEntityGuidMappable<EntitySaveableConfiguration> persistenceConfigurationMappable, 
			[NotNull] IZoneServerToGameServerClient zoneToSeverClient)
		{
			PersistenceConfigurationMappable = persistenceConfigurationMappable ?? throw new ArgumentNullException(nameof(persistenceConfigurationMappable));
			ZoneToSeverClient = zoneToSeverClient ?? throw new ArgumentNullException(nameof(zoneToSeverClient));
		}

		protected override void HandleMessage(EntityActorMessageContext messageContext, DefaultEntityActorStateContainer state, WorldTeleportPlayerEntityActorMessage message)
		{
			//Something just told player we are teleporting to another world

			//TODO: Kind of a potential exploit here. Since the process to transfer sessions
			//is async they could potentially log off and log back in quickly and avoid a trasnfer
			//So if this is a FORCED transfer, like for death or something or a kick, they could potentially avoid it??
			ProjectVersionStage.AssertBeta();

			//TODO: Find a way to make it so actors state is always valid.
			//If this throws then the entity dies. So no reason to check it.
			EntitySaveableConfiguration entity = PersistenceConfigurationMappable.RetrieveEntity(state.EntityGuid);
			entity.isWorldTeleporting = true;

			ZoneToSeverClient.TryWorldTeleportCharacter(new ZoneServerWorldTeleportCharacterRequest(state.EntityGuid, message.WorldTeleportGameObjectId))
				.ContinueWith((task, o) =>
				{
					//Whether this succeeds or not this continuation will occur
					//either way we should just disconnect the player because we
					//either have uncoverable issues or they are going to be transfered after
					//disconnection.


					//ConnectionServiceMappable.RetrieveEntity(args.TeleportingPlayer).Disconnect();
					//Since this is async the entity might actually be gone, so we used the actor messaging system
					//To indicate that it should disconnect
					messageContext.Entity.Tell(new DisconnectNetworkPlayerEntityActorMessage());
				}, TaskContinuationOptions.ExecuteSynchronously);
		}
	}
}
