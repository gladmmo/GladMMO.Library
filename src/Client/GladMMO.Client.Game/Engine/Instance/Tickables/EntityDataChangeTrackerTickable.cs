using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class EntityDataChangeTrackerTickable : IGameTickable
	{
		private IReadonlyEntityGuidMappable<IChangeTrackableEntityDataCollection> ChangeTrackableMap { get; }

		private IEntityDataChangeCallbackService EntityDataCallbackDispatcher { get; }

		private IReadonlyKnownEntitySet KnownEntites { get; }

		private ILog Logger { get; }

		/// <inheritdoc />
		public EntityDataChangeTrackerTickable(IReadonlyEntityGuidMappable<IChangeTrackableEntityDataCollection> changeTrackableMap, 
			IEntityDataChangeCallbackService entityDataCallbackDispatcher,
			[NotNull] IReadonlyKnownEntitySet knownEntites,
			[NotNull] ILog logger)
		{
			ChangeTrackableMap = changeTrackableMap ?? throw new ArgumentNullException(nameof(changeTrackableMap));
			EntityDataCallbackDispatcher = entityDataCallbackDispatcher ?? throw new ArgumentNullException(nameof(entityDataCallbackDispatcher));
			KnownEntites = knownEntites ?? throw new ArgumentNullException(nameof(knownEntites));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		/// <inheritdoc />
		public void Tick()
		{
			//We just iterate known entites, this prevents some race conditions when we're adding in new entities data into collections
			//but they aren't ready.
			foreach(NetworkEntityGuid entity in KnownEntites)
			{
				var changeTrackable = ChangeTrackableMap.RetrieveEntity(entity);

				//TODO: We can make this faster if we skip entities that have no registered callbacks.
				//The idea is here that we check the changed values, while on the MAIN THREAD
				//and dispatch the changes via a callback registeration service where UI or gameplay systems
				//may register their interest
				if(changeTrackable.HasPendingChanges)
				{
					//We have to lock here otherwise we could encounter race conditions with the
					//change tracking system.
					lock(changeTrackable.SyncObj)
					{
						//We need to try to dispatch events for each changed value.
						foreach(int changedIndex in changeTrackable.ChangeTrackingArray.EnumerateSetBitsByIndex())
						{
							if(Logger.IsDebugEnabled)
								Logger.Debug($"Entity: {entity.EntityType}:{entity.EntityId} ChangedData: {changedIndex}:{changeTrackable.GetFieldValue<int>((int)changedIndex)}");

							//TODO: We don't REALLY want to lock on the dispatching. This could be a REAL bottleneck in the future. We need to redesign this abit
							//TODO: Might be a better way to handle this API, and provide the value instead of the collection.
							EntityDataCallbackDispatcher.InvokeChangeEvents(entity, changeTrackable, changeTrackable.GetFieldValue<int>((int)changedIndex));
						}

						//After we're done servicing the changes, we should clear the changes.
						changeTrackable.ClearTrackedChanges();
					}
				}
			}
		}
	}
}
