using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using UnityEngine;

namespace GladMMO
{
	public sealed class CustomAvatarLoaderCancelableFactory : IFactoryCreatable<CustomAvatarLoaderCancelable, CustomAvatarLoaderCreationContext>, IAvatarPrefabCompletedDownloadEventSubscribable
	{
		private ILoadableContentResourceManager ContentResourceManager { get; }

		private ILog Logger { get; }

		public event EventHandler<AvatarPrefabCompletedDownloadEventArgs> OnAvatarPrefabCompletedDownloading;

		public CustomAvatarLoaderCancelableFactory([NotNull] ILoadableContentResourceManager contentResourceManager, [NotNull] ILog logger)
		{
			ContentResourceManager = contentResourceManager ?? throw new ArgumentNullException(nameof(contentResourceManager));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public CustomAvatarLoaderCancelable Create(CustomAvatarLoaderCreationContext context)
		{
			Task<IPrefabContentResourceHandle> avatarPrefabAsync = ContentResourceManager.LoadAvatarPrefabAsync(context.AvatarId);

			return new CustomAvatarLoaderCancelable(avatarPrefabAsync, Logger, prefab =>
			{
				//Callback should only occur is avatarPrefabAsync has completed
				IPrefabContentResourceHandle handle = avatarPrefabAsync.Result;

				OnAvatarPrefabCompletedDownloading?.Invoke(this, new AvatarPrefabCompletedDownloadEventArgs(handle, prefab, context.EntityGuid));
			});
		}
	}
}
