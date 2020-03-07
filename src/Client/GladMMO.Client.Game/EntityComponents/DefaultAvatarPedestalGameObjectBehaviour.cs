using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using UnityEngine;

namespace GladMMO
{
	public sealed class DefaultAvatarPedestalGameObjectBehaviour : BaseClientGameObjectEntityBehaviourComponent, IBehaviourComponentInitializable
	{
		private IFactoryCreatable<CustomModelLoaderCancelable, CustomModelLoaderCreationContext> AvatarLoaderFactory { get; }

		private ILog Logger { get; }

		public DefaultAvatarPedestalGameObjectBehaviour(ObjectGuid targetEntity, 
			GameObject rootSceneObject, 
			IEntityDataFieldContainer data,
			[NotNull] IFactoryCreatable<CustomModelLoaderCancelable, CustomModelLoaderCreationContext> avatarLoaderFactory,
			[NotNull] ILog logger) 
			: base(targetEntity, rootSceneObject, data)
		{
			AvatarLoaderFactory = avatarLoaderFactory ?? throw new ArgumentNullException(nameof(avatarLoaderFactory));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public void Initialize()
		{
			int avatarModelId = this.Data.GetFieldValue<int>(EGameObjectFields.GAMEOBJECT_DISPLAYID);

			if(Logger.IsInfoEnabled)
				Logger.Info($"Pedestal with ModelId: {avatarModelId} about to load model.");

			//This just loads the avatar on top of the gameobject.
			CustomModelLoaderCancelable loaderCancelable = AvatarLoaderFactory.Create(new CustomModelLoaderCreationContext(avatarModelId, TargetEntity, UserContentType.Avatar));
			loaderCancelable.StartLoading();
		}
	}
}
