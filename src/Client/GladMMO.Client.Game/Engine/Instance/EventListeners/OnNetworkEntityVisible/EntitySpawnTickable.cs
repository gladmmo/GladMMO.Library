using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	[AdditionalRegisterationAs(typeof(ILocalPlayerSpawnedEventSubscribable))]
	[AdditionalRegisterationAs(typeof(IEntityCreationStartingEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class EntitySpawnTickable : EventQueueBasedTickable<INetworkEntityVisibleEventSubscribable, NetworkEntityNowVisibleEventArgs>, ILocalPlayerSpawnedEventSubscribable, IEntityCreationStartingEventSubscribable
	{
		private IKnownEntitySet KnownEntites { get; }

		/// <inheritdoc />
		public event EventHandler<LocalPlayerSpawnedEventArgs> OnLocalPlayerSpawned;

		public event EventHandler<EntityCreationEventArgs> OnEntityCreationStarting;

		private ICharacterDataRepository CharacterDateRepository { get; }

		/// <inheritdoc />
		public EntitySpawnTickable([NotNull] INetworkEntityVisibleEventSubscribable subscriptionService, 
			[NotNull] ILog logger,
			[NotNull] IKnownEntitySet knownEntites,
			[NotNull] ICharacterDataRepository characterDateRepository)
			: base(subscriptionService, true, logger) //TODO: We probably shouldn't spawn everything per frame. We should probably stagger spawning.
		{
			KnownEntites = knownEntites ?? throw new ArgumentNullException(nameof(knownEntites));
			CharacterDateRepository = characterDateRepository ?? throw new ArgumentNullException(nameof(characterDateRepository));
		}

		/// <inheritdoc />
		protected override void HandleEvent(NetworkEntityNowVisibleEventArgs args)
		{
			try
			{
				//It should be assumed none of the event listeners will be async
				OnEntityCreationStarting?.Invoke(this, new EntityCreationEventArgs(args.EntityGuid));

				if(args.EntityGuid.EntityType == EntityType.Player && IsSpawningEntityLocalPlayer(args.EntityGuid))
				{
					if(Logger.IsInfoEnabled)
						Logger.Info($"Spawning local player.");

					OnLocalPlayerSpawned?.Invoke(this, new LocalPlayerSpawnedEventArgs(args.EntityGuid));
				}
				else if(args.EntityGuid.EntityType == EntityType.Player)
				{
					if(Logger.IsInfoEnabled)
						Logger.Info($"Spawning remote player.");
				}

				//TODO: We need to do abit MORE about this, to know the entity.
				KnownEntites.AddEntity(args.EntityGuid);

				if(Logger.IsDebugEnabled)
					Logger.Debug($"Entity: {args.EntityGuid.EntityType}:{args.EntityGuid.EntityId} is now known.");

				//TODO: Handle remote players and non-players.
			}
			catch(Exception e)
			{
				if(Logger.IsErrorEnabled)
					Logger.Error($"Failed to Create Entity: {args.EntityGuid} Exception: {e.Message}\n\nStack: {e.StackTrace}");
				throw;
			}
		}

		//This was brought over from the TrinityCore times, it used to be more complex to determine.
		private bool IsSpawningEntityLocalPlayer([NotNull] NetworkEntityGuid guid)
		{
			if(guid == null) throw new ArgumentNullException(nameof(guid));
			return CharacterDateRepository.CharacterId == guid.EntityId;
		}
	}
}
