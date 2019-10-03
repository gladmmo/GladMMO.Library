using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;

namespace GladMMO
{
	public sealed class DefaultWorldTeleporterInteractableGameObjectBehaviourComponent : BaseDefinedGameObjectEntityBehaviourComponent<WorldTeleporterInstanceModel>, IWorldInteractable
	{
		private ILog Logger { get; }

		public DefaultWorldTeleporterInteractableGameObjectBehaviourComponent(NetworkEntityGuid targetEntity, 
			GameObjectInstanceModel instanceData, 
			GameObjectTemplateModel templateData, 
			WorldTeleporterInstanceModel behaviourData,
			[NotNull] ILog logger) 
			: base(targetEntity, instanceData, templateData, behaviourData)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public void Interact([NotNull] NetworkEntityGuid entityInteracting)
		{
			if(entityInteracting == null) throw new ArgumentNullException(nameof(entityInteracting));

			if(Logger.IsInfoEnabled)
				Logger.Info($"Entity: {entityInteracting} interacted with Entity: {TargetEntity}.");
		}
	}
}
