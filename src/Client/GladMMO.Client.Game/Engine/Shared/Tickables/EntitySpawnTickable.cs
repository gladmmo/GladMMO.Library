using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	/*[AdditionalRegisterationAs(typeof(ILocalPlayerSpawnedEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.DefaultLobby)]
	public sealed class EntitySpawnTickable : EventQueueBasedTickable<INetworkEntityVisibleEventSubscribable, NetworkEntityNowVisibleEventArgs>, ILocalPlayerSpawnedEventSubscribable
	{
		private IKnownEntitySet KnownEntites { get; }

		/// <inheritdoc />
		public event EventHandler<LocalPlayerSpawnedEventArgs> OnLocalPlayerSpawned;

		//Really just to determine if the local player is spawning. Such a hack though.
		private IReadonlyEntityGuidMappable<MovementBlockData> MovementDataMappable { get; }

		private IFactoryCreatable<GameObject, DefaultEntityCreationContext> EntityFactory { get; }

		/// <inheritdoc />
		public EntitySpawnTickable([NotNull] INetworkEntityVisibleEventSubscribable subscriptionService, 
			[NotNull] ILog logger,
			[NotNull] IKnownEntitySet knownEntites,
			[NotNull] IReadonlyEntityGuidMappable<MovementBlockData> movementDataMappable,
			[NotNull] IFactoryCreatable<GameObject, DefaultEntityCreationContext> entityFactory)
			: base(subscriptionService, true, logger) //TODO: We probably shouldn't spawn everything per frame. We should probably stagger spawning.
		{
			KnownEntites = knownEntites ?? throw new ArgumentNullException(nameof(knownEntites));
			MovementDataMappable = movementDataMappable ?? throw new ArgumentNullException(nameof(movementDataMappable));
			EntityFactory = entityFactory ?? throw new ArgumentNullException(nameof(entityFactory));
		}

		/// <inheritdoc />
		protected override void HandleEvent(NetworkEntityNowVisibleEventArgs args)
		{
			try
			{
				//TODO: We need to do abit MORE about this, to know the entity.
				KnownEntites.AddEntity(args.EntityGuid);

				if(Logger.IsDebugEnabled)
					Logger.Debug($"Entity: {args.EntityGuid.EntityType}:{args.EntityGuid.EntityId} is now known.");

				if(args.EntityGuid.EntityType == EntityType.Player && IsSpawningEntityLocalPlayer(args.EntityGuid))
				{
					if(Logger.IsInfoEnabled)
						Logger.Info($"Spawning local player.");

					//Let's spawn the local player's world representation now.
					EntityFactory.Create(new DefaultEntityCreationContext(args.EntityGuid, MovementDataMappable[args.EntityGuid].MoveInfo.Position.ToUnityVector(), 
						MovementDataMappable[args.EntityGuid].MoveInfo.Orientation, EntityPrefab.LocalPlayer));

					OnLocalPlayerSpawned?.Invoke(this, new LocalPlayerSpawnedEventArgs(args.EntityGuid));
				}
				else
				{
					if(Logger.IsInfoEnabled)
						Logger.Info($"Spawning remote player.");
				}

				//GameObject entityRootObject = EntityFactory.Create(new DefaultEntityCreationContext(args.CreationData.EntityGuid, args.CreationData.InitialMovementData, ComputePrefabTypeFromGuid(args.EntityGuid), args.EntityDataContainer));
			}
			catch(Exception e)
			{
				if(Logger.IsErrorEnabled)
					Logger.Error($"Failed to Create Entity: {args.EntityGuid} Exception: {e.Message}\n\nStack: {e.StackTrace}");
				throw;
			}
		}

		private bool IsSpawningEntityLocalPlayer([NotNull] NetworkEntityGuid guid)
		{
			if(guid == null) throw new ArgumentNullException(nameof(guid));

			//TODO: This is a hack, is there a more efficient way?
			//Possible we create objects without any movement data, but unlikely. Don't know
			if(MovementDataMappable.ContainsKey(guid))
				return MovementDataMappable[guid].UpdateFlags.HasFlag(ObjectUpdateFlags.UPDATEFLAG_SELF); //TODO: Is this flag ONLY set for player??
			else
				throw new InvalidOperationException($"Encountered Player Entity: {guid.EntityType}:{guid.EntityId} without movement data.");

			return false;
		}
	}*/
}
