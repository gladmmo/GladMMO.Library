using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class InitializeOptionalMovementDirectionListenerEventListener : EntityAvatarChangedEventListener
	{
		private IEntityGuidMappable<IMovementDirectionChangedListener> MovementDirectionListenerMappable { get; }

		public InitializeOptionalMovementDirectionListenerEventListener(IEntityAvatarChangedEventSubscribable subscriptionService,
			[NotNull] IEntityGuidMappable<IMovementDirectionChangedListener> movementDirectionListenerMappable) 
			: base(subscriptionService)
		{
			MovementDirectionListenerMappable = movementDirectionListenerMappable ?? throw new ArgumentNullException(nameof(movementDirectionListenerMappable));
		}

		protected override void OnEventFired(object source, EntityAvatarChangedEventArgs args)
		{
			IMovementDirectionChangedListener movementChangeListener = args.AvatarWorldRepresentation.GetComponentInChildren<IMovementDirectionChangedListener>();

			if (movementChangeListener != null)
			{
				if(MovementDirectionListenerMappable.ContainsKey(args.EntityGuid))
					MovementDirectionListenerMappable.ReplaceObject(args.EntityGuid, movementChangeListener);
				else
					MovementDirectionListenerMappable.AddObject(args.EntityGuid, movementChangeListener);
			}
			else
				MovementDirectionListenerMappable.RemoveEntityEntry(args.EntityGuid); //if it's null jsut remove one if it exists.
		}
	}
}
