using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;

namespace GladMMO
{
	public sealed class DefaultWorldTeleporterInteractableGameObjectBehaviourComponent : BaseDefinedGameObjectEntityBehaviourComponent<WorldTeleporterInstanceModel>, IWorldInteractable
	{
		private ILog Logger { get; }

		private IEventPublisher<IPlayerWorldTeleporterRequestedEventSubscribable, PlayerWorldTeleporterRequestEventArgs> PlayerTeleportEventPublisher { get; }

		public DefaultWorldTeleporterInteractableGameObjectBehaviourComponent(NetworkEntityGuid targetEntity, 
			GameObjectInstanceModel instanceData, 
			GameObjectTemplateModel templateData, 
			WorldTeleporterInstanceModel behaviourData,
			[NotNull] ILog logger,
			[NotNull] IEventPublisher<IPlayerWorldTeleporterRequestedEventSubscribable, PlayerWorldTeleporterRequestEventArgs> playerTeleportEventPublisher) 
			: base(targetEntity, instanceData, templateData, behaviourData)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			PlayerTeleportEventPublisher = playerTeleportEventPublisher ?? throw new ArgumentNullException(nameof(playerTeleportEventPublisher));
		}

		public void Interact([NotNull] NetworkEntityGuid entityInteracting)
		{
			if(entityInteracting == null) throw new ArgumentNullException(nameof(entityInteracting));

			if(Logger.IsInfoEnabled)
				Logger.Info($"Entity: {entityInteracting} interacted with Entity: {TargetEntity}.");

			PlayerTeleportEventPublisher.PublishEvent(this, new PlayerWorldTeleporterRequestEventArgs(this.BehaviourData.LinkedGameObjectId, entityInteracting));
		}
	}
}
