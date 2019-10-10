using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Glader.Essentials;
using Nito.AsyncEx;

namespace GladMMO
{
	public sealed class DefaultAvatarPedestalInteractableGameObjectBehaviourComponent : BaseDefinedGameObjectEntityBehaviourComponent<AvatarPedestalInstanceModel>, IWorldInteractable, IBehaviourComponentInitializable
	{
		private ILog Logger { get; }

		private IReadonlyEntityGuidMappable<IEntityDataFieldContainer> EntityDataContainer { get; }

		private IZoneServerToGameServerClient ZoneServerDataClient { get; }

		public DefaultAvatarPedestalInteractableGameObjectBehaviourComponent(NetworkEntityGuid targetEntity, 
			GameObjectInstanceModel instanceData, 
			GameObjectTemplateModel templateData,
			AvatarPedestalInstanceModel behaviourData,
			[NotNull] ILog logger,
			[NotNull] IReadonlyEntityGuidMappable<IEntityDataFieldContainer> entityDataContainer,
			[NotNull] IZoneServerToGameServerClient zoneServerDataClient) 
			: base(targetEntity, instanceData, templateData, behaviourData)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			EntityDataContainer = entityDataContainer ?? throw new ArgumentNullException(nameof(entityDataContainer));
			ZoneServerDataClient = zoneServerDataClient ?? throw new ArgumentNullException(nameof(zoneServerDataClient));
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

			int currentAvatarId = dataFieldContainer.GetFieldValue<int>(BaseObjectField.UNIT_FIELD_DISPLAYID);
			dataFieldContainer.SetFieldValue(BaseObjectField.UNIT_FIELD_DISPLAYID, BehaviourData.AvatarModelId);

			//We update IMMEDIATELY for reponsiveness, but we can revert if it fails to save.
			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				try
				{
					await ZoneServerDataClient.UpdatePlayerAvatar(new ZoneServerAvatarPedestalInteractionCharacterRequest(entityInteracting, BehaviourData.AvatarModelId));
				}
				catch (Exception e)
				{
					//TODO: Better log.
					if(Logger.IsErrorEnabled)
						Logger.Error($"Failed to save Avatar Change. Reason: {e.Message}");


					//Even if the player has left, we still reference the data container so it should be safe
					//even if they no longer exist.
					if(currentAvatarId != 0) //special case, we cannot handle this so don't touch this. Just let them "pretend" for now.
						dataFieldContainer.SetFieldValue(BaseObjectField.UNIT_FIELD_DISPLAYID, currentAvatarId);
					throw;
				}
			});
		}

		public void Initialize()
		{
			//On initialization of this behaviour we want to basically set
			//some GameObject field data to indicate what this Pedestal is actually for.
			//We want the client to know the model id so that it can load relevant information when attempt to show it.
			IEntityDataFieldContainer entityData = EntityDataContainer.RetrieveEntity(TargetEntity);
			entityData.SetFieldValue(GameObjectField.RESERVED_DATA_1, BehaviourData.AvatarModelId);
		}
	}
}
