using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Glader.Essentials;
using Nito.AsyncEx;

namespace GladMMO
{
	/// <summary>
	/// Base abstract type for both server/client entity despawn handling.
	/// Generic type parameters should be the event pair that should trigger this event queue.
	/// </summary>
	/// <typeparam name="TEventInterfaceType">The event interface this tickable should listen for.</typeparam>
	/// <typeparam name="TEventArgs">The entity container event args.</typeparam>
	[AdditionalRegisterationAs(typeof(IEntityDeconstructionStartingEventSubscribable))]
	[AdditionalRegisterationAs(typeof(IEntityDeconstructionFinishedEventSubscribable))]
	public abstract class SharedEntityDespawnTickable<TEventInterfaceType, TEventArgs> : EventQueueBasedTickable<TEventInterfaceType, TEventArgs>, IEntityDeconstructionStartingEventSubscribable, IEntityDeconstructionFinishedEventSubscribable
		where TEventInterfaceType : class
		where TEventArgs : EventArgs, IEntityGuidContainer
	{
		public event EventHandler<EntityDeconstructionStartingEventArgs> OnEntityDeconstructionStarting;

		public event EventHandler<EntityDeconstructionFinishedEventArgs> OnEntityDeconstructionFinished;

		protected IKnownEntitySet KnownEntities { get; }

		private IReadonlyEntityGuidMappable<AsyncLock> LockMappable { get; }

		protected SharedEntityDespawnTickable(TEventInterfaceType subscriptionService, 
			ILog logger, 
			[NotNull] IKnownEntitySet knownEntities,
			[NotNull] IReadonlyEntityGuidMappable<AsyncLock> lockMappable)
			: base(subscriptionService, true, logger)
		{
			KnownEntities = knownEntities ?? throw new ArgumentNullException(nameof(knownEntities));
			LockMappable = lockMappable ?? throw new ArgumentNullException(nameof(lockMappable));
		}

		/// <inheritdoc />
		protected override void HandleEvent(TEventArgs args)
		{
			//Do it BEFOREHAND because we actually check the lock map
			//and it's possible we do not know this entity
			//and therefore will encounter throws if we try to direct access
			//before checking if known.
			//This can happen with TrinityCore related to remote player equipped items
			//not create packets are sent for these, but despawns occur.
			if(!KnownEntities.isEntityKnown(args.EntityGuid))
				if(Logger.IsWarnEnabled)
				{
					Logger.Warn($"Tried to cleanup unknown Entity: {args.EntityGuid}");
					return;
				}

			AsyncLock syncObj = LockMappable.RetrieveEntity(args.EntityGuid);

			using (syncObj.Lock())
			{
				try
				{
					if(Logger.IsInfoEnabled)
						Logger.Info($"Despawning Entity: {args.EntityGuid}.");

					//It should be assumed none of the event listeners will be async
					OnEntityDeconstructionStarting?.Invoke(this, new EntityDeconstructionStartingEventArgs(args.EntityGuid));

					KnownEntities.RemoveEntity(args.EntityGuid);

					if(Logger.IsDebugEnabled)
						Logger.Debug($"Entity: {args.EntityGuid.TypeId}:{args.EntityGuid.CurrentObjectGuid} is now forgotten.");

					OnEntityDeconstructionFinished?.Invoke(this, new EntityDeconstructionFinishedEventArgs(args.EntityGuid));
				}
				catch(Exception e)
				{
					if(Logger.IsErrorEnabled)
						Logger.Error($"Failed to Create Entity: {args.EntityGuid} Exception: {e.Message}\n\nStack: {e.StackTrace}");

					throw;
				}
			}
		}
	}
}
