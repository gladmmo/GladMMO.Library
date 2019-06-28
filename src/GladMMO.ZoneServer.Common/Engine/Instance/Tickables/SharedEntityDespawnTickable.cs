using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Glader.Essentials;

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

		protected SharedEntityDespawnTickable(TEventInterfaceType subscriptionService, 
			ILog logger, 
			[NotNull] IKnownEntitySet knownEntities)
			: base(subscriptionService, true, logger)
		{
			KnownEntities = knownEntities ?? throw new ArgumentNullException(nameof(knownEntities));
		}

		/// <inheritdoc />
		protected override void HandleEvent(TEventArgs args)
		{
			try
			{
				using(KnownEntities.LockObject.ReaderLock())
					if (!KnownEntities.isEntityKnown(args.EntityGuid))
						if (Logger.IsErrorEnabled)
							Logger.Error($"Tried to cleanup unknown Entity: {args.EntityGuid}");

				if(Logger.IsInfoEnabled)
					Logger.Info($"Despawning Entity: {args.EntityGuid}.");

				//It should be assumed none of the event listeners will be async
				OnEntityDeconstructionStarting?.Invoke(this, new EntityDeconstructionStartingEventArgs(args.EntityGuid));

				//For multithread syncronization we should lock the known entities list, since it is now known and will change the known entity collection.
				using(KnownEntities.LockObject.WriterLock())
					KnownEntities.RemoveEntity(args.EntityGuid);

				if(Logger.IsDebugEnabled)
					Logger.Debug($"Entity: {args.EntityGuid.EntityType}:{args.EntityGuid.EntityId} is now forgotten.");

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
