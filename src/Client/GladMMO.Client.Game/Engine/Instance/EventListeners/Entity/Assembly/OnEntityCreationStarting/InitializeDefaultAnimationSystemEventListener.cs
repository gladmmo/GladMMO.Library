using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class InitializeDefaultAnimationSystemEventListener : EntityCreationStartingEventListener
	{
		private IEntityGuidMappable<EntityAnimationController> AnimationControllerMappable { get; }

		public InitializeDefaultAnimationSystemEventListener(IEntityCreationStartingEventSubscribable subscriptionService,
			[NotNull] IEntityGuidMappable<EntityAnimationController> animationControllerMappable) 
			: base(subscriptionService)
		{
			AnimationControllerMappable = animationControllerMappable ?? throw new ArgumentNullException(nameof(animationControllerMappable));
		}

		protected override void OnEventFired(object source, EntityCreationStartingEventArgs args)
		{
			//Since we're just starting to create, we are null for animation
			AnimationControllerMappable.AddObject(args.EntityGuid, new EntityAnimationController(null));
		}
	}
}
