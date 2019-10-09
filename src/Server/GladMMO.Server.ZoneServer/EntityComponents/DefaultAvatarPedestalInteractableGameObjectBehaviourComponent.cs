using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;

namespace GladMMO
{
	public sealed class DefaultAvatarPedestalInteractableGameObjectBehaviourComponent : BaseDefinedGameObjectEntityBehaviourComponent<AvatarPedestalInstanceModel>, IWorldInteractable
	{
		private ILog Logger { get; }

		private IReadonlyEntityGuidMappable<IEntityDataFieldContainer> EntityDataContainer { get; }

		public DefaultAvatarPedestalInteractableGameObjectBehaviourComponent(NetworkEntityGuid targetEntity, 
			GameObjectInstanceModel instanceData, 
			GameObjectTemplateModel templateData,
			AvatarPedestalInstanceModel behaviourData,
			[NotNull] ILog logger,
			[NotNull] IReadonlyEntityGuidMappable<IEntityDataFieldContainer> entityDataContainer) 
			: base(targetEntity, instanceData, templateData, behaviourData)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			EntityDataContainer = entityDataContainer ?? throw new ArgumentNullException(nameof(entityDataContainer));
		}

		public void Interact([NotNull] NetworkEntityGuid entityInteracting)
		{
			if(entityInteracting == null) throw new ArgumentNullException(nameof(entityInteracting));

			if(Logger.IsInfoEnabled)
				Logger.Info($"Entity: {entityInteracting} interacted with Entity: {TargetEntity}.");

			//Only players should be able to interact with this.
			if (entityInteracting.EntityType != EntityType.Player)
				return;

			IEntityDataFieldContainer dataFieldContainer = EntityDataContainer.RetrieveEntity(entityInteracting);

			dataFieldContainer.SetFieldValue(EUnitFields.UNIT_FIELD_DISPLAYID, BehaviourData.AvatarModelId);
		}
	}
}
