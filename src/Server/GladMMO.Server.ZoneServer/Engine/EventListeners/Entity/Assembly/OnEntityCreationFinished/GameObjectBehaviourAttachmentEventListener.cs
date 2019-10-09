using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Glader.Essentials;

namespace GladMMO
{
	/// <summary>
	/// Event listener that listens for the finished construction of a GameObject entity.
	/// It then attaches any required <see cref="BaseGameObjectEntityBehaviourComponent"/>.
	/// </summary>
	[AdditionalRegisterationAs(typeof(IGameObjectBehaviourCreatedEventSubscribable))]
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class GameObjectBehaviourAttachmentEventListener : GameObjectCreationFinishedEventListener, IGameObjectBehaviourCreatedEventSubscribable
	{
		private IGameObjectEntityBehaviourFactory BehaviourFactory { get; }

		private IReadonlyEntityGuidMappable<GameObjectTemplateModel> GameObjectTemplateMappable { get; }

		private IEntityGuidMappable<BaseGameObjectEntityBehaviourComponent> GameObjectBehaviorComponentMappable { get; }

		private ILog Logger { get; }

		public event EventHandler<GameObjectBehaviourCreatedEventArgs> OnBehaviourCreated;

		public GameObjectBehaviourAttachmentEventListener(IEntityCreationFinishedEventSubscribable subscriptionService,
			[NotNull] IGameObjectEntityBehaviourFactory behaviourFactory,
			[NotNull] IReadonlyEntityGuidMappable<GameObjectTemplateModel> gameObjectTemplateMappable,
			[NotNull] IEntityGuidMappable<BaseGameObjectEntityBehaviourComponent> gameObjectBehaviorComponentMappable,
			[NotNull] ILog logger) 
			: base(subscriptionService)
		{
			BehaviourFactory = behaviourFactory ?? throw new ArgumentNullException(nameof(behaviourFactory));
			GameObjectTemplateMappable = gameObjectTemplateMappable ?? throw new ArgumentNullException(nameof(gameObjectTemplateMappable));
			GameObjectBehaviorComponentMappable = gameObjectBehaviorComponentMappable ?? throw new ArgumentNullException(nameof(gameObjectBehaviorComponentMappable));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void OnEntityCreationFinished(EntityCreationFinishedEventArgs args)
		{
			GameObjectTemplateModel template = GameObjectTemplateMappable.RetrieveEntity(args.EntityGuid);

			//We don't need to do any behavior for visible objects.
			if (template.ObjectType == GameObjectType.Visual)
				return;

			var behavior = BehaviourFactory.Create(args.EntityGuid);

			if(Logger.IsInfoEnabled)
				Logger.Info($"Attached {behavior.GetType().Name} to Entity: {args.EntityGuid}");

			GameObjectBehaviorComponentMappable.AddObject(args.EntityGuid, behavior);

			OnBehaviourCreated?.Invoke(this, new GameObjectBehaviourCreatedEventArgs(behavior));
		}
	}
}
