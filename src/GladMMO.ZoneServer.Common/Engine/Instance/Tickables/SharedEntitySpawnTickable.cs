using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Glader.Essentials;

namespace GladMMO
{
	/// <summary>
	/// Base abstract type for both server/client entity spawn handling.
	/// Generic type parameters should be the event pair that should trigger this event queue.
	/// </summary>
	/// <typeparam name="TEventInterfaceType">The event interface this tickable should listen for.</typeparam>
	/// <typeparam name="TEventArgs">The entity container event args.</typeparam>
	[AdditionalRegisterationAs(typeof(IEntityCreationStartingEventSubscribable))]
	[AdditionalRegisterationAs(typeof(IEntityCreationFinishedEventSubscribable))]
	public abstract class SharedEntitySpawnTickable<TEventInterfaceType, TEventArgs> : EventQueueBasedTickable<TEventInterfaceType, TEventArgs>, IEntityCreationStartingEventSubscribable, IEntityCreationFinishedEventSubscribable
		where TEventInterfaceType : class 
		where TEventArgs : EventArgs, IEntityGuidContainer
	{
		private IKnownEntitySet KnownEntities { get; }

		public event EventHandler<EntityCreationStartingEventArgs> OnEntityCreationStarting;

		public event EventHandler<EntityCreationFinishedEventArgs> OnEntityCreationFinished;

		/// <inheritdoc />
		protected SharedEntitySpawnTickable([NotNull] TEventInterfaceType subscriptionService,
			[NotNull] ILog logger,
			[NotNull] IKnownEntitySet knownEntities)
			: base(subscriptionService, true, logger) //TODO: We probably shouldn't spawn everything per frame. We should probably stagger spawning.
		{
			KnownEntities = knownEntities ?? throw new ArgumentNullException(nameof(knownEntities));
		}

		/// <inheritdoc />
		protected override void HandleEvent(TEventArgs args)
		{
			//Due to issue with TrinityCore/WoW design or implementation
			//we must actually check if the entity already exists.
			//Otherwise, we will duplicate spawn
			if (KnownEntities.isEntityKnown(args.EntityGuid))
			{
				if(Logger.IsWarnEnabled)
					Logger.Warn($"Duplicate Spawn Request: {args.EntityGuid} (Known Issue). Ignore.");

				return;
			}

			try
			{
				if(Logger.IsInfoEnabled)
					Logger.Info($"Spawning Entity: {args.EntityGuid}.");

				//It should be assumed none of the event listeners will be async
				OnEntityCreationStarting?.Invoke(this, new EntityCreationStartingEventArgs(args.EntityGuid));

				KnownEntities.AddEntity(args.EntityGuid);

				if(Logger.IsDebugEnabled)
					Logger.Debug($"Entity: {args.EntityGuid.TypeId}:{args.EntityGuid.CurrentObjectGuid} is now known.");

				OnEntityCreationFinished?.Invoke(this, new EntityCreationFinishedEventArgs(args.EntityGuid));
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
