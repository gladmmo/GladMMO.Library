using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
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
	public sealed class InterestChangeEventHandlerTickable : EventQueueBasedTickable<IEntityInterestChangeEventSubscribable, EntityInterestChangeEventArgs>
	{
		private IReadonlyEntityGuidMappable<InterestCollection> ManagedInterestCollections { get; }

		private INetworkMessageSender<EntityVisibilityChangeContext> VisibilityMessageSender { get; }

		private IReadonlyKnownEntitySet KnownEntities { get; }

		private IReadonlyEntityGuidMappable<IActorRef> ActorReferenceMappable { get; }

		/// <inheritdoc />
		public InterestChangeEventHandlerTickable(
			[NotNull] IEntityInterestChangeEventSubscribable subscriptionService,
			[NotNull] ILog logger,
			[NotNull] IReadonlyEntityGuidMappable<InterestCollection> managedInterestCollections,
			[NotNull] INetworkMessageSender<EntityVisibilityChangeContext> visibilityMessageSender,
			[NotNull] IReadonlyKnownEntitySet knownEntities,
			[NotNull] IReadonlyEntityGuidMappable<IActorRef> actorReferenceMappable)
			: base(subscriptionService, true, logger)
		{
			ManagedInterestCollections = managedInterestCollections ?? throw new ArgumentNullException(nameof(managedInterestCollections));
			VisibilityMessageSender = visibilityMessageSender ?? throw new ArgumentNullException(nameof(visibilityMessageSender));
			KnownEntities = knownEntities ?? throw new ArgumentNullException(nameof(knownEntities));
			ActorReferenceMappable = actorReferenceMappable ?? throw new ArgumentNullException(nameof(actorReferenceMappable));
		}

		private void ThrowIfNoEntityInterestManaged(ObjectGuid entryContext, ObjectGuid entityGuid)
		{
			if(!ManagedInterestCollections.ContainsKey(entryContext))
				throw new InvalidOperationException($"Guid: {entityGuid} tried to enter Entity: {entryContext} interest. But Entity does not maintain interest. Does not exist in interest collection.");
		}

		/// <inheritdoc />
		protected override void HandleEvent(EntityInterestChangeEventArgs args)
		{
			//TODO: Check if entity is still known
			KnownEntities.LockObject.EnterReadLock();
			try
			{
				ThrowIfNoEntityInterestManaged(args.EnterableEntity, args.EnteringEntity);

				//When we encounter an entity interest change, we just want to register into the interest collection
				//Something should eventually run to handle the interest changes, we just basically register/queue them up.
				switch (args.ChangingType)
				{
					case EntityInterestChangeEventArgs.ChangeType.Enter:
						//HelloKitty: Yea, so if we actually register a change of ourselves it actually breaks the client for some reason
						//right now but technically everything is fine even if we do. No exceptions on either the client or server. Very strange
						//but limited time to investigate
						if(args.EnterableEntity != args.EnteringEntity)
						//if (!ManagedInterestCollections.RetrieveEntity(args.EnterableEntity).Contains(args.EnteringEntity))
						{
							//If the entity already knows the entity then we should not register it.
							ManagedInterestCollections.RetrieveEntity(args.EnterableEntity).Register(args.EnteringEntity, args.EnteringEntity);
							ActorReferenceMappable.RetrieveEntity(args.EnterableEntity).Tell(new EntityInterestGainedMessage(args.EnteringEntity), ActorReferenceMappable.RetrieveEntity(args.EnterableEntity));
						}
						break;
					case EntityInterestChangeEventArgs.ChangeType.Exit:
						//It's possible we'll want to be having an entity EXIT without being known.
						//They could have Entered (and will be added above at some point) but within
						//the time before this interest system services interest it's possible
						//that they also LEFT it. Therefore there could be an ENTER + EXIT in one go.
						//Registering exits always will address this cleanup.
						ManagedInterestCollections.RetrieveEntity(args.EnterableEntity).Unregister(args.EnteringEntity);
						ActorReferenceMappable.RetrieveEntity(args.EnterableEntity).Tell(new EntityInterestRemoveMessage(args.EnteringEntity), ActorReferenceMappable.RetrieveEntity(args.EnterableEntity));
						break;
				}
			}
			finally
			{
				KnownEntities.LockObject.ExitReadLock();
			}
		}

		/// <inheritdoc />
		protected override void OnFinishedServicingEvents()
		{
			//After ALL the queued interest changes have been serviced
			//we can actually handle the changes and send them and such

			KnownEntities.LockObject.EnterReadLock();
			try
			{
				//We need to iterate the entire interest dictionary
				//That means we need to check the new incoming and outgoing entities
				//We do this because we need to build update packets for the players
				//so that they can become aware of them AND we can start pushing
				//events to them
				foreach (var entity in KnownEntities)
				{
					InterestCollection interestCollection = ManagedInterestCollections.RetrieveEntity(entity);

					//We want to skip any collection that doesn't have any pending changes.
					//No reason to send a message about it nor dequeue anything
					if (!interestCollection.HasPendingChanges())
						continue;

					//We should only build packets for players.
					if (entity.EntityType == EntityType.Player)
						VisibilityMessageSender.Send(new EntityVisibilityChangeContext(entity, interestCollection));

					//No matter player or NPC we should dequeue the joining/leaving
					//entites so that the state of the known entites reflects the diff packets sent
					InterestDequeueSetCommand dequeueCommand = new InterestDequeueSetCommand(interestCollection);

					//TODO: Should we execute right away? Or after all packets are sent?
					dequeueCommand.Execute();
				}
			}
			finally
			{
				KnownEntities.LockObject.ExitReadLock();
			}
		}
	}
}