using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using Nito.AsyncEx;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class OnEntityCreatingRegisterModelChangeListenerEventListener : BaseSingleEventListenerInitializable<IEntityCreationStartingEventSubscribable, EntityCreationStartingEventArgs>
	{
		private IEntityDataChangeCallbackRegisterable EntityDataCallbackRegister { get; }

		private ILog Logger { get; }

		private IFactoryCreatable<CustomAvatarLoaderCancelable, CustomAvatarLoaderCreationContext> AvatarLoaderFactory { get; }

		private IEntityGuidMappable<CustomAvatarLoaderCancelable> AvatarLoaderMappable { get; }

		public OnEntityCreatingRegisterModelChangeListenerEventListener(IEntityCreationStartingEventSubscribable subscriptionService,
			[NotNull] IEntityDataChangeCallbackRegisterable entityDataCallbackRegister,
			[NotNull] ILog logger,
			[NotNull] IFactoryCreatable<CustomAvatarLoaderCancelable, CustomAvatarLoaderCreationContext> avatarLoaderFactory,
			[NotNull] IEntityGuidMappable<CustomAvatarLoaderCancelable> avatarLoaderMappable) 
			: base(subscriptionService)
		{
			EntityDataCallbackRegister = entityDataCallbackRegister ?? throw new ArgumentNullException(nameof(entityDataCallbackRegister));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			AvatarLoaderFactory = avatarLoaderFactory ?? throw new ArgumentNullException(nameof(avatarLoaderFactory));
			AvatarLoaderMappable = avatarLoaderMappable ?? throw new ArgumentNullException(nameof(avatarLoaderMappable));
		}

		protected override void OnEventFired(object source, EntityCreationStartingEventArgs args)
		{
			//We only handle player downloading right now.
			if (args.EntityGuid.EntityType != EntityType.Player)
				return;

			//We're just using the WoW EUnitFields for now, and using display id for the avatar.
			EntityDataCallbackRegister.RegisterCallback<int>(args.EntityGuid, (int)EUnitFields.UNIT_FIELD_DISPLAYID, HandleModelChange);
		}

		private void HandleModelChange([NotNull] NetworkEntityGuid entityGuid, EntityDataChangedArgs<int> changedModelId)
		{
			if (entityGuid == null) throw new ArgumentNullException(nameof(entityGuid));

			//The new id of the model is now known.
			CustomAvatarLoaderCancelable cancelable = AvatarLoaderFactory.Create(new CustomAvatarLoaderCreationContext(changedModelId.NewValue, entityGuid));

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
