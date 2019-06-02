using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	[AdditionalRegisterationAs(typeof(IEntityInterestChangeEventSpoofable))]
	[AdditionalRegisterationAs(typeof(IEntityInterestChangeEventSubscribable))]
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class InterestPhysicsTriggerEventListener : IGameInitializable, IEntityInterestChangeEventSubscribable, IEntityInterestChangeEventSpoofable
	{
		private IPhysicsTriggerEventSubscribable TriggerEventSubscribable { get; }

		public IReadonlyGameObjectToEntityMappable ObjectToEntityMapper { get; }

		public ILog Logger { get; }

		/// <inheritdoc />
		public event EventHandler<EntityInterestChangeEventArgs> OnEntityInterestChanged;

		/// <inheritdoc />
		public InterestPhysicsTriggerEventListener(
			[NotNull] IPhysicsTriggerEventSubscribable triggerEventSubscribable, 
			[NotNull] IReadonlyGameObjectToEntityMappable objectToEntityMapper, 
			[NotNull] ILog logger)
		{
			TriggerEventSubscribable = triggerEventSubscribable ?? throw new ArgumentNullException(nameof(triggerEventSubscribable));
			ObjectToEntityMapper = objectToEntityMapper ?? throw new ArgumentNullException(nameof(objectToEntityMapper));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		private void InterestTriggerExit([NotNull] object sender, [NotNull] PhysicsTriggerEventArgs args)
		{
			if(sender == null) throw new ArgumentNullException(nameof(sender));
			if(args == null) throw new ArgumentNullException(nameof(args));

			GameObject rootObject = args.ColliderThatTriggered.GetRootGameObject();

			NetworkEntityGuid me = ObjectToEntityMapper.ObjectToEntityMap[args.GameObjectTriggered.transform.GetRootGameObject()];

			if(!ObjectToEntityMapper.ObjectToEntityMap.ContainsKey(rootObject))
			{
				if(Logger.IsWarnEnabled)
					Logger.Warn($"Tried to remove Entity: {rootObject.name} from Entity interest ID: {me} but does not exist. Is not owned.");

				return;
			}

			OnEntityInterestChanged?.Invoke(this, new EntityInterestChangeEventArgs(me, ObjectToEntityMapper.ObjectToEntityMap[rootObject], EntityInterestChangeEventArgs.ChangeType.Exit));
		}

		private void InterestTriggerEnter([NotNull] object sender, [NotNull] PhysicsTriggerEventArgs args)
		{
			if(sender == null) throw new ArgumentNullException(nameof(sender));
			if(args == null) throw new ArgumentNullException(nameof(args));

			GameObject rootObject = args.ColliderThatTriggered.GetRootGameObject();

			//TODO: This WON'T work if you parent the object. We NEED a better way to handle looking up the guid from collision
			//TODO: Should entites be interested in themselves?
			//Right now the way this is set up we don't check if this is the entity itself.
			//Which means an entity is always interested in itself, unless manually removed
			//because when it enters the world it will enter its own interest radius
			//This behavior MAY change.
			NetworkEntityGuid me = ObjectToEntityMapper.ObjectToEntityMap[args.GameObjectTriggered.transform.GetRootGameObject()];

			if(!ObjectToEntityMapper.ObjectToEntityMap.ContainsKey(rootObject))
			{
				if(Logger.IsWarnEnabled)
					Logger.Warn($"Tried to enter Entity: {rootObject.name} to Entity interest ID: {me} but does not exist. Is not owned.");

				return;
			}

			OnEntityInterestChanged?.Invoke(this, new EntityInterestChangeEventArgs(me, ObjectToEntityMapper.ObjectToEntityMap[rootObject], EntityInterestChangeEventArgs.ChangeType.Enter));
		}

		/// <inheritdoc />
		public Task OnGameInitialized()
		{
			TriggerEventSubscribable.RegisterTriggerEnterEventSubscription(PhysicsTriggerEventType.Interest, InterestTriggerEnter);
			TriggerEventSubscribable.RegisterTriggerExitEventSubscription(PhysicsTriggerEventType.Interest, InterestTriggerExit);

			return Task.CompletedTask;
		}

		/// <inheritdoc />
		public void SpoofExitInterest(EntityInterestChangeEventArgs args)
		{
			if(args.ChangingType != EntityInterestChangeEventArgs.ChangeType.Exit)
				throw new InvalidOperationException($"Called: {nameof(SpoofExitInterest)} with args: {args.ChangingType}. Must be Exit.");

			OnEntityInterestChanged?.Invoke(this, args);
		}

		/// <inheritdoc />
		public void SpoofEnterInterest(EntityInterestChangeEventArgs args)
		{
			if(args.ChangingType != EntityInterestChangeEventArgs.ChangeType.Enter)
				throw new InvalidOperationException($"Called: {nameof(SpoofEnterInterest)} with args: {args.ChangingType}. Must be Enter.");

			OnEntityInterestChanged?.Invoke(this, args);
		}
	}
}
