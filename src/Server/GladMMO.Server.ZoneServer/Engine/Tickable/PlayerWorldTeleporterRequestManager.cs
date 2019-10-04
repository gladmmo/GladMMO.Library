using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Glader.Essentials;
using GladNet;
using Nito.AsyncEx;

namespace GladMMO
{
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class PlayerWorldTeleporterRequestManager : EventQueueBasedTickable<IPlayerWorldTeleporterRequestedEventSubscribable, PlayerWorldTeleporterRequestEventArgs>
	{
		private IReadonlyEntityGuidMappable<EntitySaveableConfiguration> PersistenceConfigurationMappable { get; }

		private IZoneServerToGameServerClient ZoneToSeverClient { get; }

		private IReadonlyEntityGuidMappable<IConnectionService> ConnectionServiceMappable { get; }

		public PlayerWorldTeleporterRequestManager(IPlayerWorldTeleporterRequestedEventSubscribable subscriptionService, 
			ILog logger,
			[NotNull] IReadonlyEntityGuidMappable<EntitySaveableConfiguration> persistenceConfigurationMappable,
			[NotNull] IZoneServerToGameServerClient zoneToSeverClient,
			[NotNull] IReadonlyEntityGuidMappable<IConnectionService> connectionServiceMappable) 
			: base(subscriptionService, true, logger)
		{
			PersistenceConfigurationMappable = persistenceConfigurationMappable ?? throw new ArgumentNullException(nameof(persistenceConfigurationMappable));
			ZoneToSeverClient = zoneToSeverClient ?? throw new ArgumentNullException(nameof(zoneToSeverClient));
			ConnectionServiceMappable = connectionServiceMappable ?? throw new ArgumentNullException(nameof(connectionServiceMappable));
		}

		protected override void HandleEvent(PlayerWorldTeleporterRequestEventArgs args)
		{
			//TODO: Kind of a potential exploit here. Since the process to transfer sessions
			//is async they could potentially log off and log back in quickly and avoid a trasnfer
			//So if this is a FORCED transfer, like for death or something or a kick, they could potentially avoid it??
			ProjectVersionStage.AssertBeta();

			EntitySaveableConfiguration entity = PersistenceConfigurationMappable.RetrieveEntity(args.TeleportingPlayer);
			entity.isWorldTeleporting = true;

			//TODO: We should more gracefully handle failure

			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				try
				{
					await ZoneToSeverClient.TryWorldTeleportCharacter(new ZoneServerWorldTeleportCharacterRequest(args.TeleportingPlayer, args.WorldTeleporterEntryId))
						.ConfigureAwait(true);

					//TODO: Since this is async they might have disconnected already.
					ConnectionServiceMappable.RetrieveEntity(args.TeleportingPlayer).Disconnect();
				}
				catch (Exception e)
				{
					throw;
				}
			});
		}
	}
}
