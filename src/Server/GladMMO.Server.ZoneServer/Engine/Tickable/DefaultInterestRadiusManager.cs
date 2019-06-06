using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using GladMMO;
using GladNet;
using JetBrains.Annotations;

namespace GladMMO
{
	//Don't do a Skippable here, because we actually don't have a good design. It's possible without work there is still something to do.
	[GameInitializableOrdering(0)] //this should run first
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class DefaultInterestRadiusManager : EventQueueBasedTickable<IEntityInterestChangeEventSubscribable, EntityInterestChangeEventArgs>
	{
		private IReadonlyEntityGuidMappable<InterestCollection> ManagedInterestCollections { get; }

		private INetworkMessageSender<EntityVisibilityChangeContext> VisibilityMessageSender { get; }

		/// <summary>
		/// The collections locking policy.
		/// </summary>
		private GlobalEntityCollectionsLockingPolicy LockingPolicy { get; }

		/// <inheritdoc />
		public DefaultInterestRadiusManager(
			[PostSharp.Patterns.Contracts.NotNull] IEntityInterestChangeEventSubscribable subscriptionService,
			[PostSharp.Patterns.Contracts.NotNull] ILog logger,
			[PostSharp.Patterns.Contracts.NotNull] IReadonlyEntityGuidMappable<InterestCollection> managedInterestCollections,
			[PostSharp.Patterns.Contracts.NotNull] INetworkMessageSender<EntityVisibilityChangeContext> visibilityMessageSender,
			[PostSharp.Patterns.Contracts.NotNull] GlobalEntityCollectionsLockingPolicy lockingPolicy) 
			: base(subscriptionService, true, logger)
		{
			ManagedInterestCollections = managedInterestCollections ?? throw new ArgumentNullException(nameof(managedInterestCollections));
			VisibilityMessageSender = visibilityMessageSender ?? throw new ArgumentNullException(nameof(visibilityMessageSender));
			LockingPolicy = lockingPolicy ?? throw new ArgumentNullException(nameof(lockingPolicy));
		}

		private void ThrowIfNoEntityInterestManaged(NetworkEntityGuid entryContext, NetworkEntityGuid entityGuid)
		{
			if(!ManagedInterestCollections.ContainsKey(entryContext))
				throw new InvalidOperationException($"Guid: {entityGuid} tried to enter Entity: {entryContext} interest. But Entity does not maintain interest. Does not exist in interest collection.");
		}

		/// <inheritdoc />
		protected override void HandleEvent(EntityInterestChangeEventArgs args)
		{
			using(LockingPolicy.ReaderLock(null, CancellationToken.None))
			{
				ThrowIfNoEntityInterestManaged(args.EnterableEntity, args.EnteringEntity);

				//When we encounter an entity interest change, we just want to register into the interest collection
				//Something should eventually run to handle the interest changes, we just basically register/queue them up.
				switch(args.ChangingType)
				{
					case EntityInterestChangeEventArgs.ChangeType.Enter:
						//If the entity already knows the entity then we should not register it.
						if(!ManagedInterestCollections.RetrieveEntity(args.EnterableEntity).Contains(args.EnteringEntity))
							ManagedInterestCollections.RetrieveEntity(args.EnterableEntity).Register(args.EnteringEntity, args.EnteringEntity);
						break;
					case EntityInterestChangeEventArgs.ChangeType.Exit:
						//It's possible we'll want to be having an entity EXIT without being known.
						//They could have Entered (and will be added above at some point) but within
						//the time before this interest system services interest it's possible
						//that they also LEFT it. Therefore there could be an ENTER + EXIT in one go.
						//Registering exits always will address this cleanup.
						ManagedInterestCollections.RetrieveEntity(args.EnterableEntity).Unregister(args.EnteringEntity);
						break;
				}
			}
		}

		/// <inheritdoc />
		protected override void OnFinishedServicingEvents()
		{
			//After ALL the queued interest changes have been serviced
			//we can actually handle the changes and send them and such

			//We need to iterate the entire interest dictionary
			//That means we need to check the new incoming and outgoing entities
			//We do this because we need to build update packets for the players
			//so that they can become aware of them AND we can start pushing
			//events to them
			foreach(var kvp in ManagedInterestCollections)
			{
				//We want to skip any collection that doesn't have any pending changes.
				//No reason to send a message about it nor dequeue anything
				if(!kvp.Value.HasPendingChanges())
					continue;

				//Even though this modifies the collections
				//the write lock of this type is reserved only
				//for adding or removing new entities. Not for
				//actually changing the data itself.
				using(LockingPolicy.ReaderLock(null, CancellationToken.None))
				{
					//We should only build packets for players.
					if(kvp.Key.EntityType == EntityType.Player)
					{
						VisibilityMessageSender.Send(new EntityVisibilityChangeContext(kvp.Key, kvp.Value));
					}

					//No matter player or NPC we should dequeue the joining/leaving
					//entites so that the state of the known entites reflects the diff packets sent
					InterestDequeueSetCommand dequeueCommand = new InterestDequeueSetCommand(kvp.Value, kvp.Value);

					//TODO: Should we execute right away? Or after all packets are sent?
					dequeueCommand.Execute();
				}
			}

#if DEBUG || DEBUG_BUILD
			foreach(var kvp in ManagedInterestCollections)
			{
				if(!kvp.Value.EnteringDequeueable.isEmpty)
					throw new InvalidOperationException($"Failed to fully queue: {nameof(kvp.Value.EnteringDequeueable)}");

				if(!kvp.Value.LeavingDequeueable.isEmpty)
					throw new InvalidOperationException($"Failed to fully queue: {nameof(kvp.Value.LeavingDequeueable)}");
			}
#endif
		}
	}
}