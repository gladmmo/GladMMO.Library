using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Glader.Essentials;
using GladNet;
using JetBrains.Annotations;

namespace GladMMO
{
	//TODO: Refactor, this is just for testing
	[GameInitializableOrdering(1)]
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class EntityDataUpdateManager : IGameTickable
	{
		private IReadonlyEntityGuidMappable<IPeerPayloadSendService<GameServerPacketPayload>> SessionMappable { get; }

		private IReadonlyEntityGuidMappable<InterestCollection> GuidToInterestCollectionMappable { get; }

		private IFactoryCreatable<FieldValueUpdate, EntityFieldUpdateCreationContext> UpdateFactory { get; }

		private IReadonlyEntityGuidMappable<IChangeTrackableEntityDataCollection> ChangeTrackingCollections { get; }

		private IReadonlyKnownEntitySet KnownEntities { get; }

		/// <inheritdoc />
		public EntityDataUpdateManager(
			IReadonlyEntityGuidMappable<IPeerPayloadSendService<GameServerPacketPayload>> sessionMappable, 
			IReadonlyEntityGuidMappable<InterestCollection> guidToInterestCollectionMappable, 
			IFactoryCreatable<FieldValueUpdate, EntityFieldUpdateCreationContext> updateFactory, 
			IReadonlyEntityGuidMappable<IChangeTrackableEntityDataCollection> changeTrackingCollections,
			[NotNull] IReadonlyKnownEntitySet knownEntities)
		{
			SessionMappable = sessionMappable;
			GuidToInterestCollectionMappable = guidToInterestCollectionMappable;
			UpdateFactory = updateFactory;
			ChangeTrackingCollections = changeTrackingCollections;
			KnownEntities = knownEntities ?? throw new ArgumentNullException(nameof(knownEntities));
		}

		/// <inheritdoc />
		public void Tick()
		{
			foreach (var entry in GuidToInterestCollectionMappable.EnumerateWithGuid(KnownEntities))
			{
				InterestCollection interest = entry.ComponentValue;

				//Even if we only know ourselves we should do this anyway
				//so that the client can receieve entity data changes about itself

				//TODO: We probably won't send an update about ALL entites, so this is some wasted allocations and time
				List<EntityAssociatedData<FieldValueUpdate>> updates = new List<EntityAssociatedData<FieldValueUpdate>>(interest.ContainedEntities.Count);

				foreach(var interestingEntityGuid in interest.ContainedEntities)
				{
					//Don't build an update for entities that don't have any changes
					if(!ChangeTrackerHasChangesForEntity(interestingEntityGuid))
						continue;

					//TODO: We should cache this update value so we don't need to recompute it for ALL players who are interested
					//This is the update collection for the particular Entity with guid interestingEntityGuid
					//We want to use the CHANGE TRACKING bitarray for updates. If this was initial discovery we'd use the SIT bitarray to send all set values.
					FieldValueUpdate update = UpdateFactory.Create(new EntityFieldUpdateCreationContext(ChangeTrackingCollections.RetrieveEntity(interestingEntityGuid), ChangeTrackingCollections.RetrieveEntity(interestingEntityGuid).ChangeTrackingArray));

					updates.Add(new EntityAssociatedData<FieldValueUpdate>(interestingEntityGuid, update));
				}

				//It's possible no entity had updates, so we should not send a packet update
				if(updates.Count != 0)
					SendUpdate(entry.EntityGuid, updates);
			}

			foreach(var dataEntityCollection in ChangeTrackingCollections.Enumerate(KnownEntities))
				dataEntityCollection.ClearTrackedChanges();
		}

		private void SendUpdate(NetworkEntityGuid guid, List<EntityAssociatedData<FieldValueUpdate>> updates)
		{
			try
			{
				SessionMappable.RetrieveEntity(guid).SendMessageImmediately(new FieldValueUpdateEvent(updates.ToArray()));
			}
			catch(Exception e)
			{
				throw new InvalidOperationException($"Failed to send update to session with Guid: {guid}. Exception: {e.Message}", e);
			}
		}

		private bool ChangeTrackerHasChangesForEntity(NetworkEntityGuid interestingEntityGuid)
		{
			try
			{
				return ChangeTrackingCollections.RetrieveEntity(interestingEntityGuid).HasPendingChanges;
			}
			catch(Exception e)
			{
				throw new InvalidOperationException($"Attempted to load Entity: {interestingEntityGuid}'s interst collection From: {ChangeTrackingCollections.GetType().Name} but failed. No entry matched the key. Exception: {e.Message}", e);
			}
		}
	}
}
