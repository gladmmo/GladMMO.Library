using Common.Logging;
using FreecraftCore;
using Glader.Essentials;
using System;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class OnEntityCreatingRegisterModelChangeListenerEventListener : BaseSingleEventListenerInitializable<IEntityCreationStartingEventSubscribable, EntityCreationStartingEventArgs>
	{
		private IEntityDataChangeCallbackRegisterable EntityDataCallbackRegister { get; }

		private ILog Logger { get; }

		private IFactoryCreatable<CustomModelLoaderCancelable, CustomModelLoaderCreationContext> AvatarLoaderFactory { get; }

		private IEntityGuidMappable<CustomModelLoaderCancelable> AvatarLoaderMappable { get; }

		public OnEntityCreatingRegisterModelChangeListenerEventListener(IEntityCreationStartingEventSubscribable subscriptionService,
			[NotNull] IEntityDataChangeCallbackRegisterable entityDataCallbackRegister,
			[NotNull] ILog logger,
			[NotNull] IFactoryCreatable<CustomModelLoaderCancelable, CustomModelLoaderCreationContext> avatarLoaderFactory,
			[NotNull] IEntityGuidMappable<CustomModelLoaderCancelable> avatarLoaderMappable) 
			: base(subscriptionService)
		{
			EntityDataCallbackRegister = entityDataCallbackRegister ?? throw new ArgumentNullException(nameof(entityDataCallbackRegister));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			AvatarLoaderFactory = avatarLoaderFactory ?? throw new ArgumentNullException(nameof(avatarLoaderFactory));
			AvatarLoaderMappable = avatarLoaderMappable ?? throw new ArgumentNullException(nameof(avatarLoaderMappable));
		}

		protected override void OnEventFired(object source, EntityCreationStartingEventArgs args)
		{
			//Whyyyy Blizzard, so dumb... We need SEPERATE handling for DisplayID for GameObjects
			if (args.EntityGuid.TypeId == EntityTypeId.TYPEID_GAMEOBJECT)
				EntityDataCallbackRegister.RegisterCallback<int>(args.EntityGuid, (int)EGameObjectFields.GAMEOBJECT_DISPLAYID, HandleModelChange);
			else
				EntityDataCallbackRegister.RegisterCallback<int>(args.EntityGuid, (int)EUnitFields.UNIT_FIELD_DISPLAYID, HandleModelChange);
		}

		private void HandleModelChange([NotNull] ObjectGuid entityGuid, EntityDataChangedArgs<int> changedModelId)
		{
			if (entityGuid == null) throw new ArgumentNullException(nameof(entityGuid));

			//The new id of the model is now known.
			CustomModelLoaderCancelable cancelable = AvatarLoaderFactory.Create(new CustomModelLoaderCreationContext(changedModelId.NewValue, entityGuid));

			if (AvatarLoaderMappable.ContainsKey(entityGuid))
			{
				AvatarLoaderMappable.RetrieveEntity(entityGuid).Cancel();
				AvatarLoaderMappable.ReplaceObject(entityGuid, cancelable);
			}
			else
				AvatarLoaderMappable.AddObject(entityGuid, cancelable);

			cancelable.StartLoading();
		}
	}
}
